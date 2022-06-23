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
using System.Text;
using System.Threading.Tasks;

namespace ShippingMgr.Core.Application
{
    public sealed class ItemService: EntityService<Item, ItemVM>,IItemService
    {
        private readonly AppDataContext context;
        private readonly IMapper mapper;
        public ItemService
    (
        AppDataContext _context,
        IMapper _mapper
    ):base(_context, _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
    }
}
