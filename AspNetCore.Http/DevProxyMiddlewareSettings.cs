using System;
using Microsoft.AspNetCore.Http;

namespace DashboardCode.AspNetCore
{
    public class DevProxyMiddlewareSettings
    {
        public readonly PathString FolderPathString;
        public readonly Uri ProxyUri;
        public DevProxyMiddlewareSettings(PathString folderPathString, Uri proxyUri)
        {
            this.FolderPathString = folderPathString;
            this.ProxyUri = proxyUri;
        }
    }
}