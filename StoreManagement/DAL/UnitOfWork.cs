using StoreManagement.Models;

namespace StoreManagement.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private StoreManagementEntities DbContext;
        private CustomerRepository _customers;
        private ProductRepository _products;
        private StoreRepository _stores;
        private SaleRepository _sales;

        public UnitOfWork()
        {
            DbContext = new StoreManagementEntities();            
        }

        public CustomerRepository CustRepo
        {
            get
            {
                if (_customers == null)
                {
                    _customers = new CustomerRepository(DbContext);
                }
                return _customers;
            }
        }

        public ProductRepository ProdRepo
        {
            get
            {
                if (_products == null)
                {
                    _products = new ProductRepository(DbContext);
                }
                return _products;
            }
        }

        public StoreRepository StoreRepo
        {
            get
            {
                if (_stores == null)
                {
                    _stores = new StoreRepository(DbContext);
                }
                return _stores;
            }
        }

        public SaleRepository SaleRepo
        {
            get
            {
                if (_sales == null)
                {
                    _sales = new SaleRepository(DbContext);
                }
                return _sales;
            }
        }        
        
        public int Save()
        {
            return DbContext.SaveChanges();          
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}