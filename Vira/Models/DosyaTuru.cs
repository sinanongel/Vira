using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class DosyaTuru
    {
        [Key]
        public int DosyaTuruId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string DosyaTuruAd { get; set; }

        public ICollection<Dosya> Dosyas { get; set; }
    }
}