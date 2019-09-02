using System;

namespace StoreManagement.Models
{
    public class ProductSoldDTO
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public int ProductId { get; set; }
        public string Customer { get; set; }
        public int CustomerId { get; set; }
        public string Store { get; set; }
        public int StoreId { get; set; }
        public DateTime? DateSold { get; set; }
    }
}