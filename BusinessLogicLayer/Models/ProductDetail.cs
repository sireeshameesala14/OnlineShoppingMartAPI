using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    public class ProductDetail
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }

        public string ProductSpecification { get; set; }

        public long ProductCount { get; set; }

        public string Brand { get; set; }
        public decimal ShippingCharge { get; set; }

        public decimal Tax { get; set; }

        public decimal ProductPrice { get; set; }

        public string ProductType { get; set; }

        public string ProductCategory { get; set; }

        public List<string> ProductColor { get; set; }

        public string SelectedProductColor { get; set; }

        public string SelectedProductQuantity { get; set; }

        public bool IsActive { get; set; }
        public List<ProductImagesModel> Images { get; set; }

        public List<ProductReviewModel> ProductReviews { get; set; }
    }
}
