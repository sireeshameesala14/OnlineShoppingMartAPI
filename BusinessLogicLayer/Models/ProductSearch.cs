using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    public class ProductSearch
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }

        public string ProductType { get; set; }
        public decimal ProductPrice { get; set; }

        public string Brand { get; set; }

        public string ProductColor { get; set; }

        public List<ProductImagesModel> Images { get; set; }

        public List<ProductReviewModel> ProductReviews { get; set; }
    }
}
