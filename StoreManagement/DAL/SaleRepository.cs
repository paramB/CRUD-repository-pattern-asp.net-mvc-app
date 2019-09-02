using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreManagement.Models;
using System.Data.Entity;

namespace StoreManagement.DAL
{
    public class SaleRepository : Repository<ProductSoldDTO>
    {
        public SaleRepository(StoreManagementEntities Dbcontext) : base(Dbcontext)
        {
        }

        public override IEnumerable<ProductSoldDTO> GetAllRecords()
        {
            IEnumerable<ProductSoldDTO> query = Dbcontext.ProductSolds
                                                         .Include(s => s.Customer)
                                                         .Include(s => s.Product)
                                                         .Include(s => s.Store)
                                                         .AsEnumerable()
                                                         .Select(x => new ProductSoldDTO()
                                                         {
                                                                Id = x.Id,
                                                                Product = x.Product.Name,
                                                                ProductId = (int)x.ProductId,
                                                                Customer = x.Customer.Name,
                                                                CustomerId = (int)x.CustomerId,
                                                                Store = x.Store.Name,
                                                                StoreId = (int)x.StoreId,
                                                                DateSold = (DateTime)x.DateSold,
                                                         });
            return query;
        }        

        public ProductSold AddRecord(ProductSold productSold)
        {
            var newRecord = Dbcontext.ProductSolds.Add(productSold);
            return newRecord;
        }

        public void UpdateRecord(ProductSold pSold)
        {
            Dbcontext.Entry(pSold).State = EntityState.Modified;
        }

        public new ProductSold DeleteRecord(int Id)
        {
            ProductSold entity = Dbcontext.ProductSolds.Find(Id);
            return Dbcontext.ProductSolds.Remove(entity);
        }
    }
}