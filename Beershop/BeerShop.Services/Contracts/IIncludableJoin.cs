using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeerShop.Services.Contracts
{
    public interface IIncludableJoin<out TEntity, out TProperty> : IQueryable<TEntity>
    {
    }
}
