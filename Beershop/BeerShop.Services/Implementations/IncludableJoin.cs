using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using BeerShop.Services.Contracts;
using Microsoft.EntityFrameworkCore.Query;

namespace BeerShop.Services.Implementations
{
    public class IncludableJoin<TEntity, TPreviousProperty> : IIncludableJoin<TEntity, TPreviousProperty>
    {
        private readonly IIncludableQueryable<TEntity, TPreviousProperty> query;

        public IncludableJoin(IIncludableQueryable<TEntity, TPreviousProperty> query)
        {
            this.query = query;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return this.query.GetEnumerator();
        }

        public Expression Expression => this.query.Expression;
        public Type ElementType => this.query.ElementType;
        public IQueryProvider Provider => this.query.Provider;

        internal IIncludableQueryable<TEntity, TPreviousProperty> GetQuery()
        {
            return this.query;
        }
    }
}
