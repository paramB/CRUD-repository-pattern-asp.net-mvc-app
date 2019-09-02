using System;

namespace StoreManagement.DAL
{
    public interface IUnitOfWork : IDisposable
    {                    
        CustomerRepository CustRepo { get; }
        ProductRepository ProdRepo { get; }
        StoreRepository StoreRepo { get; }
        SaleRepository SaleRepo { get; }        
        int Save();
    }
}
