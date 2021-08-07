using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class Araclar
    {
        [Key]
        public int AraclarId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(10)]
        public string AracPlakasi { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(15)]
        public string AracMarka { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(25)]
        public string AracModel { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(10)]
        public string CalismaSekli { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string KullanimYeri { get; set; }

        public int AracCinsiId { get; set; }
        public virtual AracCinsi AracCinsi { get; set; }

        public ICollection<YakitTakip> YakitTakips { get; set; }
    }
}