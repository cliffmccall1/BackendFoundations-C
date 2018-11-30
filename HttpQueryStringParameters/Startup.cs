using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace HttpQueryStringParameters {
    public class Startup {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices (IServiceCollection services) { }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.Run (async (context) => {
                string response = "<h1>Query String Parameters</h1>" +
                    "<p>Enter a URL like:</p>" +
                    "<a href=\"http://localhost:5000/?firstname=Jane&lastname=Smith&age=30\">" +
                    "http://localhost:5000/?firstname=Jane&lastname=Smith&age=30</a>";
                foreach (var queryParameter in context.Request.Query) {
                    response += "<p>" + queryParameter + "</p>";
                }
                await context.Response.WriteAsync (response);
            });
        }
    }
}