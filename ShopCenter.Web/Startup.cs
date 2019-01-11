using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Dal.EF;
using Shop.Dal.EF.Repositories;
using Shop.Domain.Contract;
using Shop.Domain.Contract.Repositories;
using Shop.Domain.Contrect.Repositories;
using Shop.Domain.Entities;
using ShopCenter.Web.Models;
using ShopCenter.Web.Payment;

namespace ShopCenter.Web
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }
        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("appsettings.json").Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
            });
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();
            services.AddDbContext<ShopCenterDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("NewConnection")));
            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Admin/Account/Login";
                options.AccessDeniedPath = "/Admin/Account/AccessDenied";
            });
            services.AddScoped<ShopCenterDBContext, ShopCenterDBContext>();
            services.AddScoped<UnitOfWork, UnitOfWork>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<Cart>(Sp => SessionCart.GetCart(Sp));
            services.AddTransient<IPayment, PaymentClass>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepsitory, OrderRepsitory>();
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            app.UseMvc(Route =>
{
    Route.MapRoute(
        name: "areas",
        template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    Route.MapRoute(
        name: "default",
        template: "{controller=Home}/{action=Index}/{id?}");
});
        }
    }
}