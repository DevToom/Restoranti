using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [Table("User")]
    public class User
    {
        public int Id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
