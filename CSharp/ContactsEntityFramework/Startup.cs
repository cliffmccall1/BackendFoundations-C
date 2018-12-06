using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsEntityFramework.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactsEntityFramework {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices (IServiceCollection services) {
            services.AddMvc ();

            /*
            Below you add a reference to the SQLite Database. Since the file referenced in the connectionString
            does not exist when you run the migration it will create the file and name it "ContactsEntityFramework.db"
            */
            var connectionString = "Data Source=ContactsEntityFramework.db";
            services.AddDbContext<ContactsContext> (options =>
                options.UseSqlite (connectionString));
        }

        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseExceptionHandler ("/Home/Error");
            }

            app.UseStaticFiles ();

            app.UseMvc (routes => {
                routes.MapRoute (
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}