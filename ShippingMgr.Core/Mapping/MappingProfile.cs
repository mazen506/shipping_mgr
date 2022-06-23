using AutoMapper;
using AutoMapper.EquivalencyExpression;
using ShippingMgr.Core.Models;
using ShippingMgr.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingMgr.Core.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Currency, CurrencyVM>().ReverseMap();
            CreateMap<Customer, CustomerVM>().ReverseMap();
            CreateMap<Order, OrderVM>()
                        //.ForMember(dest=> dest.Customer,opt=> opt.MapFrom(src=> src.Customer))
                        //.ForPath(dest => dest.Customer.Name, opt => opt.MapFrom(src => src.Customer.Name))
                        //.EqualityComparison((src, dest) => src.Id == dest.Id)
                        .ReverseMap();
                        //.ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items)).ReverseMap();
                        //.EqualityComparison((src, dest) => src.Id == dest.Id);
            //CreateMap<OrderVM, Order>();
            CreateMap<Item, ItemVM>().ReverseMap();
            CreateMap<OrderItem, OrderItemVM>().ReverseMap();
        }
    }
}
