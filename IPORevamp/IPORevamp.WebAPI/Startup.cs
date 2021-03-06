﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using IPORevamp.Data;
using IPORevamp.Data.UserManagement.Model;
using Swashbuckle.AspNetCore.Swagger;
using AutoMapper;
using EmailEngine.Base.Repository.EmailRepository;
using EmailEngine.Repository.EmailRepository;
using IPORevamp.Data.Entities.Email;
using EmailEngine.Repository.Interface;
using Autofac;
using EmailEngine.Repository.AutoFacModule;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using IPORevamp.WebAPI.Extension;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using IPORevamp.Data.Entities.AuditTrail;

using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Logging;
using IPORevamp.WebAPI.Filters;
using IPORevamp.WebAPI.Filters;
using ElmahCore;
using ElmahCore.Mvc;
using IPORevamp.Repository.AutoFacModule;
using IPORevamp.WebAPI.Models;
using System.Net;
using IPORevamp.Data.Entities.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using IPORevamp.Repository.Base;

using IPORevamp.Data.Entity.Interface;
using NACC.Data.UserManagement.Model;
using IPORevamp.Repository.Department;
using IPORevamp.Repository.Publication;

namespace IPORevamp.WebAPI
{


    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });



            services.AddDbContext<IPOContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services
                .AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<IPOContext>()
                .AddUserManager<ApplicationUserManager>()
                .AddRoleManager<ApplicationRoleManager>()
                .AddSignInManager<ApplicationSignInManager>()
                .AddUserStore<ApplicationUserStore>()
                .AddDefaultTokenProviders();
            services.AddScoped<ApplicationUserStore>();


            services.Configure<IdentityOptions>(options =>
            {

                // options.Password.RequireUppercase = true;
                options.Password.RequireUppercase = false;
                //  options.Password.RequiredUniqueChars = 1;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
                options.User.RequireUniqueEmail = true;
            });

            services.AddElmah(options =>
            {
                options.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
            });


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg =>
            {

                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["JwtIssuer"],
                    ValidateAudience = true,
                    ValidAudience = Configuration["JwtIssuer"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),


                    ValidateLifetime = false,
                    ClockSkew = TimeSpan.Zero,
                    // remove delay of token when expire,

                };
                cfg.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        //context. = new ObjectResult(new ApiResponse("Autorization has failed", (HttpStatusCode)context.Response.StatusCode, null, true));
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                        logger.LogError("Authentication failed.", context.Exception);
                        return Task.CompletedTask;
                    },


                    OnMessageReceived = context =>
                    {
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                        logger.LogError("OnChallenge error", context.Error, context.ErrorDescription);
                        return Task.CompletedTask;
                    },
                };

            });

            //services.AddScoped<IRepository<Email>>();
            services.AddTransient<IEmailManager<EmailLog, EmailTemplate>, EmailManager<EmailLog, EmailTemplate>>();
            services.AddTransient<IAuditTrailManager<AuditTrail>, AuditTrailManager<AuditTrail>>();
            services.AddTransient<IBilling<BillLog, PaymentLog, ApplicationUser, int>, Billing<BillLog, PaymentLog, ApplicationUser, int>>();
            services.AddTransient<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IAuthorizationHandler, AttendeeAuthorizer>();
            services.AddTransient<IEmailSender, EmailService>();
            services.AddSingleton(Configuration);

            services.AddScoped<IPublicationJob, PublicationJob>();





            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new Info { Title = "IPO Nigeria WebAPI", Version = "V1" });

                //  var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                //  var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);


                //  c.IncludeXmlComments(xmlPath);
            });

            services.AddDataProtection();

            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));


            services.Configure<ApiBehaviorOptions>(options => {
                options.SuppressModelStateInvalidFilter = true;

            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", c => { c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
            });


            services.AddMvc(options =>
            {
                //options.Filters.Add<ApiExceptionFilter>();
            }).AddJsonOptions(
                options => options.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore
    )

            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAutoMapper();
            services.AddLogging();

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule<BillRepositoryModule<BillLog, PaymentLog, ApplicationUser, int, IPOContext>>();
            builder.RegisterModule<RepositoryModule<EmailLog, EmailTemplate, IPOContext>>();
            builder.RegisterModule<GenericRepositoryModule<AuditTrail, IPOContext>>();
            builder.RegisterModule<RepositoryModule>();

            ApplicationContainer = builder.Build();

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(ApplicationContainer);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider, IBackgroundJobClient backgroundJobs)
        {

            // app.UseMiddleware<CookieChecker>();

            app.UseCors("AllowAllOrigins");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCookiePolicy();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(Configuration["swaggerjson"], "IPORevamp WebAPI V1");
            });

            //   GlobalConfiguration.Configuration
            //        .UseActivator(new HangfireActivator(serviceProvider));










            app.UseAuthentication();
            app.UseHangfireDashboard();


            var cronsetting = Cron.Hourly();

            var cronsetting2 = Cron.DayInterval(27);


            string cronExp = Cron.Daily();



            app.UseHangfireDashboard();
            app.UseHangfireServer();


            RecurringJob.AddOrUpdate<IPublicationJob>(j => j.CheckPublicationStatus(), cronExp);
            RecurringJob.AddOrUpdate<IPublicationJob>(j => j.CheckPendingApplication(), cronExp);
            RecurringJob.AddOrUpdate<IPublicationJob>(j => j.CheckDesignPublicationStatus(), cronExp);

           RecurringJob.AddOrUpdate<IPublicationJob>(j => j.SendMonthyUserReport(), cronsetting2);



            app.UseElmah();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
