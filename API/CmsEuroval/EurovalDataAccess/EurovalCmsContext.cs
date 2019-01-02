using EurovalDataAccess.Entities;
using EurovalDataAccess.Entities.InitialData;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EurovalDataAccess
{
    public class EurovalCmsContext : IdentityDbContext<CmsUser>
    {
        public EurovalCmsContext(DbContextOptions<EurovalCmsContext> options) : base(options)
        {

        }

        public DbSet<Pista> Pistas { get; set; }
        public DbSet<Socio> Socios { get; set; }
        public DbSet<Reserva> Reservas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<Pista>()
            //   .HasData(CmsExampleData.Pistas);
            //builder.Entity<Socio>()
            //   .HasData(CmsExampleData.Socios);
            //builder.Entity<Reserva>()
            //   .HasData(CmsExampleData.Reservas);
        }
    }
}
