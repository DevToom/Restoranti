using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities.VM
{
    /// <summary>
    /// VM de principio para enviar a Cozinha. 
    /// </summary>
    public class OrderResponseVM
    {
        public string OrderNumber { get; set; }
        public int TableNumber { get; set; }
        public int? UserId { get; set; }
        public EOrderType Type { get; set; }
        public EStatus Status { get; set; }
        public List<ProductVM> Products { get; set; }
        public bool hasObservation { get; set; }
        public decimal? Total { get; set; }
    }
}
