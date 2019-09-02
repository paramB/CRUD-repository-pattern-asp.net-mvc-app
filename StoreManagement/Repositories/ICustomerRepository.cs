using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnBoardingTaskV3.Models;

namespace OnBoardingTaskV3.Repositories
{
    public interface ICustomerRepository : IDisposable
    {
        IEnumerable<Customer> GetCustomerList();
        //Customer GetStudentByID(int studentId);
        void AddCustomer(Customer cust);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int Id);
        void Save();
    }    
}


    
