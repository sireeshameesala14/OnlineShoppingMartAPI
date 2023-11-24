using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    public class ProductReviewModel
    {
        public int Rating { get; set; }

        public long UserId { get; set; }

        public string ReviewComments { get; set; }

    }
}
