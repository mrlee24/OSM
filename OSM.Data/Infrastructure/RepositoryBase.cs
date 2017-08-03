using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace OSM.Data.Infrastructure
{
    public class RepositoryBase : IRepositoryBase
    {
        AppsDbContext _context;
        public RepositoryBase(IDbFactory dbFactory)
        {
            _context = dbFactory.GetDataContext;
        }
        public IQueryable<T> All<T>() where T : class
        {
            return _context.Set<T>().AsQueryable();
        }
        public virtual void Add<T>(T entity) where T : class
        {
            var newEntry = _context.Add(entity);
        }
        public virtual void Update<T>(T entity) where T : class
        {
            try
            {
                var entry = _context.Entry(entity);
                _context.Set<T>().Attach(entity);
                entry.State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual void Delete<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
        }
        public void Delete<T>(int id) where T : class
        {
            var entity = GetSingle<T>(id);

            if (entity == null) return;

            Delete(entity);
        }
        public void DeleteMulti<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            IEnumerable<T> entities = _context.Set<T>().Where(predicate);

            foreach (var entity in entities)
            {
                _context.Entry<T>(entity).State = EntityState.Deleted;
            }
        }
        public IEnumerable<T> AllIncluding<T>(params Expression<Func<T, object>>[] includeProperties) where T : class
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.AsEnumerable();
        }
        public IEnumerable<T> GetAll<T>() where T : class
        {
            return _context.Set<T>().AsEnumerable();
        }
        public IEnumerable<T> GetAll<T>(string[] includes = null) where T : class
        {
            throw new NotImplementedException();
        }
        public T GetSingle<T>(int id) where T : class
        {
            // kiem tra ham EntityBase
            //  return _context.Set<T>().FirstOrDefault(x => x.Id == id);
            return _context.Set<T>().Find(id);
        }
        //  200
        public T GetSingle<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }
        //  200
        public T GetSingle<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties) where T : class
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate).FirstOrDefault();
        }
        //200
        public IEnumerable<T> GetMulti<T>(Expression<Func<T, bool>> predicate, string[] includes = null) where T : class
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = _context.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.Where<T>(predicate).AsQueryable<T>();
            }

            return _context.Set<T>().Where<T>(predicate).AsQueryable<T>();
        }
        public IEnumerable<T> GetMultiPaging<T>(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50, string[] includes = null) where T : class
        {
            int skipCount = index * size;
            var _resetSet = filter != null ? _context.Set<T>
            ().Where<T>
            (filter).AsQueryable() : _context.Set<T>().AsQueryable();
            _resetSet = skipCount == 0 ? _resetSet.Take(size) :
            _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }
        public int Count<T>() where T : class
        {
            return _context.Set<T>().Count();
        }
        public IEnumerable<T> FindBy<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _context.Set<T>().Where<T>(predicate).AsQueryable<T>();
        }
        public virtual void ExecuteProcedure(String procedureCommand, params SqlParameter[] sqlParams)
        {
            _context.Database.ExecuteSqlCommand(procedureCommand, sqlParams);
        }
        public bool Contains<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _context.Set<T>().Count<T>(predicate) > 0;
        }
        /*
        public T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        //Phan trang
        public virtual IEnumerable<T> Filter<T>(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50)
        where T : class
        {
            int skipCount = index * size;
            var _resetSet = filter != null ? _context.Set<T>
            ().Where<T>
            (filter).AsQueryable() : _context.Set<T>().AsQueryable();
            _resetSet = skipCount == 0 ? _resetSet.Take(size) :
            _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }
        public virtual void Add(T entity)
        {
            //EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            //_context.Set<T>().Add(entity);

            _context.Set<T>().Add(entity);
            //_context.Add(entity);
            //if (entity == null)
            //{
            //    throw new ArgumentNullException("entity");
            //}

            //EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            //if (dbEntityEntry.State != (EntityState)EntityState.Detached)
            //{
            //    dbEntityEntry.State = EntityState.Added;
            //}
            //else
            //{
            //    _context.Set<T>().Add(entity);
            //}
        }
        public virtual void AddRange(T entities)
        {
            _context.Set<T>().AddRange(entities);
        }
        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public virtual void DeleteRange(T entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
        public virtual void Update(T entity)
        {
            try
            {
                var entry = _context.Entry(entity);
                _context.Set<T>().Attach(entity);
                entry.State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual void Delete<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            var objects = Filter<T>(predicate);
            foreach (var obj in objects)
                _context.Set<T>().Remove(obj);
        }
        public bool Contains<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _context.Set<T>().Count<T>(predicate) > 0;
        }
        public virtual T Find<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _context.Set<T>().FirstOrDefault<T>(predicate);
        }
        public virtual void ExecuteProcedure(String procedureCommand,
       params SqlParameter[] sqlParams)
        {
            _context.Database.ExecuteSqlCommand(procedureCommand, sqlParams);
        }
    }*/
    }
}
