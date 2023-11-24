using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.OrderApi
{
    public interface IOrderLogicApi
    {
        long SaveOrderDetail(OrderDetailsModel model);
        long SaveTransectionDetail(TransactionDetailsModel model);
        bool SaveProductOrderRel(ProductOrderRelModel model);
        CheckoutSaveResponse SaveOrderDetails(CheckoutDetails checkoutDetails);
        bool UpdateTransectionDetail(TransactionDetailsModel model);

        bool UpdateOrderStatus(long orderId, string orderStatus);

    }
}