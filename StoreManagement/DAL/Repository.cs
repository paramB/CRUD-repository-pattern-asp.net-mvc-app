using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using StoreManagement.Models;

namespace StoreManagement.DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly StoreManagementEntities Dbcontext;
        private DbSet<T> dbSet;
        private readonly IUnitOfWork _unitOfWork;

        public Repository(StoreManagementEntities context)
        {
            Dbcontext = context;
            dbSet = Dbcontext.Set<T>();
        }

        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public virtual IEnumerable<T> GetAllRecords()
        {
            return dbSet.ToList();
        }

        public virtual T AddRecord(T entity)
        {
            var newRecord = dbSet.Add(entity);
            return newRecord;
        }

        public virtual void UpdateRecord(T entity)
        {   
            Dbcontext.Entry(entity).State = EntityState.Modified;           
        }

        public virtual T DeleteRecord(int Id)
        {
            T entity = dbSet.Find(Id);
            return dbSet.Remove(entity);
        }
        
    }
}