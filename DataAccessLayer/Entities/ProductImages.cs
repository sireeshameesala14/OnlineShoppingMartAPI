using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccessLayer.Entities
{
    public class ProductImages
    {
        [Key]
        public long ImageId { get; set; }
        public long ProductId { get; set; } 
        public string ImageName { get; set; }
        public string ImageSize { get; set; }
    }
}

