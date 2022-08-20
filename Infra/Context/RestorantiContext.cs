using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Context
{
    public class RestorantiContext : DbContext
    {
        public RestorantiContext()
        {

        }

        public RestorantiContext(DbContextOptions<RestorantiContext> options) : base(options)
        {

        }

        #region DbSet's

        public DbSet<User> Users { get; set; }

        #endregion

    }
}
