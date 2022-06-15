
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ETicaret.DataAccessLayer.Abstrack
{
    public interface IRepository<T>
    {
        void Create(T entity);
        void  Update(T entity);
        void Delete(T entity);

        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        T GetByID(int id);
    }
}
