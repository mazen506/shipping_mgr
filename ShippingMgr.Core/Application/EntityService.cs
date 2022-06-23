using AutoMapper;
using AutoMapper.EntityFrameworkCore;
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
    public class EntityService<T, TVM>: IEntityService <T, TVM> where T: BaseEntity
                                                                where TVM: BaseVM, new()
    {
        private readonly AppDataContext context;
        private readonly IMapper mapper;
        public EntityService(AppDataContext _context, IMapper _mapper)
        {
            this.context = _context;
            this.mapper = _mapper;
        }

        public async Task<IResult> AddAsync(TVM model)
        {
            var dbSet = context.Set<T>();
            var record =await dbSet.Persist(mapper).InsertOrUpdateAsync(model);
            await context.SaveChangesAsync();
            model.Id = record.Id;
            return model.Id.Success();
        }

        public async Task<IResult> DeleteAsync(long id)
        {
            await context.Set<T>().Persist(mapper).RemoveAsync(new TVM() { Id = id });
            await context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<TVM> GetAsync(long id)
        {
            var record = await context.FindAsync<T>(id);
            return mapper.Map<TVM>(record);
        }

        public async Task<IEnumerable<TVM>> ListAsync(Expression<Func<T, bool>> filter = null, 
                                                      Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
                                                      params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = context.Set<T>();
            foreach (Expression<Func<T, object>> include in includes)
                query = query.Include(include);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            var records = await query.ToListAsync();
            var vms = mapper.Map<List<TVM>>(records).AsEnumerable();
            return vms;
        }

        public async Task<IResult> UpdateAsync(TVM model)
        {
            var dbSet = context.Set<T>();
            var orgRecord = await dbSet.FindAsync(model.Id);
            if (orgRecord == null) //Does not exist
                return Result.Fail("Not exits!");
            await context.Set<T>().Persist(mapper).InsertOrUpdateAsync(model);
            await context.SaveChangesAsync();
            return Result.Success();
        }
    }
}
