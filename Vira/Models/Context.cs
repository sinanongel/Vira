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
        public DbSet<Ay> Ays { get; set; }
        public DbSet<Yillar> Yillars { get; set; }
        public DbSet<Yekdem> Yekdems { get; set; }
        public DbSet<Sozlesme> Sozlesmes { get; set; }

        //public DbSet<HavuzKot> HavuzKots { get; set; }
        //public DbSet<Havuz> Havuzs { get; set; }
        //public DbSet<HavaDurumu> HavaDurumus { get; set; }
        //public DbSet<HavuzOkuma> HavuzOkumas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FaturaDetay>().Property(x => x.FdBirimFiyat).HasPrecision(18, 8);
            modelBuilder.Entity<FaturaDetay>().Property(x => x.FdBirimFiyatTl).HasPrecision(18, 8);

            modelBuilder.Entity<Sozlesme>().HasRequired(m => m.YukleniciKurum).WithMany(m => m.YukleniciKurumSozlesmes).HasForeignKey(m => m.YukleniciKurumId);
            modelBuilder.Entity<Sozlesme>().HasRequired(m => m.IsverenKurum).WithMany(m => m.IsverenKurumSozlesmes).HasForeignKey(m => m.IsverenKurumId);
        }
    }
}