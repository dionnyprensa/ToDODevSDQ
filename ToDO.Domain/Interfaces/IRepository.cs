using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ToDO.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get();

        T GetById(int id);

        void Insert(T entity);

        void Delete(T entity);

        void Update(T entity);

        IEnumerable<T> GetMany(Func<T, bool> where);

        IQueryable<T> GetManyQueryable(Func<T, bool> where);

        T Get(Func<T, bool> where);

        void DeleteHard(int id);

        void DeleteSoft(int id);

        void DeleteHard(Func<T, bool> where);

        void DeleteSoft(Func<T, bool> where);

        void Delete(Func<T, bool> where);

        IEnumerable<T> GetAll();

        IQueryable<T> GetWithInclude(Expression<Func<T, bool>> predicate, params string[] include);
    }
}