using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    public class OrderDetailsModel
    {
        public long OrderId { get; set; }

        public long UserId { get; set; }
        public decimal ProductAmount { get; set; }
        public decimal ShippingCharge { get; set; }
        public decimal Tax { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMode { get; set; }
        public string OrderStatus { get; set; }
        public bool IsActive { get; set; }

        public string OrderNotes { get; set; }
        public bool IsTermsAccepted { get; set; }

        public long AddressId { get; set; }
    }
}