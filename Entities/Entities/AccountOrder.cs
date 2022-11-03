using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [Table("AccountOrder")]
    public class AccountOrder : ModelBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TableNumber { get; set; }
        public int UserAppId { get; set; }
        public string StatusAccountOrder { get; set; }
        public decimal? ValorConta { get; set; }
    }
}
