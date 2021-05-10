using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class Kullanici
    {
        [Key]
        public int KullaniciId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string KullaniciAdi { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(25)]
        public string Ad { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(25)]
        public string Soyad { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(10)]
        public string Sifre { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string Mail { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(1)]
        public string YetkiTur { get; set; }
    }
}