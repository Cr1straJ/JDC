using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDC.Common.Entities
{
    public class AbstractUser
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
