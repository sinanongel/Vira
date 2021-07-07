using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class Dosya
    {
        [Key]
        public int DosyaId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(250)]
        public string DosyaYolu { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(250)]
        public string DosyaAdi { get; set; }

        public int DosyaTuruId { get; set; }
        public virtual DosyaTuru DosyaTuru { get; set; }
    }
}