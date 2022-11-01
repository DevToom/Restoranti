using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities.Constants
{
    public class TableConstants
    {
        /// <summary>
        /// Status Inicial de uma mesa - quando o funcionário fechar a mesa no caixa.
        /// </summary>
        public const string OPEN_VALUE = "DISPONÍVEL";

        /// <summary>
        /// Status de uma mesa - quando uma conta for aberta na mesa
        /// </summary>
        public const string BLOCK_VALUE = "OCUPADA";
    }
}
