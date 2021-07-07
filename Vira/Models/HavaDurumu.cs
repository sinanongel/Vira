using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class HavaDurumu
    {
        [Key]
        public int HavaDurumuId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(10)]
        public string Durum { get; set; }

        public ICollection<HavuzOkuma> HavuzOkumas { get; set; }
    }
}