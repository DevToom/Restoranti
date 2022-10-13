using Domain.Interface;
using Entities.Entities;
using Infra.Context;
using Infra.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class RProduct : RestorantiRepository<Product>, IRProduct
    {
        private readonly DbContextOptions<RestorantiContext> _optionsBuilder;
        public RProduct()
        {
            this._optionsBuilder = new DbContextOptions<RestorantiContext>();
        }
    }
}
