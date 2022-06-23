using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using ShippingMgr.Api;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShippingMgr.Core.Database.Context;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Identity;
using ShippingMgr.Core.Models;
using Microsoft.Extensions.Logging.Console;

namespace ShippingMgr.IntegrationTests
{
    public class TestBase<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        private IServiceProvider _serviceProvider;
        public IServiceProvider GetServiceProvider()
        {
            return _serviceProvider;
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddLogging(builder => builder.ClearProviders().AddDebug());

                var descriptor = services.SingleOrDefault(
                   d => d.ServiceType ==
                       typeof(DbContextOptions<AppDataContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContextPool<AppDataContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                _serviceProvider = services.BuildServiceProvider();

                using (var scope = _serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;

                    var db = scopedServices.GetRequiredService<AppDataContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<TestBase<TEntryPoint>>>();
                    var userManager = scopedServices.GetRequiredService<UserManager<AppUser>>();
                    var roleManager = scopedServices.GetRequiredService<RoleManager<IdentityRole>>();

                    // Ensure the database is created.
                    db.Database.EnsureCreated();

                    if (!db.Database.IsInMemory())
                    {
                        throw new Exception("Database is not in-memory");
                    }

                    try
                    {
                        // Seed the database with test data.
                        var adminRole = new IdentityRole();
                        adminRole.Name = UserRoles.Admin;
                        var roleResult =  roleManager.CreateAsync(adminRole);

                        var clientRole = new IdentityRole();
                        clientRole.Name = UserRoles.Client;
                        roleResult = roleManager.CreateAsync(clientRole);


                        var mazen = new AppUser
                        {
                            UserName = "mazen506@gmail.com",
                            Email = "mazen506@gmail.com",
                            FirstName = "Mazen",
                            LastName = "Mustafa"
                        };

                        var userResult = userManager.CreateAsync(mazen, "Test@1234").Result;
                        if (!userResult.Succeeded)
                        {
                            throw new Exception("Unable to create alice:\r\n" + string.Join("\r\n", userResult.Errors.Select(error => $"{error.Code}: {error.Description}")));
                        }
                        roleResult = userManager.AddToRoleAsync(mazen, UserRoles.Admin);

                        var emailConfirmationToken = userManager.GenerateEmailConfirmationTokenAsync(mazen).Result;
                        userResult = userManager.ConfirmEmailAsync(mazen, emailConfirmationToken).Result;
                        if (!userResult.Succeeded)
                        {
                            throw new Exception("Unable to verify alices email address:\r\n" + string.Join("\r\n", userResult.Errors.Select(error => $"{error.Code}: {error.Description}")));
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                            "database with test messages. Error: {Message}", ex.Message);
                    }
                }
            });
        }
    }
}