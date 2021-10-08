using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Planner.Data;
using Planner.Models;
using Microsoft.AspNetCore.Identity;
using Planner.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json;
using Planner.Services;

namespace Planner
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
            // Add this in order to access HTTP context
            services.AddHttpContextAccessor();

            services.AddControllersWithViews(options => {
                options.AllowEmptyInputInBodyModelBinding = true;
            }).AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            );

            services.AddDbContext<DatabaseContext>(options =>
            {
                // Get connection string
                var connectionString = Configuration.GetConnectionString("DatabaseContext");

                // Establish connection
                options.UseSqlServer(connectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            // Add Identity with default configurations for User into Identity Role
            // Use EF to save information about Identity
            // Add Token provider
            // We MUST ADD user manager and sign in manager here
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders()
                .AddUserManager<UserManager<User>>()
                .AddSignInManager<SignInManager<User>>();

            services.Configure<IdentityOptions>(options =>
            {
                // Configure password
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

                // Configure email unique
                options.User.RequireUniqueEmail = true;
            });

            // Add options for mail sending
            services.AddOptions();
            var mailsettings = Configuration.GetSection("MailSettings");
            services.Configure<MailSettings>(mailsettings);
            services.AddTransient<IEmailSender, SendMailService>();

            // Register Http utils with DI
            services.AddScoped<IHttpUtils, HttpUtils>();

            // Register current user service with DI
            services.AddScoped<ICurrentUser, CurrentUserService>();

            // Register Error getter with DI
            services.AddScoped<IErrorGetter, ErrorGetter>();

            services.AddAuthentication();
            services.AddAuthorization();

            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseRouting();

            // Add authentication and authorization
            // authentication MUST BE placed before authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Welcome}/{action=Index}/{id?}");
            });
        }
    }
}
