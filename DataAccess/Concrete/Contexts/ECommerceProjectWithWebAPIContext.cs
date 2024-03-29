﻿using DataAccess.Concrete.EntityFramework.Mapping;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Contexts
{
    public class ECommerceProjectWithWebAPIContext : DbContext
    {
        public ECommerceProjectWithWebAPIContext(DbContextOptions<ECommerceProjectWithWebAPIContext> options) : base(options)
        {

        }
        public ECommerceProjectWithWebAPIContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connString = @"Server=DESKTOP-QVV2406;Database=ECommerceProjectWithWebAPIDb;Trusted_Connection=True;Connect Timeout=30;MultipleActiveResultSets=True;";
            optionsBuilder.UseSqlServer(connString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
        }
        public virtual DbSet<User> Users { get; set; }
    }
}
