using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    public class TransactionDetailsModel
    {
        public long TransactionId { get; set; }

        public long OrderId { get; set; }

        public decimal Amount { get; set; }
        public string GatewayName { get; set; }
        public string PaymentId { get; set; }
        public string PayerId { get; set; }
        public string RefrenceId { get; set; }
        public bool IsPaymentSuccessful { get; set; }

        public long UserId { get; set; }
    }
}