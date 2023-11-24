using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class ProductReview
    {
        [Key]
        public long ProductReviewId { get; set; }
        public long ProductId { get; set; }
        public int Rating { get; set; }
        public string ReviewComments { get; set; }
        public long UserId { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get;set; }
        public DateTime CreatedOn { get; set;}
        public long UpdatedBy { get;set; }
        public DateTime UpdatedOn { get; set;}
    }
}
