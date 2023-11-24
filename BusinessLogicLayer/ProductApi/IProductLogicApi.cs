using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogicLayer.ProductApi
{
    public interface IProductLogicApi
    {
        List<ProductCategories> GetAllProductCategories();
        List<ProductSearch> Search(string searchText,  string productCategoryId);
        ProductDetail GetProductDetail(long productId);
        bool UpdateProductCount(long productId, int quantity);
        string AddProductCategory(ProductCategoryUpdateRequest productCategoryUpdateRequest);
        string UpdateProductCategory(ProductCategoryUpdateRequest productCategoryUpdateRequest);
    }
}
