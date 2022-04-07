using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PainAssessment.Areas.Admin.Data;
using PainAssessment.Areas.Admin.Services;
using PainAssessment.Data;
using PainAssessment.Domain;
using PainAssessment.Interfaces;
using System;

using PainAssessment.Areas.ModuleTwo.Data;
using PainAssessment.Areas.ModuleTwo.Services;

namespace PainAssessment
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

            //mod 2 services 
            //services.AddTransient<IUnitOfwork, UnitOfWork>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(x => x.LoginPath = "/Login/Index");

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Login";
                //options.AccessDeniedPath = "/Identity/Account/Logout";
                options.SlidingExpiration = true;
            });

            services.AddDbContext<HospitalContext>(options =>
            // Localdb connection 
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            // MySQL connection
            // options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            // Add DI for Services
            services.AddScoped<IClinicalAreaService, ClinicalAreaService>();
            services.AddScoped<IPracticeTypeService, PracticeTypeService>();
            services.AddScoped<IPractitionerService, PractitionerService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IGatewayManager, GatewayManager>();
            services.AddScoped<IPainEducationService, PainEducationService>();


            services.AddControllersWithViews();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            // To add everytime there's new interface
            services.AddTransient<ITemplateChecklistService, TemplateChecklistService>();
            services.AddTransient<IDefaultQuestionsService, DefaultQuestionService>();
            services.AddTransient<ILoginService, LoginService>();
          
            services.AddDbContext<MvcChecklistContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("MvcChecklistContext")));

            services.AddDbContext<MvcConsultationChecklistContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("MvcConsultationChecklistContext")));
            services.AddTransient<IChecklistUnitOfWork, ChecklistUnitOfWork>();
            services.AddTransient<IChecklistService, ChecklistService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAdministratorService, AdministratorService>();

            services.AddScoped<ITemplateChecklistAdapter, TemplateChecklistAdapter>();

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

            app.UseStatusCodePagesWithReExecute("/Home/ErrorPage", "?statusCode={0}");

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                      name: "Admin",
                      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                    );
                    endpoints.MapControllerRoute(
                      name: "Module2",
                      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                    );
                });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
