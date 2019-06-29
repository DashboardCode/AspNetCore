# DashboardCode.AspNetCore

Contains DevProxyMiddleware - proxy that pass GET requests (for `*.css` and `*.js` files) to the webpack devserver.

First add this to the Startup.cs

    using DashboardCode.AspNetCore.Http;

Then add this to `Startup.ConfigureServices()` changing folder that contains bundle and URI of webpack deverver:

    serviceCollection.AddSingleton( new DevProxyMiddlewareSettings(new PathString("/dist"), new Uri("http://localhost:55510")));

Then add this to the `Startup.Configure:`

    app.UseMiddleware<DevProxyMiddleware>();

After this all GET requests like http://localhost:xxxx/dist/sample.js will be redirected to the http://localhost:55510/dist/sample.js


What is nice, it works for both cases: self-hosted and IISExpress hosted debugged application.

  [1]: https://www.nuget.org/packages/DashboardCode.AspNetCore.Http/


