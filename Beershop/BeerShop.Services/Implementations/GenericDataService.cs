﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using BeerShop.Data;
using BeerShop.Services.Contracts;


namespace BeerShop.Services.Implementations
{
    public abstract class GenericDataService<T> : IGenericDataService<T> where T : class
    {
        protected DbSet<T> _dbSet;

        protected GenericDataService(BeerShopDbContext dbContext)
        {
            this._dbSet = dbContext.Set<T>();
        }

        public virtual IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new BeerShopDbContext(new DbContextOptions<BeerShopDbContext>()))
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                list = dbQuery
                    .AsNoTracking()
                    .ToList<T>();
            }
            return list;
        }
        public virtual IList<T> GetList(Func<T, bool> where,
            params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new BeerShopDbContext(new DbContextOptions<BeerShopDbContext>()))
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                list = dbQuery
                    .AsNoTracking()
                    .AsEnumerable()
                    .Where(where)
                    .ToList<T>();
            }
            return list;
        }

        public virtual T GetSingle(Func<T, bool> where,
            params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;
            using (var context = new BeerShopDbContext(new DbContextOptions<BeerShopDbContext>()))
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                item = dbQuery
                    .AsNoTracking() //Don't track any changes for the selected item
                    .FirstOrDefault(where); //Apply where clause
            }
            return item;
        }

        public virtual void Add(params T[] items)
        {
            using (var context = new BeerShopDbContext(new DbContextOptions<BeerShopDbContext>()))
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = EntityState.Added;
                }
                context.SaveChanges();
            }
        }

        public virtual void Update(params T[] items)
        {
            using (var context = new BeerShopDbContext(new DbContextOptions<BeerShopDbContext>()))
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = EntityState.Modified;
                }
                context.SaveChanges();
            }
        }

        public virtual void Remove(params T[] items)
        {
            using (var context = new BeerShopDbContext(new DbContextOptions<BeerShopDbContext>()))
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = EntityState.Deleted;
                }
                context.SaveChanges();
            }
        }

        public bool Any(Func<T, bool> where,
            params Expression<Func<T, object>>[] navigationProperties)
        {
            using (var context = new BeerShopDbContext(new DbContextOptions<BeerShopDbContext>()))
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                return dbQuery
                    .AsNoTracking() //Don't track any changes for the selected item
                    .Any(where); //Apply where clause
            }
        }

        public bool Any()
        {
            using (var context = new BeerShopDbContext(new DbContextOptions<BeerShopDbContext>()))
            {
                IQueryable<T> dbQuery = context.Set<T>();

                return dbQuery
                    .AsNoTracking() //Don't track any changes for the selected item
                    .Any();
            }
        }

//        public IIncludableJoin<T, TProperty> Join<TProperty>(Expression<Func<T, TProperty>> navigationProperty)
//        {
//            return ((IQueryable<T>)this._dbSet).Join(navigationProperty);
//        }
    }
}