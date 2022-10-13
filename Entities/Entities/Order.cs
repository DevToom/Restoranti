using Entities.Entities.VM;
using System;
using System.Collections.Generic;
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
        public int OrderId { get; set; }
        public string OrderNumber { get; set; } = GenerateOrderNumber();
        public int TableNumber { get; set; }
        public int UserId { get; set; }
        public EOrderType Type { get; set; }

        //[JsonIgnore]
        //public List<Itens> Itens { get; set; }

        public decimal? Total { get; set; }
        public bool HasObservation { get; set; }
        public string? Observation { get; set; }


        /// <summary>
        /// Gerar número do pedido
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateOrderNumber(int length = 10)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }

    public enum EOrderType
    {
        LaCarte = 0,
        Rodizio = 10
    }




}
