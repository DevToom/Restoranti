using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class UserResponse
    {
        public bool HasError { get; set; }
        public string? Message { get; set; }

        public UserInternal? User { get; set; }
    }
}
