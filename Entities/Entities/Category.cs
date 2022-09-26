using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Category : ModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ECategoryStatus Status { get; set; }
        public byte[] bytesImage { get; set; }
    }

    public enum ECategoryStatus
    {
        Invativo = 0,
        Ativo = 10
    }
}
