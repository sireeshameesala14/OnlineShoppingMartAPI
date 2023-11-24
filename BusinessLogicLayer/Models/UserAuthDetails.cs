using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    public class UserAuthDetails
    {
        public long UserId { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserType { get; set; }
    }
}
