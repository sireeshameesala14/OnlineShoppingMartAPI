using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class TransactionDetails
    {
        [Key]
        public long TransactionId { get; set; }

        public long OrderId { get; set; }

        public decimal Amount { get; set; }
        public string GatewayName { get; set; }
        public string PaymentId { get; set; }
        public string PayerId { get; set; }
        public string RefrenceId { get; set; }
        public bool IsPaymentSuccessful { get; set; }
        public long CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public long UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }


    }
}