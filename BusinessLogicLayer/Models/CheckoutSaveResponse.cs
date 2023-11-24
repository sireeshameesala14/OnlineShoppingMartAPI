using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    public class CheckoutSaveResponse
    {
        public long OrderId { get; set; }

        public long TransactionId { get; set; }

        public bool IsSuccess { get; set; }

        public string ShippingStatus { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentMode { get; set; }
        public bool IsPaymentSuccessful { get; set; }

        public decimal TotalOrderPrice { get; set; }


    }
}