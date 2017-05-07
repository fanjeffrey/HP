using HPCN.UnionOnline.Data;
using HPCN.UnionOnline.Services;
using HPCN.UnionOnline.Site.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HPCN.UnionOnline
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<HPCNUnionOnlineDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc()
                .AddRazorOptions(options =>
                {
                    options.ViewLocationFormats.Clear();
                    options.ViewLocationFormats.Add("/Site/Views/{0}.cshtml");
                    options.ViewLocationFormats.Add("/Site/Views/Shared/{0}.cshtml");
                    options.ViewLocationFormats.Add("/Site/Views/{1}/{0}.cshtml");
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireClaim("IsAdmin", true.ToString()));
            });

            services.Configure<ProductPictureOptions>(Configuration.GetSection("ProductPicture"));

            // Add application services.
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IActivityService, ActivityService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IEmailSender, SmtpEmailSender>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IOperationService, OperationService>();
            services.AddTransient<IOptionsService, OptionsService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IPointsService, PointsService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductPictureService, ProductPictureService>();
            services.AddTransient<IUserService, UserService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "HPCN.UnionOnline.CookieScheme",
                LoginPath = new PathString("/Account/Login/"),
                LogoutPath = new PathString("/Account/Logout"),
                AccessDeniedPath = new PathString("/Account/AccessDenied/"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                Events = new CookieAuthenticationEvents
                {
                    OnValidatePrincipal = ValidatePrincipal
                }
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var accountService = context.HttpContext.RequestServices.GetRequiredService<IAccountService>();
            var username = context.Principal.GetUsername();
            var updatedTime = context.Principal.GetUpdatedTime();

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(updatedTime) ||
                !await accountService.ValidatePrincipal(username, updatedTime))
            {
                context.RejectPrincipal();
                await context.HttpContext.Authentication.SignOutAsync("HPCN.UnionOnline.CookieScheme");
            }
        }
    }
}
