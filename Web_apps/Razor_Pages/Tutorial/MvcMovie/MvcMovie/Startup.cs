using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Models;
using Microsoft.EntityFrameworkCore;

namespace MvcMovie
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Register the database context with the dependency injection container
            // (i.e. Startup.ConfigureServices)
            // The MvcMovieContext object handles the task of connecting to the database
            // and mapping Movie objects to database records
            services.AddDbContext<MvcMovieContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("MovieContext")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    /** 
                     * Model View Controller (MVC) invokes controller classes (
                     * and the action methods within them) depending on the incoming
                     * URL. The default URL routing logic used by MVC uses a format
                     * like this to determine what code to invoke:
                     * /[Controller]/[ActionName]/[Parameters]
                     *
                     * The routing format is set here.
                     *
                     * When you browse to the app and don't supply any URL segments,
                     * defaults to the "Home" controller and the "Index" method.
                     *
                     * The first URL segment determines the controller class to run.
                     * So localhost:xxxx/HelloWorld maps to the HelloWorldController class.
                     * The second part of the URL segment determines the action method
                     * on the class. So localhost:xxxx/HelloWorld/Index would cause
                     * the Index method of the HelloWorldController class to run.
                     * Notice that you only had to browse to localhost:xxxx/HelloWorld
                     * and the Index method was called by default. Because Index is
                     * the default method that will be called on a controller if a
                     * method name is not explicitly specified. The third part fo the
                     * URL segment (id) is for route data.
                    */
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
