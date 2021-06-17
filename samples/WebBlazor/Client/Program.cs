// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazor.Client.Services;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using CoolStore.AppContracts.RestApi;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RestEase.HttpClientFactory;

namespace Blazor.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.TryAddSingleton<AuthenticationStateProvider, HostAuthenticationStateProvider>();
            builder.Services.TryAddSingleton(sp => (HostAuthenticationStateProvider)sp.GetRequiredService<AuthenticationStateProvider>());

            builder.Services.AddBlazorise(options =>
                {
                    options.ChangeTextOnKeyPress = false;
                    options.DelayTextOnKeyPress = true;
                    options.DelayTextOnKeyPressInterval = 1000;
                })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();

            builder.Services.AddTransient<AntiforgeryHandler>();

            // Because of HostAuthenticationStateProvider.cs
            builder.Services.AddHttpClient("backend", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<AntiforgeryHandler>();
            builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("backend"));

            builder.Services.AddRestEaseClient<IAppApi>(builder.HostEnvironment.BaseAddress)
                .AddHttpMessageHandler<AntiforgeryHandler>();

            builder.RootComponents.Add<App>("#app");
            await builder.Build().RunAsync();
        }
    }
}
