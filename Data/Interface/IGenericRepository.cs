using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PainAssessment.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById<T1, TId>(TId id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
