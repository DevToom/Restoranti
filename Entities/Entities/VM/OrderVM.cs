using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities.VM
{
    public class OrderVM
    {
        public int UserId { get; set; }
        public int TableNumber { get; set; }
        //public List<Itens> itens { get; set; }
        public decimal Total { get; set; }
        public bool hasObservation { get; set; }
        public string Observation { get; set; }
    }

    //public class Itens
    //{
    //    public int ProductId { get; set; }
    //    public int CategoryId { get; set; }
    //    public string Name { get; set; }
    //    public decimal Value { get; set; }
    //    public string Quantity { get; set; }
    //}

}
