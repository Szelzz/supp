using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Supp.Core.Authorization;
using Supp.Core.Data.EF;
using Supp.Core.Posts;
using Supp.Core.Projects;
using Supp.Core.Users;
using Supp.Web.Infrastructure.DependencyInjection;
using Supp.Web.Security;
using System;
using System.Security.Claims;

namespace Supp.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // App services
            services.AddScoped<PostService>();
            services.AddScoped<ProjectService>();
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<PermissionAuthorizationService>();

            // Security
            services.AddScoped<AppAuthorizationService>();
            services.AddAuthorization();
            services.AddScoped<IAuthorizationHandler, ResourceAuthorizationHandler>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

            // Asp specific
            services.AddRouting(o => { o.LowercaseUrls = true; o.LowercaseQueryStrings = true; });
            services.AddRazorPages();
            services.AddHttpContextAccessor();
            services.AddTransient(f => f.GetService<IHttpContextAccessor>().HttpContext.User);

            // Identity
            services.AddScoped<IUserClaimsPrincipalFactory<User>, ClaimFactory>();
            services.AddIdentityWithoutRoles<User>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/AccessDenied";
                options.SlidingExpiration = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
            app.UseStaticFiles();
        }
    }
}
