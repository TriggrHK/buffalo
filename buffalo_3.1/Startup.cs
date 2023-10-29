using buffalo_3._1.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using buffalo_3._1.Models;
using Microsoft.AspNetCore.Http;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.ML.OnnxRuntime;

namespace buffalo_3._1
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
            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 12;
                options.Password.RequiredUniqueChars = 2;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential 
                // cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                // requires using Microsoft.AspNetCore.Http;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.ConsentCookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            services.AddSingleton<InferenceSession>(new InferenceSession("./wwwroot/onnx/model.onnx"));

            services.AddDbContext<buffaloContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            
            //services.AddDbContext<ApplicationDbContext>(options =>
            //  options.UseSqlServer(Configuration.GetConnectionString("UserConnection")));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("UserConnection")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
               .AddEntityFrameworkStores<ApplicationDbContext>();

            //enable session
            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCookiePolicy();
            app.UseStaticFiles();

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add(
                "Content-Security-Policy",
                "default-src 'self'; " +
                "script-src 'self';" +
                "style-src 'self' https://fonts.googleapis.com 'sha256-aqNNdDLnnrDOnTNdkJpYlAxKVJtLt9CtFLklmInuUAE=';" +
                "img-src 'self'; " +
                "font-src 'self' https://fonts.gstatic.com; " +
                "media-src 'self'; " +
                "frame-src 'self'; " +
                "connect-src 'self'"
                );
                await next();
            });

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //single routes for burial filters
                endpoints.MapControllerRoute(
                    name: "adultFilter",
                    pattern: "A={AdultFilter}/Page{pageNum}",
                    defaults: new { Controller = "Home", action = "Burials" }
                    );
                endpoints.MapControllerRoute(
                    name: "wrapFilter",
                    pattern: "W={WrappingFilter}/Page{pageNum}",
                    defaults: new { Controller = "Home", action = "Burials" }
                    );
                endpoints.MapControllerRoute(
                    name: "ageFilter",
                    pattern: "AG={AgeFilter}/Page{pageNum}",
                    defaults: new { Controller = "Home", action = "Burials" }
                    );
                endpoints.MapControllerRoute(
                    name: "hairFilter",
                    pattern: "HA={HairFilter}/Page{pageNum}",
                    defaults: new { Controller = "Home", action = "Burials" }
                    );
                endpoints.MapControllerRoute(
                    name: "headFilter",
                    pattern: "HE={HeadFilter}/Page{pageNum}",
                    defaults: new { Controller = "Home", action = "Burials" }
                    );
                endpoints.MapControllerRoute(
                    name: "samplesFilter",
                    pattern: "S={SamplesFilter}/Page{pageNum}",
                    defaults: new {Controller = "Home", action = "Burials"}
                    );
                endpoints.MapControllerRoute(
                    name: "tColorFilter",
                    pattern: "TCF={TColorFilter}/Page{pageNum}",
                    defaults: new { Controller = "Home", action = "Burials" }
                    ); 
                endpoints.MapControllerRoute(
                     name: "tFunctionFilter",
                     pattern: "TFF={TFunctionFilter}/Page{pageNum}",
                     defaults: new { Controller = "Home", action = "Burials" }
                     );
                endpoints.MapControllerRoute(
                     name: "sFunctionFilter",
                     pattern: "SFF={SFunctionFilter}/Page{pageNum}",
                     defaults: new { Controller = "Home", action = "Burials" }
                     );

                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
}
