using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using ToDO.Domain.Interfaces;
using ToDO.Infraestructure.Context;

namespace ToDO.Infraestructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        internal ToDoContext Context;
        internal DbSet<T> DbSet;

        public Repository(ToDoContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }

        public virtual IEnumerable<T> Get()
        {
            IQueryable<T> query = DbSet;
            return query.ToList();
        }

        public virtual T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public virtual void Insert(T entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
                DbSet.Attach(entity);
            DbSet.Remove(entity);
        }

        public virtual void Update(T entity)
        {
            DbSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual IEnumerable<T> GetMany(Func<T, bool> where)
        {
            return DbSet.Where(where).ToList();
        }

        public virtual IQueryable<T> GetManyQueryable(Func<T, bool> where)
        {
            return DbSet.Where(where).AsQueryable();
        }

        public T Get(Func<T, bool> where)
        {
            return DbSet.Where(where).FirstOrDefault();
        }

        public virtual void DeleteHard(int id)
        {
            var entity = DbSet.Find(id);
            Delete(entity);
        }

        public virtual void DeleteSoft(int id)
        {
            var entity = DbSet.Find(id);
            Update(entity);
        }

        public virtual void DeleteHard(Func<T, bool> where)
        {
            Delete(where);
        }

        public virtual void DeleteSoft(Func<T, bool> where)
        {
            var entities = DbSet.Where(where).AsQueryable();

            foreach (var entity in entities)
                Update(entity);
        }

        public void Delete(Func<T, bool> where)
        {
            var objects = DbSet.Where(where).AsQueryable();
            foreach (var obj in objects)
                DbSet.Remove(obj);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return DbSet.ToList();
        }

        public IQueryable<T> GetWithInclude(Expression<Func<T, bool>> predicate, params string[] include)
        {
            IQueryable<T> query = DbSet;
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query.Where(predicate);
        }
    }
}