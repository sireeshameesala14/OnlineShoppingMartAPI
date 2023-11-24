using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class ProductType
    {
        [Key]
        public long ProductTypeId { get; set; }

        public long CategoryId { get; set; }

        public string ProductTypeName { get; set; }

        public bool IsActive { get; set; }

        public long CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public long UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

    }
}
