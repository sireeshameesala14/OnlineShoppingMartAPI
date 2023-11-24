using BusinessLogicLayer.Models;
using BusinessLogicLayer.ProductApi;
using BusinessLogicLayer.UserApi;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using OnlineShoppingMartAPI.Filters;
using OnlineShoppingMartAPI.Utility;

namespace OnlineShoppingMartAPI.Controllers
{
    [OsmAuth]
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;

        private readonly IProductLogicApi _productApi;

        public ProductController(ILogger<UserController> logger, IProductLogicApi productApi)
        {
            _logger = logger;
            _productApi = productApi;
        }


        [HttpGet]
        [Route("GetAllProductCategories")]
        public IActionResult GetProductCategories()
        {
            IActionResult response = null;
            _logger.LogInformation("GetProductCategories is called.");
            try
            {
                var productCatageoryDetail = _productApi.GetAllProductCategories();
                response = Ok(productCatageoryDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetProductCatageories : " + ex.Message);
                response = StatusCode(500, "There is some issue with application.Please try after some time.");
            }
            return response;

        }
        [HttpGet]
        [Route("GetProductSearchResult")]
        public IActionResult GetProductSearchDetail(string searchText, string productCategoryId)
        {
            IActionResult response = null;
            _logger.LogInformation("GetProductSearchResult is called.");
            try
            {
                var productDetails = _productApi.Search(searchText, productCategoryId);
                response = Ok(productDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetProductSearchResult : " + ex.Message);
                response = StatusCode(500, "There is some issue with application.Please try after some time.");
            }
            return response;

        }
        [HttpGet]
        [Route("GetProductDetail")]
        public IActionResult GetProductInformation(long productId)
        {
            IActionResult response = null;
            _logger.LogInformation("GetProductInformation is called.");
            try
            {
                var productDetails = _productApi.GetProductDetail(productId);
                response = Ok(productDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetProductInformation : " + ex.Message);
                response = StatusCode(500, "There is some issue with application.Please try after some time.");
            }
            return response;

        }
        [HttpGet]
        [Route("UpdateProductQuantity")]
        public IActionResult UpdateProductCount(long productId, int quantity)
        {
            IActionResult response = null;
            _logger.LogInformation("UpdateProductQuantity is called.");
            try
            {
                var status = _productApi.UpdateProductCount(productId, quantity);
                response = Ok(status);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in UpdateProductQuantity : " + ex.Message);
                response = StatusCode(500, "There is some issue with application.Please try after some time.");
            }
            return response;
        }

            [HttpPost]
        [Route("UpdateProductCategory")]
        public IActionResult UpdateProductCategory(ProductCategoryUpdateRequest productCategoryUpdateRequest)
        {
            IActionResult response = null;
            _logger.LogInformation("UpdateProductCategories is called.");
            try
            {
                var retVal = _productApi.UpdateProductCategory(productCategoryUpdateRequest);
                response = Ok(retVal);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in UpdateProductCategory : " + ex.Message);
                response = StatusCode(500, "There is some issue with application.Please try after some time.");
            }
            return response;

        }

        [HttpPost]
        [Route("AddProductCategory")]
        public IActionResult AddProductCategory(ProductCategoryUpdateRequest productCategoryUpdateRequest)
        {
            IActionResult response = null;
            _logger.LogInformation("AddProductCategory is called.");
            try
            {
                var retVal = _productApi.AddProductCategory(productCategoryUpdateRequest);
                response = Ok(retVal);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in AddProductCategory : " + ex.Message);
                response = StatusCode(500, "There is some issue with application.Please try after some time.");
            }
            return response;

        }
    }
}