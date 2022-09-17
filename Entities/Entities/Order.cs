using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [Table("Order")]
    public class Order : ModelBase
    {
        public int OrderId { get; set; }
        public int TableNumber { get; set; } 
        public int UserId { get; set; }
        public List<Product> Itens { get; set; }
        public decimal? Total { get; set; }
        public bool HasObservation { get; set; }
        public string? Observation { get; set; }
    }
}
