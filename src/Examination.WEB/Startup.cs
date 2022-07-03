using Examination.Data.Models;
using Examination.Data.Services;
using Examination.WEB.Areas.Identity.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.WEB
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
            services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));
                                   
            services.AddScoped(typeof(ExaminationContext));
            services.AddScoped(typeof(ExaminationData));
            services.AddScoped<IExaminationData, ExaminationData>();
            services.AddControllersWithViews();
            

            services.AddDefaultIdentity<IdentityUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 5;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env,
                              UserManager<IdentityUser> userManager,
                              RoleManager<IdentityRole> roleManger)
        {
            bool dbWasCreated = false;
            using (var scope = app.ApplicationServices.CreateScope()) 
            {
                using (var identityContext = scope.ServiceProvider.GetService<AppDbContext>()) 
                {
                    if (identityContext.Database.EnsureCreated())
                        IdentityDataInitializer.SeedData(userManager, roleManger);

                    using (var examinationData = scope.ServiceProvider.GetService<ExaminationData>()) 
                    {
                        //examinationData.GetExaminationContext.Database.EnsureDeleted();
                        dbWasCreated = examinationData.GetExaminationContext?.Database.EnsureCreated() ?? false;
                        if (dbWasCreated)
                        {
                            examinationData.SeedTestData();
                        }
                    }
                }
            }

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

            app.UseAuthentication();
            app.UseAuthorization();
                        
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                
                endpoints.MapRazorPages();
            });
        }
    }
}
