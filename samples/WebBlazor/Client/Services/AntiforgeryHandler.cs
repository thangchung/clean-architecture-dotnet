// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor.Client
{
    public class AntiforgeryHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("X-CSRF", "1");
            return base.SendAsync(request, cancellationToken);
        }
    }
}