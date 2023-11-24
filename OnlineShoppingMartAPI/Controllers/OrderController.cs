using BusinessLogicLayer.Models;
using BusinessLogicLayer.OrderApi;
using BusinessLogicLayer.ProductApi;
using BusinessLogicLayer.UserApi;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using OnlineShoppingMartAPI.Filters;
using OnlineShoppingMartAPI.Utility;

namespace OnlineShoppingMartAPI.Controllers
{
    [OsmAuth]
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly ILogger<OrderController> _logger;

        private readonly IOrderLogicApi _orderApi;

        public OrderController(ILogger<OrderController> logger, IOrderLogicApi orderApi)
        {
            _logger = logger;
            _orderApi = orderApi;
        }

        [Route("SaveOrderDetail")]
        [HttpPost]
        public IActionResult AddOrderDetail([FromBody] OrderDetailsModel orderDetail)
        {
            IActionResult actionResult = null;
            try
            {
                var orderId = _orderApi.SaveOrderDetail(orderDetail);

                actionResult = Ok(orderId);

            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in AddOrderDetail : " + ex.Message);

                actionResult = StatusCode(500, "There is some technical issue. Please try after some time");
            }

            return actionResult;
        }
        [Route("SaveProductOrderRel")]
        [HttpPost]
        public IActionResult AddProductOrderRel([FromBody] ProductOrderRelModel pOrderModel) 
        { 
            IActionResult actionResult = null;
            try
            {
                var status = _orderApi.SaveProductOrderRel(pOrderModel);
                actionResult = Ok(status);

            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in SaveProductOrderRel : " + ex.Message);

                actionResult = StatusCode(500, "There is some technical issue. Please try after some time");
            }

            return actionResult;
        }
        [Route("SaveTransactionDetail")]
        [HttpPost]
        public IActionResult AddTransactionDetail([FromBody] TransactionDetailsModel transactionDetail)
        {
            IActionResult actionResult = null;
            try
            {
                var transactionId = _orderApi.SaveTransectionDetail(transactionDetail);

                actionResult = Ok(transactionId);

            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in SaveTransectionDetail : " + ex.Message);

                actionResult = StatusCode(500, "There is some technical isue. Please try afer some time");
            }

            return actionResult;
        }
        [Route("SaveCheckoutDetail")]
        [HttpPost]
        public IActionResult AddCheckoutDetail([FromBody] CheckoutDetails checkoutDetail)
        {
            IActionResult actionResult = null;
            try
            {
                var orderStatus = _orderApi.SaveOrderDetails(checkoutDetail);

                actionResult = Ok(orderStatus);

            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in SaveCheckoutDetail : " + ex.Message);

                actionResult = StatusCode(500, "There is some technical issue. Please try after some time");
            }

            return actionResult;
        }
        [Route("UpdateTransectionDetail")]
        [HttpPost]
        public IActionResult UpdateTransectionDetail([FromBody] TransactionDetailsModel transectionDetail)
        {
            IActionResult actionResult = null;
            try
            {
                var transectionId = _orderApi.UpdateTransectionDetail(transectionDetail);

                actionResult = Ok(transectionId);

            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in UpdateTransectionDetail : " + ex.Message);

                actionResult = StatusCode(500, "There is some technical issue. Please try after some time");
            }

            return actionResult;
        }

        [Route("UpdateOrderStatus")]
        [HttpGet]
        public IActionResult UpdateOrderStatus(long orderId, string orderStatus)
        {
            IActionResult actionResult = null;
            try
            {
                var status = _orderApi.UpdateOrderStatus(orderId, orderStatus);

                actionResult = Ok(status);

            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in UpdateOrderStatus : " + ex.Message);

                actionResult = StatusCode(500, "There is some technical issue. Please try after some time");
            }

            return actionResult;
        }


    }

}
