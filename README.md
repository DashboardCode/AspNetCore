# DashboardCode.AspNetCore

Contains DevProxyMiddleware - proxy that pass GET requests (for `*.css` and `*.js` files) to the webpack devserver.

NUGET: https://www.nuget.org/packages/DashboardCode.Routines.AspNetCore/

First add this to the Startup.cs

    using DashboardCode.AspNetCore.Http;

Then add this to `Startup.ConfigureServices()` changing folder that contains bundle and URI of webpack deverver:

    serviceCollection.AddSingleton( new DevProxyMiddlewareSettings(new PathString("/dist"), new Uri("http://localhost:55555")));

Then add this to the `Startup.Configure:`

    app.UseMiddleware<DevProxyMiddleware>();

After this all GET requests like http://localhost:xxxx/dist/sample.js will be redirected to the http://localhost:55555/dist/sample.js


What is nice, it works for both cases: self-hosted and IISExpress hosted debugged application.

## Alternatives and other ideas:

https://github.com/EDGE10/Webpack.NET - loop through manifest.json (unfortunatly manifest.json also will be in memory when use deveserver, so it is left unknown form how they solve this problem)

  [1]: https://www.nuget.org/packages/DashboardCode.AspNetCore.Http/


