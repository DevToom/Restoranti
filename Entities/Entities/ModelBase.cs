using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class ModelBase
    {
        public DateTime? CreationDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Está sendo iniciado sempre com 1, pois 1 vai ser código de criação via senha admnistrador
        /// </summary>
        public int? CreationUserId { get; set; } = 1;
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedUserId { get; set; }

    }
}
