﻿namespace Stock.Chat.CrossCutting.Handler
{
    public class LoggingHttpHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) => await base.SendAsync(request, cancellationToken);
    }
}
