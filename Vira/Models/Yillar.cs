using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class Yillar
    {
        [Key]
        public int YillarId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(4)]
        public string Yil { get; set; }

        public ICollection<Fatura> Faturas { get; set; }
        public ICollection<Yekdem> Yekdems { get; set; }
    }
}