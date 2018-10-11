﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using TestC.Repositories.Interface;

namespace TestC.Repositories.Implements
{
    public abstract class Repository<T> : IRepository<T>
     where T : class
    {
        protected DbContext _entities;
        protected readonly IDbSet<T> _dbset;

        public Repository(DbContext context)
        {

            _entities = context;
            _dbset = context.Set<T>();
        }

        public virtual IQueryable<T> FindAll()
        {
            //  _entities.Configuration.AutoDetectChangesEnabled = false;
            var result = _dbset;

            //  _entities.Configuration.AutoDetectChangesEnabled = true;
            return result;
        }

        public async Task<List<T>> WhereAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            List<T> query = await _dbset.Where(predicate).ToListAsync();
            return query;
        }

        public IQueryable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            //   _entities.Configuration.AutoDetectChangesEnabled = false;
            IQueryable<T> query = _dbset.Where(predicate);
            // IQueryable<T> query = _dbset.AsNoTracking().Where(predicate);

            //  _entities.Configuration.AutoDetectChangesEnabled = true;
            return query;
        }

        public virtual T Add(T entity)
        {
            return _dbset.Add(entity);
        }

        public virtual T Delete(T entity)
        {
            return _dbset.Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return _dbset.Any(predicate);
        }

        public bool All(Expression<Func<T, bool>> predicate)
        {
            return _dbset.All(predicate);
        }
    }
}