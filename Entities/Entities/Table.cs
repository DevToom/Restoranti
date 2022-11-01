using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [Table("Tables")]
    public class Table : ModelBase
    {
        [Key]
        public int Id { get; set; }
        public int TableNumber { get; set; }
        public string TableStatus { get; set; }
        public bool IsActive { get; set; }
    }
}
