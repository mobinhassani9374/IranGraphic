using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SeratGraphic.DomainModels.Entities;
using SeratGraphic.Data.Context;
using Microsoft.AspNetCore.Identity;
using SeratGraphic.Messaging.WhiteSms;
using Microsoft.Extensions.Configuration;
using SeratGraphic.Messaging.WhiteSms.Setting;

namespace SeratGraphic
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }
        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsetting.json", optional: false, reloadOnChange: true)
                .Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfigurationRoot>(c => { return Configuration; });

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<User, IdentityRole>(options =>
             {
                 options.SignIn.RequireConfirmedPhoneNumber = true;
                 options.Password.RequireDigit = false;
                 options.Password.RequiredLength = 6;
                 options.Password.RequiredUniqueChars = 0;
                 options.Password.RequireLowercase = false;
                 options.Password.RequireNonAlphanumeric = false;
                 options.Password.RequireUppercase = false;
             })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders()
            .AddClaimsPrincipalFactory<Identity.UserClaimsPrincipalFactory>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
                options.AccessDeniedPath = "/accessDenied";
                options.Cookie = new CookieBuilder()
                {
                    HttpOnly = true,
                    Name = "mobinHassani"
                };
            });

            services.AddHttpClient();

            services.AddMvc();

            //whiteSms injection
            services.AddScoped<Functions>();
            services.AddScoped<RequestProvider>();
            services.AddScoped<WhiteSmsService>();
            services.Configure<Key>(config => Configuration.GetSection("WhiteSMSConfig").Bind(config));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AppDbContext db,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {

            db.Database.Migrate();

            DatabaseInitializer.SeedData(userManager, roleManager);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
