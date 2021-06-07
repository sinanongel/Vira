using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vira.Models.Siniflar
{
    public class TeiasVeris
    {
        public int? AyId { get; set; }
        public string Yil { get; set; }
        public string Donem { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FaturaTarihi { get; set; }
        public decimal? UretimMiktari { get; set; }
        public decimal? SistemKullanimBedeliS { get; set; }
        public decimal? SistemKullanimBedeliD { get; set; }
        public decimal? SistemIsletimBedeli { get; set; }
        public decimal? IletimEkUcreti { get; set; }
        public decimal? Toplam { get; set; }
    }
}