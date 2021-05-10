using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class Context : DbContext
    {
        public DbSet<Birim> Birims { get; set; }
        public DbSet<Ilceler> Ilcelers { get; set; }
        public DbSet<Iller> Illers { get; set; }
        public DbSet<Kurum> Kurums { get; set; }
        public DbSet<MalHizmet> MalHizmets { get; set; }
        public DbSet<Dosya> Dosyas { get; set; }
        public DbSet<DosyaTuru> DosyaTurus { get; set; }
        public DbSet<Doviz> Dovizs { get; set; }
        public DbSet<Fatura> Faturas { get; set; }
        public DbSet<FaturaDetay> FaturaDetays { get; set; }
        public DbSet<Kdv> Kdvs { get; set; }
        public DbSet<Kullanici> Kullanicis { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FaturaDetay>().Property(x => x.FdBirimFiyat).HasPrecision(18, 8);
            modelBuilder.Entity<FaturaDetay>().Property(x => x.FdBirimFiyatTl).HasPrecision(18, 8);
        }
    }
}