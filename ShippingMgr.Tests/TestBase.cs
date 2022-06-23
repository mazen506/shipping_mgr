using AutoMapper;
using AutoMapper.EquivalencyExpression;
using DotNetCore.EntityFrameworkCore;
using DotNetCore.IoC;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShippingMgr.Core.Application;
using ShippingMgr.Core.Application.Interfaces;
using ShippingMgr.Api;
using ShippingMgr.Core.Database.Context;
using ShippingMgr.Core.Mapping;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShippingMgr.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SimpleInjector;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace ShippingMgr.Tests
{
    public  class TestBase
    {
        private AppDataContext _context;
        private IMapper _mapper;
      

        public TestBase()
        {
            var services = new ServiceCollection();
            services.AddDbContext<AppDataContext>(options=> 
                    options.UseInMemoryDatabase("shipping-mgr")
            );

            var config = new MapperConfiguration( cfg =>
            {
                cfg.AddProfile<MappingProfile>();
                cfg.AddCollectionMappers();
                cfg.UseEntityFrameworkCoreModel<AppDataContext>(services);
            });

            //, typeof(AppDataContext).Assembly
            _mapper = config.CreateMapper();


            if (_context == null)
            {
                var _options = new DbContextOptionsBuilder<AppDataContext>()
                    .UseInMemoryDatabase("Tesing")
                    .Options;

                _context = new AppDataContext(_options);
            }

            SeedData();
        }

        public void SeedData()
        {
            //Customers
            _context.Customers.AddRange(
                new Customer()
                {
                   Name="Mohammed",
                   Address="China",
                   Phone="008611121212"
                },
                 new Customer()
                 {
                     Name = "Essam",
                     Address = "Djibouti",
                     Phone = "008611121212"
                 },
                  new Customer()
                  {
                      Name = "AbdulNaser",
                      Address = "China",
                      Phone = "008611121212"
                  }
            );


            
            //
            _context.SaveChanges();
        }

        public AppDataContext GetDbContext()
        {
            return _context;
        }

        public IMapper GetMapper()
        {
            return _mapper;
        }



    }
}
