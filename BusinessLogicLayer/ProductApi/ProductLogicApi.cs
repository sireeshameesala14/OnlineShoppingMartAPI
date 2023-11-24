using BusinessLogicLayer.Models;
using DataAccessLayer;
using DataAccessLayer.DBContext;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.ProductApi
{
    public class ProductLogicApi : IProductLogicApi
    {
        private readonly OSMDBContext _dbContext;

        public ProductLogicApi(OSMDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ProductCategories> GetAllProductCategories()
        {
            List<ProductCategories> productCatageories = new List<ProductCategories>();
            var pCatageory = _dbContext.ProductCategory.Where(m => m.IsActive == true).ToList();
            foreach (var productCategory in pCatageory)
            {
                ProductCategories pCataInfo = new ProductCategories();
                pCataInfo.CategoryId = productCategory.CategoryId;
                pCataInfo.CategoryName = productCategory.CategoryName;
                productCatageories.Add(pCataInfo);
            }
            return productCatageories;
        }

        public List<ProductSearch> Search(string searchText, string productCatageoryId)
        {
            searchText = searchText.ToUpper();

            List<ProductSearch> products = new List<ProductSearch>();

            List<ProductDetails> productDetails = new List<ProductDetails>();

            if (!string.IsNullOrEmpty(searchText) && !string.IsNullOrEmpty(productCatageoryId) && productCatageoryId != "0")
            {
                productDetails = _dbContext.ProductDetails.Where(m => m.ProductName.ToUpper().Contains(searchText) && m.CategoryId == Convert.ToInt32(productCatageoryId)).ToList();
            }
            else
            {
                if (!string.IsNullOrEmpty(searchText))
                {
                    productDetails = _dbContext.ProductDetails.Where(m => m.ProductName.ToUpper().Contains(searchText)).ToList();
                }
                else if (productCatageoryId != "0")
                {
                    productDetails = _dbContext.ProductDetails.Where(m => m.CategoryId == Convert.ToInt32(productCatageoryId)).ToList();

                }
                else
                {
                    productDetails = _dbContext.ProductDetails.ToList();
                }
            }

            foreach (var prod in productDetails)
            {
                ProductSearch pSearch = new ProductSearch();
                pSearch.ProductId = prod.ProductId;
                pSearch.ProductName = prod.ProductName;
                pSearch.ProductColor = prod.ProductColor;
                pSearch.Brand = prod.Brand;
                pSearch.ProductPrice = prod.ProductPrice;
                pSearch.ProductType = _dbContext.ProductType.Where(m => m.ProductTypeId == prod.ProductTypeId).FirstOrDefault().ProductTypeName;

                var images = _dbContext.ProductImages.Where(m => m.ProductId == prod.ProductId).ToList();

                pSearch.Images = new List<ProductImagesModel>();
                foreach (var image in images)
                {
                    ProductImagesModel img = new ProductImagesModel();
                    img.ImageSize = image.ImageSize;
                    img.ImageName = image.ImageName;
                    pSearch.Images.Add(img);
                }

                var reviewes = _dbContext.ProductReview.Where(m => m.ProductId == prod.ProductId).ToList();

                pSearch.ProductReviews = new List<ProductReviewModel>();
                foreach (var review in reviewes)
                {
                    ProductReviewModel rwes = new ProductReviewModel();
                    rwes.UserId = review.UserId;
                    rwes.Rating = review.Rating;
                    rwes.ReviewComments = review.ReviewComments;
                    pSearch.ProductReviews.Add(rwes);
                }
                products.Add(pSearch);


            }

            return products;
        }
        public ProductDetail GetProductDetail(long productId)
        {
            var prod = _dbContext.ProductDetails.Where(m => m.ProductId == productId).FirstOrDefault();
            ProductDetail product = new ProductDetail();
            product.ProductId = prod.ProductId;
            product.ProductName = prod.ProductName;
            product.ProductColor = prod.ProductColor.Split(',').ToList();
            product.Brand = prod.Brand;
            product.ProductPrice = prod.ProductPrice;
            product.ProductType = _dbContext.ProductType.Where(m => m.ProductTypeId == prod.ProductTypeId).FirstOrDefault().ProductTypeName;
            product.ProductCategory = _dbContext.ProductCategory.Where(m => m.CategoryId == prod.CategoryId).FirstOrDefault().CategoryName;
            product.ProductCount = prod.ProductCount;
            product.ProductSpecification = prod.ProductSpecification;
            product.IsActive = prod.IsActive;
            product.ShippingCharge = prod.ShippingCharge;
            product.Tax = prod.Tax;
            var images = _dbContext.ProductImages.Where(m => m.ProductId == prod.ProductId).ToList();

            product.Images = new List<ProductImagesModel>();
            foreach (var image in images)
            {
                ProductImagesModel img = new ProductImagesModel();
                img.ImageSize = image.ImageSize;
                img.ImageName = image.ImageName;
                product.Images.Add(img);
            }

            var reviewes = _dbContext.ProductReview.Where(m => m.ProductId == prod.ProductId).ToList();

            product.ProductReviews = new List<ProductReviewModel>();
            foreach (var review in reviewes)
            {
                ProductReviewModel rwes = new ProductReviewModel();
                rwes.UserId = review.UserId;
                rwes.Rating = review.Rating;
                rwes.ReviewComments = review.ReviewComments;
                product.ProductReviews.Add(rwes);
            }
            return product;
        }
        public bool UpdateProductCount(long productId, int quantity)
       {
           bool status = false;

            if (productId != 0)
            {
                var result = _dbContext.ProductDetails.Where(m => m.ProductId == productId).FirstOrDefault();
                if (result != null)
                {
                    result.ProductCount = quantity;
                    _dbContext.Entry(result).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    status = true;
                }
            }
            return status;
        }
            public string UpdateProductCategory(ProductCategoryUpdateRequest productCategoryUpdateRequest)
            {
                var pCatageory = _dbContext.ProductCategory.FirstOrDefault(m => m.IsActive == true && m.CategoryId == productCategoryUpdateRequest.CategoryId);
                if (pCatageory != null)
                {
                    if (!string.IsNullOrEmpty(productCategoryUpdateRequest.CategoryName))
                        pCatageory.CategoryName = productCategoryUpdateRequest.CategoryName;
                    pCatageory.IsActive = productCategoryUpdateRequest.IsActive;
                    _dbContext.ProductCategory.Update(pCatageory);
                    _dbContext.SaveChanges();
                }
                return "Updated Successfully";
            }

            public string AddProductCategory(ProductCategoryUpdateRequest productCategoryUpdateRequest)
            {

                var pCatageory = new ProductCategory()
                {
                    CategoryName = productCategoryUpdateRequest.CategoryName,
                    IsActive = true,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                    CreatedBy = 0,
                    UpdatedBy = 0
                };
                _dbContext.ProductCategory.Add(pCatageory);
                _dbContext.SaveChanges();
                return "Added Successfully";
            }

        }
    }

    
