using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class Ay
    {
        [Key]
        public int AyId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(7)]
        public string AyAd { get; set; }

        public ICollection<Fatura> Faturas { get; set; }
        public ICollection<Yekdem> Yekdems { get; set; }

    }
}