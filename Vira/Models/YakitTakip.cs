using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class YakitTakip
    {
        [Key]
        public int YakitTakipId { get; set; }
        public DateTime YakitAlimTarihi { get; set; }
        public decimal YakitAlimMiktari { get; set; }
        public decimal BaslangicKm { get; set; }
        public decimal BitisKm { get; set; }
        public decimal GidilenKm { get; set; }
        public decimal OrtalamaTuketim { get; set; }
        public int? OncekiKayitId { get; set; }

        public int AraclarId { get; set; }
        public virtual Araclar Araclar { get; set; }

    }
}