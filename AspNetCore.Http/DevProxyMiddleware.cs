using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DashboardCode.AspNetCore.Http
{
    public class DevProxyMiddleware
    {
        private readonly RequestDelegate next;
        private readonly DevProxyMiddlewareSettings devProxyMiddlewareSettings;

        public DevProxyMiddleware(RequestDelegate next, DevProxyMiddlewareSettings devProxyMiddlewareSettings)
        {
            this.devProxyMiddlewareSettings = devProxyMiddlewareSettings;
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments(devProxyMiddlewareSettings.FolderPathString, out PathString remainingPathString)
                && HttpMethods.IsGet(context.Request.Method)
                )
            {
                var httpRequestMessage = new HttpRequestMessage();
                foreach (var header in context.Request.Headers)
                    httpRequestMessage.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());

                var proxyUri = new Uri(devProxyMiddlewareSettings.ProxyUri, devProxyMiddlewareSettings.FolderPathString.Add(remainingPathString));
                httpRequestMessage.RequestUri = proxyUri;
                httpRequestMessage.Headers.Host = proxyUri.Host;
                httpRequestMessage.Method = HttpMethod.Get;
                using (var httpClient = new HttpClient())
                {
                    using (var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage, HttpCompletionOption.ResponseHeadersRead, context.RequestAborted))
                    {
                        var response = context.Response;
                        foreach (var header in httpResponseMessage.Content.Headers)
                            response.Headers[header.Key] = header.Value.ToArray();

                        foreach (var header in httpResponseMessage.Headers)
                            response.Headers[header.Key] = header.Value.ToArray();

                        response.Headers.Remove("transfer-encoding"); // if it was e.g. gzip it is not exactly true for our responce
                        response.StatusCode = (int)httpResponseMessage.StatusCode;

                        await httpResponseMessage.Content.CopyToAsync(response.Body);
                    }
                }
            }
            else
            {
                await next(context);
            }
        }
    }
}
