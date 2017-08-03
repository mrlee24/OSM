using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace OSM.Data.Infrastructure
{
    public interface IRepositoryBase
    {
        IQueryable<T> All<T>() where T : class;
        void Add<T>(T entity) where T: class;
        IEnumerable<T> AllIncluding<T>(params Expression<Func<T, object>>[] includeProperties) where T : class;
        int Count<T>() where T : class;
        void Delete<T>(int id) where T : class;
        void Delete<T>(T entity) where T : class;
        void DeleteMulti<T>(Expression<Func<T, bool>> predicate) where T : class;
        IEnumerable<T> FindBy<T>(Expression<Func<T, bool>> predicate) where T : class;
        IEnumerable<T> GetAll<T>() where T : class;
        IEnumerable<T> GetAll<T>(string[] includes = null) where T : class;
        IEnumerable<T> GetMulti<T>(Expression<Func<T, bool>> predicate, string[] includes = null) where T : class;
        IEnumerable<T> GetMultiPaging<T>(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50, string[] includes = null) where T : class;
        T GetSingle<T>(Expression<Func<T, bool>> predicate) where T : class;
        T GetSingle<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties) where T : class;
        T GetSingle<T>(int id) where T : class;
        void Update<T>(T entity) where T : class;
        void ExecuteProcedure(String procedureCommand, params SqlParameter[] sqlParams);
        bool Contains<T>(Expression<Func<T, bool>> predicate) where T : class;
    }
}