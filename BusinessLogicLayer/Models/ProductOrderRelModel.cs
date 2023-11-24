using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    public class ProductOrderRelModel
    {
        public long ProductId { get; set; }

        public int Quantity { get; set; }

        public long OrderId { get; set; }

    }
}