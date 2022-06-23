using DotNetCore.EntityFrameworkCore;
using DotNetCore.IoC;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShippingMgr.Core.Application.Interfaces;
using ShippingMgr.Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ShippingMgr.Core.Database.Context;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using System.Configuration;
using ShippingMgr.Core.Helpers;
using ShippingMgr.Core.Application;
using Microsoft.AspNetCore.SpaServices.Extensions;

namespace ShippingMgr.Api.Extensions
{
    public static class Extensions
    {
        public static void AddContext(this IServiceCollection services)
        {
            var connectionString = services.GetConnectionString(nameof(AppDataContext));
            var serverVersion = new MySqlServerVersion(new Version(5, 5, 27));
            services.AddContext<AppDataContext>(options => 
                options.UseMySql(connectionString, serverVersion
                                )
                );
        }

        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAutoMapper((serviceProvider, automapper) =>
            {
                automapper.AddCollectionMappers();
                automapper.UseEntityFrameworkCoreModel<AppDataContext>(serviceProvider);
            }, typeof(AppDataContext).Assembly);

            var jwtTokenConfig = configuration.GetSection("jwtTokenConfig").Get<JwtTokenConfig>();
            services.AddSingleton(jwtTokenConfig);

            var serviceProvider = services.BuildServiceProvider();

            services.AddLocalization();
            services.AddRequestLocalization(x =>
            {
                x.DefaultRequestCulture = new RequestCulture("en");
                x.ApplyCurrentCultureToResponseHeaders = true;
                x.SupportedCultures = new List<CultureInfo> { new("ar"), new("en") };
                x.SupportedUICultures = new List<CultureInfo> { new("ar"), new("en") };
            });

            services.AddClassesMatchingInterfaces(typeof(ICurrencyService).Assembly);
            services.AddClassesMatchingInterfaces(typeof(IUserService).Assembly);
            services.AddClassesMatchingInterfaces(typeof(IEmailService).Assembly);
            services.AddClassesMatchingInterfaces(typeof(ILocalizeService).Assembly);
            services.AddSingleton<IJwtAuthManager, JwtAuthManager>();


            services.AddIdentity<AppUser, IdentityRole>(
                    options =>
                    {
                        options.SignIn.RequireConfirmedAccount = false;
                    }
            )
            .AddEntityFrameworkStores<AppDataContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<IUserClaimsPrincipalFactory<AppUser>,
                AppUserClaimsPrincipalFactory>();

            services.AddScoped<IPasswordHasher<AppUser>, BCryptPasswordHasher<AppUser>>();

            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: "AllowOrigin",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .WithExposedHeaders("*");
                    });
            });

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtTokenConfig.Issuer,
                    ValidAudience = jwtTokenConfig.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenConfig.Secret))
                };
            });

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {
            });
        }
    }
}
