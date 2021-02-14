using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Appapi.Data;
using Appapi.Data.Interfaces;
using Appapi.Data.UnitOfWork;
using Appapi.Helpers;
using Appapi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Appapi.Hubs;
using Appapi.EmailService;

namespace Appapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(x =>
            {
                x.UseLazyLoadingProxies();
                x.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            });
            ConfigureServices(services);

        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(x =>
            {
                x.UseLazyLoadingProxies();
                x.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            });
            ConfigureServices(services);
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        { 
            // to reset settings for padssword indetity 
            IdentityBuilder bulider = services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 4;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
            });

            bulider = new IdentityBuilder(bulider.UserType, typeof(Role), bulider.Services);
            bulider.AddEntityFrameworkStores<DataContext>();
            bulider.AddRoleValidator<RoleValidator<Role>>();
            bulider.AddRoleManager<RoleManager<Role>>();
            bulider.AddSignInManager<SignInManager<User>>();

            // Add Authentication to your app 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                        .GetBytes(Configuration.GetSection("AppSettings:ToKen").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            // add roles acsses pages
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequiredRootRole", policy => policy.RequireRole("Root"));
                options.AddPolicy("RequiredAdminRole", policy => policy.RequireRole("Admin"));
            });

            // filters policy
            services.AddControllers(options =>
             {
                 var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                 options.Filters.Add(new AuthorizeFilter(policy));
             }).AddNewtonsoftJson(opt =>
             {
                 opt.SerializerSettings.ReferenceLoopHandling =
                 Newtonsoft.Json.ReferenceLoopHandling.Ignore;
             }).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddCors();
            services.AddAutoMapper(typeof(UnitOfWork<>).Assembly);
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            
            services.AddSignalR(e => e.EnableDetailedErrors = true);
            services.Configure<EmailConfiguration>(Configuration.GetSection("EmailConfiguration"));
            services.AddTransient<IMailService, MailService>();
            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

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
                app.UseExceptionHandler(bulilder =>
                {
                    bulilder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }



             

            // app.UseHttpsRedirection(); 
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot")),
                RequestPath = new PathString("/wwwroot")
            });
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapHub<SignalRHub>("/SignalRHub"); endpoints.MapControllers(); });
        }
    }
}
