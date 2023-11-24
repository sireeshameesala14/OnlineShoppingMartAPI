using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class ProductOrderRel
    {
       
        public long ProductId { get; set; }

        public int Quantity { get; set; }
       
        public long OrderId { get; set; }

    }
}