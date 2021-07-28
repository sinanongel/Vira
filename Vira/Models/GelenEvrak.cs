using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class GelenEvrak
    {
        [Key]
        public int GelenEvrakId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string GelenEvrakSayi { get; set; }
        public DateTime GelenEvrakTarih { get; set; }
        public DateTime TebligatTarihi { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(500)]
        public string GelenEvrakKonu { get; set; }
        public bool GelenEvrakDurum { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(500)]
        public string GelenEvrakAciklama { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string GelenEvrakdosya { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(500)]
        public string IlgiEvrak { get; set; }
        public int? EkAdet { get; set; }

        public int KonuBasligiId { get; set; }
        public virtual KonuBasligi KonuBasligi { get; set; }
        public int FirmaId { get; set; }
        public virtual Firma Firma { get; set; }
        public int KurumId { get; set; }
        public virtual Kurum Kurum { get; set; }
        public int? AltBirimId { get; set; }
        public virtual AltBirim AltBirim { get; set; }

        public ICollection<EvrakEk> EvrakEks { get; set; }
    }
}