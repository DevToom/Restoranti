using Entities.Entities;
using Infra.Context;
using Infra.Repository.Generics;
using Infra.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class RUser : RestorantiRepository<User>, IRUser
    {
        public RUser(RestorantiContext context) : base(context)
        {

        }
    }
}
