using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [Table("Products")]
    public class Product : ModelBase
    {
        /// <summary>
        /// Id do Produto
        /// </summary>
        [Key]
        public int ProductId { get; set; }
        /// <summary>
        /// Id da categoria vinculado ao produto
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// Nome do Produto
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Descrição do produto
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Preço do produto no sistema À La Carte
        /// </summary>
        public decimal PriceALaCarte { get; set; }
        /// <summary>
        /// Preço do produto no sistema Rodizio
        /// </summary>
        public decimal PriceRodizio { get; set; }
        /// <summary>
        /// Status do Produto
        /// </summary>
        public EProductStatus Status { get; set; }
        /// <summary>
        /// Imagem da categoria
        /// </summary>
        public byte[]? ImageContent { get; set; }
    }

    /// <summary>
    /// Enum para status do produto
    /// </summary>
    public enum EProductStatus
    {
        Ativo = 0,
        Inativo = 1
    }

}
