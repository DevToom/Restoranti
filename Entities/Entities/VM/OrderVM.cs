using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities.VM
{
    public class OrderVM
    {
        /// <summary>
        /// Número do pedido
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// Número da mesa
        /// </summary>
        public int TableNumber { get; set; }

        /// <summary>
        /// Id do cliente que realizou o pedido, caso tenha.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Tipo de pedido
        /// </summary>
        public EOrderType Type { get; set; }

        /// <summary>
        /// Status do Pedido
        /// </summary>
        public EStatus Status { get; set; }

        /// <summary>
        /// Valor total do pedido
        /// </summary>
        public decimal? Total { get; set; }

        /// <summary>
        /// Produtos VM
        /// </summary>
        public List<ProductVM> Product { get; set; }

        /// <summary>
        /// Número da conta que está salvo com o front.
        /// </summary>
        public int AccountOrderId { get; set; }

        /// <summary>
        /// Informação que o front vai informar para dizer se aquele é o primeiro pedido do cliente
        /// </summary>
        public bool isFirst { get; set; }
    }
}
