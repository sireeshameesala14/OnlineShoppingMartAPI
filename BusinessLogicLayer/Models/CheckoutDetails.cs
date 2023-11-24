using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    public class CheckoutDetails
    {
        public AddressDetailModel BillingAddress { get; set; }

        public AddressDetailModel ShippingAddress { get; set; }

        public bool IsShippingAnotherAddress { get; set; }

        public string OrderNotes { get; set; }
        public bool IsTermsAccepted { get; set; }

        public decimal TotalOrderPrice { get; set; }
        public List<ProductDetail> Products { get; set; }

        public decimal TotalProductAmount { get; set; }
        public decimal TotalShipping { get; set; }
        public decimal TotalTax { get; set; }

        public long UserId { get; set; }

        public string PaymentMode { get; set; }
    }
}