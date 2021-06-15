// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Duende.Bff;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Serilog;

namespace Blazor.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBff()
                .AddServerSideSessions();

            services.Replace(ServiceDescriptor.Transient<IUserService, DefaultUserService>());

            services.AddControllers();
            services.AddRazorPages();

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = "cookie";
                    options.DefaultChallengeScheme = "oidc";
                    options.DefaultSignOutScheme = "oidc";
                })
                .AddCookie("cookie", options =>
                {
                    options.Cookie.Name = "__Host-blazor";
                    options.Cookie.SameSite = SameSiteMode.Strict;
                })
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = "https://localhost:5001";
                    //options.RequireHttpsMetadata = false; // dev only

                    // confidential client using code flow + PKCE
                    options.ClientId = "spa";
                    options.ClientSecret = "secret";
                    options.ResponseType = "code";
                    options.ResponseMode = "query";
                    options.UsePkce = true;

                    options.MapInboundClaims = false;
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.SaveTokens = true;

                    // request scopes + refresh tokens
                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("api");
                    options.Scope.Add("offline_access");
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSerilogRequestLogging();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseBff();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBffManagementEndpoints();

                endpoints.MapRemoteBffApiEndpoint("/api", "http://localhost:5000")
                    .RequireAccessToken(TokenType.UserOrClient);

                endpoints.MapRazorPages();

                endpoints.MapControllers()
                    .RequireAuthorization()
                    .AsLocalBffApiEndpoint();

                endpoints.MapFallbackToFile("index.html");
            });
        }
    }

    /// <summary>
    /// Hacks: https://github.com/DuendeSoftware/BFF/blob/1.0.0-rc.1/src/Duende.Bff/Endpoints/DefaultUserService.cs
    /// </summary>
    public class DefaultUserService : IUserService
    {
        protected readonly BffOptions Options;

        public DefaultUserService(BffOptions options, ILoggerFactory loggerFactory)
        {
            Options = options;
        }

        /// <inheritdoc />
        public async Task ProcessRequestAsync(HttpContext context)
        {
            context.CheckForBffMiddleware(Options);

            if (Options.RequireAntiforgeryHeaderForUserEndpoint)
            {
                if (!context.CheckAntiForgeryHeader(Options))
                {
                    context.Response.StatusCode = 401;
                    return;
                }
            }

            var result = await context.AuthenticateAsync();

            if (!result.Succeeded)
            {
                context.Response.StatusCode = 401;
            }
            else
            {
                var claims = new List<ClaimRecord>();
                claims.AddRange(GetUserClaims(result));
                claims.AddRange(GetManagementClaims(result));

                var json = JsonSerializer.Serialize(claims);

                context.Response.StatusCode = 200;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json, Encoding.UTF8);
            }
        }

        protected virtual IEnumerable<ClaimRecord> GetUserClaims(AuthenticateResult authenticateResult)
        {
            return authenticateResult.Principal.Claims.Select(x => new ClaimRecord(x.Type, x.Value));
        }

        protected virtual IEnumerable<ClaimRecord> GetManagementClaims(AuthenticateResult authenticateResult)
        {
            var claims = new List<ClaimRecord>();

            var sessionId = authenticateResult.Principal.FindFirst(JwtClaimTypes.SessionId)?.Value;
            if (!String.IsNullOrWhiteSpace(sessionId))
            {
                claims.Add(new ClaimRecord(
                    Constants.ClaimTypes.LogoutUrl,
                    Options.ManagementBasePath.Add($"/logout?sid={UrlEncoder.Default.Encode(sessionId)}").Value));
            }

            var expiresInSeconds =
                authenticateResult.Properties.ExpiresUtc.Value.Subtract(DateTimeOffset.UtcNow).TotalSeconds;
            claims.Add(new ClaimRecord(
                Constants.ClaimTypes.SessionExpiresIn,
                Math.Round(expiresInSeconds).ToString(CultureInfo.InvariantCulture)));

            if (authenticateResult.Properties.Items.TryGetValue(OpenIdConnectSessionProperties.SessionState, out var sessionState))
            {
                claims.Add(new ClaimRecord(Constants.ClaimTypes.SessionState, sessionState));
            }

            return claims;
        }

        protected record ClaimRecord(string type, object value);
    }

    /// <summary>
    /// Hacks: https://github.com/DuendeSoftware/BFF/blob/1.0.0-rc.1/src/Duende.Bff/Extensions.cs#L10
    /// </summary>
    internal static class Extensions
    {
        public static void CheckForBffMiddleware(this HttpContext context, BffOptions options)
        {
            if (options.EnforceBffMiddlewareOnManagementEndpoints)
            {
                // Hacks: https://github.com/DuendeSoftware/BFF/blob/1.0.0-rc.1/src/Duende.Bff/Constants.cs#L8
                var found = context.Items.TryGetValue("Duende.Bff.BffMiddlewareMarker", out _);
                if (!found)
                {
                    throw new InvalidOperationException(
                        "The BFF middleware is missing in the pipeline. Add 'app.UseBff' after 'app.UseRouting' but before 'app.UseAuthorization'");
                }
            }
        }

        public static bool CheckAntiForgeryHeader(this HttpContext context, BffOptions options)
        {
            var antiForgeryHeader = context.Request.Headers[options.AntiForgeryHeaderName].FirstOrDefault();
            return antiForgeryHeader != null && antiForgeryHeader == options.AntiForgeryHeaderValue;
        }
    }
}
