using DotNetCore.Results;
using ShippingMgr.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShippingMgr.Core.Application.Interfaces
{
    public interface IEntityService<T, TVM>
    {
        public Task<IResult> AddAsync(TVM model);
        public Task<IResult> DeleteAsync(long id);
        public Task<TVM> GetAsync(long id);
        public Task<IEnumerable<TVM>> ListAsync(
            Expression<Func<T, bool>> filter = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] include);
        public Task<IResult> UpdateAsync(TVM model);
    }
}
