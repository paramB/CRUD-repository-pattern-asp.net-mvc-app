using System.Collections.Generic;
using System.Linq;
using OnBoardingTaskV3.Models;

namespace OnBoardingTaskV3.DAL
{
    public class CustomerRepository : Repository<Customer>
    {
        public CustomerRepository(StoreManagementEntities Dbcontext) : base(Dbcontext)
        {
        }

        public override IEnumerable<Customer> GetAllRecords()
        {
            var list = Dbcontext.Customers.AsEnumerable().Select(x => new Customer
            {
                Id = x.Id,
                Name = x.Name,
                Age = x.Age,
                Address = x.Address,
            });
            return list;
        }
    }
}