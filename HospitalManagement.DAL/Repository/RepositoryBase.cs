using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.DAL.Repository
{
    public class RepositoryBase<T> where T : class
    {
        internal HospitalManagementMasterEntities objDataContext { get; set; }
        internal HospitalManagementClientEntities objDataClientContext { get; set; }
        internal DbSet<T> objDbSet { get; set; }


        internal RepositoryBase(HospitalManagementMasterEntities dbcontext)
        {
            this.objDataContext = dbcontext;
            this.objDbSet = dbcontext.Set<T>();
        }

        internal RepositoryBase(HospitalManagementClientEntities dbcontext)
        {
            this.objDataClientContext = dbcontext;
            this.objDbSet = dbcontext.Set<T>();
        }

        public virtual List<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? take = null, int? skip = 0,
            string includeProperties = "")
        {
            IQueryable<T> query = this.objDbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (take != null)
            {
                query = query.Take(Convert.ToInt32(take));
            }

            if (skip != null)
            {
                query = query.Skip(Convert.ToInt32(skip));
            }

            return query.ToList();
        }

        public virtual void Add(T entity)
        {
            this.objDbSet.Add(entity);
        }
        public virtual void Delete(T entity)
        {
            this.objDbSet.Attach(entity);
            this.objDbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = this.objDbSet.Where<T>(where).AsEnumerable();
            foreach(T obj in objects)
            {
                this.objDbSet.Remove(obj);
            }
        }

        public virtual T GetById(long id)
        {
            return this.objDbSet.Find(id);
        }
        public virtual T GetById(string id)
        {
            return this.objDbSet.Find(id);
        }
        public virtual List<T> GetAll()
        {
            return this.objDbSet.ToList();
        }
        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return this.objDbSet.Where(where).ToList();
        }
        public T Get(Expression<Func<T, bool>> where)
        {
            return this.objDbSet.Where(where).FirstOrDefault();
        }
        public IEnumerable<T1> GetBy<T1>(Expression<Func<T, bool>> exp, Expression<Func<T, T1>> columns)
        {
            return this.objDbSet.Where<T>(exp).Select<T, T1>(columns);
        }
    }
}
