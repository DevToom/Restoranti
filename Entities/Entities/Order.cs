using Entities.Entities.VM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [Table("Orders")]
    public class Order : ModelBase
    {
        [Key]
        public int Id { get; set; }

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
        public int? UserId { get; set; }

        /// <summary>
        /// Tipo de pedido
        /// </summary>
        public EOrderType Type { get; set; }

        /// <summary>
        /// Status do Pedido
        /// </summary>
        public EStatus Status { get; set; }

        /// <summary>
        /// Id do Produto
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Nome do Produto
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Quantidade escolhida
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Valor do Produto
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Tem observação?
        /// </summary>
        public bool hasObservation { get; set; }

        /// <summary>
        /// Caso tenha observação descrever aqui, por produto
        /// </summary>
        public string Observation { get; set; }

        /// <summary>
        /// Valor total do pedido
        /// </summary>
        public decimal? Total { get; set; }

        
    }
    

    public enum EStatus
    {
        A_PREPARAR = 0,
        PREPARANDO = 1,
        PREPARADO = 2
    }

    public enum EOrderType
    {
        ALaCarte = 0,
        Rodizio = 10
    }




}
