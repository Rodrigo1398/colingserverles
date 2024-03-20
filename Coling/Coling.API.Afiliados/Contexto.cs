using Coling.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados
{
    public class Contexto:DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {}
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Telefono> Telefono { get; set; }
        public virtual DbSet<Direccion> Direccion { get; set; }
        public virtual DbSet<TipoSocial> TipoSocial { get; set; }
        public virtual DbSet<PersonaTipoSocial> PersonaTipoSocial { get; set; }
        public virtual DbSet<Afiliado> Afiliado { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Telefono>()
        //        .HasOne(t => t.Persona)
        //        .WithMany(p => p.Telefono)
        //        .HasForeignKey(t => t.PersonaId);
        //}
    }
}
