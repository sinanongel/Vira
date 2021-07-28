using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class GidenEvrak
    {
        [Key]
        public int GidenEvrakId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string GidenEvrakSayi { get; set; }
        public DateTime GidenEvrakTarih { get; set; }
        public DateTime IslemTarihi { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(500)]
        public string GidenEvrakKonu { get; set; }
        public bool GidenEvrakDurum { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(500)]
        public string GidenEvrakAciklama { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string GidenEvrakdosya { get; set; }

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