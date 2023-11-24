using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class ProductDetails
    {
        [Key]
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductSpecification { get; set; }
        public long ProductCount { get; set; }
        public string Brand  { get; set; }
        public decimal ShippingCharge { get;set; }
        public decimal Tax { get; set; }
        public decimal ProductPrice { get; set; }
        public long ProductTypeId { get; set; }
        public long CategoryId { get; set; }
        public string ProductColor { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long UpdatedBy { get ; set; }
        public DateTime UpdatedOn { get; set;}

    }
}
