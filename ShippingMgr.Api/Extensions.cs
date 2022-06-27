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

    internal static class DbInitializerExtension
    {
        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<AppDataContext>();
                DbInitializer.Initialize(services,context);
            }
            catch (Exception ex)
            {

            }

            return app;
        }
    }

    internal class DbInitializer
    {
        internal static void Initialize(IServiceProvider services,AppDataContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
            dbContext.Database.EnsureCreated();

            //Add Users
            if (!dbContext.Roles.Any())
            {
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var adminRole = new IdentityRole();
                adminRole.Name = UserRoles.Admin;
                roleManager.CreateAsync(adminRole);

                var clientRole = new IdentityRole();
                clientRole.Name = UserRoles.Client;
                roleManager.CreateAsync(clientRole);

                var mazen = new AppUser { Email = "mazen506@gmail.com", UserName="mazen506@gmail.com", FirstName = "Mazen", LastName = "Mustafa" };
                var userResult = userManager.CreateAsync(mazen, "Test@1234").Result;
                if (!userResult.Succeeded)
                {
                    throw new Exception("Unable to create mazen:\r\n" + string.Join("\r\n", userResult.Errors.Select(error => $"{error.Code}: {error.Description}")));
                }
            }

            //Add Currencies
            if (!dbContext.Currencies.Any())
            {
                var currencies = new Currency[]
                {
                     new Currency{Name_En="Dollar", Name_Ar="دولار", Code_En="$", Code_Ar="$"}
                };
                dbContext.Currencies.AddRange(currencies);
            }

            //Add Items
            if (!dbContext.Currencies.Any())
            {
                var items = new Item[]
                {
                     new Item{Name="Jeans trouser", Price=1000, Description="Jeans trouser"},
                     new Item{Name="T-Shirt", Price=800, Description="T-Shirt"}
                };
                dbContext.Items.AddRange(items);
            }

            dbContext.SaveChanges();
        }
    }
}
