﻿using Entities.Entities;
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
        public RestorantiContext(DbContextOptions<RestorantiContext> options) : base(options)
        {}

        #region DbSet's

        public DbSet<UserInternal>? Users { get; set; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(GetConnectionString(), new MySqlServerVersion(new Version()));
            }
        }

        private string GetConnectionString()
        {
            //return "Server = 50.116.86.84; Port = 3306; Database = trans052_bancoteste; Uid = trans052_admin; Pwd = TCC@unip2022";
            return "Server=localhost;Port=3306;Database=meubanco;Uid=root;Pwd=admin";
        }

    }
}
