using AutoMapper;
using DotNetCore.Results;
using Microsoft.EntityFrameworkCore;
using ShippingMgr.Core.Application.Interfaces;
using ShippingMgr.Core.Database.Context;
using ShippingMgr.Core.Models;
using ShippingMgr.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShippingMgr.Core.Application
{
    public sealed class OrderService: EntityService<Order, OrderVM>, IOrderService
    {
        private readonly AppDataContext context;
        private readonly IMapper mapper;
        public OrderService
    (
        AppDataContext _context,
        IMapper _mapper
    ):base (_context, _mapper)
        {
            context = _context;
            mapper = _mapper;
        }

        public async Task<IEnumerable<OrderVM>> ListAsync()
        {
            var result = await this.ListAsync(null, null, x=> x.Items, x => x.Customer);
            return result;
        }
    }
}
