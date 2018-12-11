using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using OnBoardingTaskV3.Models;

namespace OnBoardingTaskV3.Repositories
{
    public class CustomerRepository : ICustomerRepository, IDisposable
    {
        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}
        private StoreManagementEntities DbContext;

        public CustomerRepository(StoreManagementEntities DbContext)
        {
            this.DbContext = DbContext;
        }

        public IEnumerable<Customer> GetCustomerList()
        {
            return DbContext.Customers.ToList();
        }

        public void AddCustomer(Customer cust)
        {
            DbContext.Customers.Add(cust);

        }

        public void UpdateCustomer(Customer customer)
        {           
            DbContext.Entry(customer).State = EntityState.Modified;
        }

        public void DeleteCustomer(int Id)
        {
            Customer cust = DbContext.Customers.Find(Id);
            DbContext.Customers.Remove(cust);
        }        

        public void Save()
        {
            DbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
