using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreManagement.Models;

namespace StoreManagement.DAL
{
    public class ProductRepository : Repository<Product>
    {
        public ProductRepository(StoreManagementEntities Dbcontext) : base(Dbcontext)
        {
        }

        public override IEnumerable<Product> GetAllRecords()
        {
            var list = Dbcontext.Products.AsEnumerable().Select(x => new Product
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price
            });
            return list;
        }
    }
}