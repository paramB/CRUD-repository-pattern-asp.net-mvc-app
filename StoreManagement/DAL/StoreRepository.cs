using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreManagement.Models;

namespace StoreManagement.DAL
{
    public class StoreRepository : Repository<Store>
    {
        public StoreRepository(StoreManagementEntities Dbcontext) : base(Dbcontext)
        {
        }

        public override IEnumerable<Store> GetAllRecords()
        {
            var list = Dbcontext.Stores.AsEnumerable().Select(x => new Store
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address
            });
            return list;
        }
    }
}