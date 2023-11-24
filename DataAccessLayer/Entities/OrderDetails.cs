using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class OrderDetails
    {
        [Key]
        public long OrderId { get; set; }

        public long UserId { get; set; }

        public decimal ProductAmount { get; set; }
        public decimal ShippingCharge { get; set; }
        public decimal Tax { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMode { get; set; }
        public string OrderStatus { get; set; }
        public string ShippingStatus { get; set; }

        public bool IsActive { get; set; }

        public string OrderNotes { get; set; }
        public bool IsTermsAccepted { get; set; }

        public long AddressId { get; set; }

        public long CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public long UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }


    }
}