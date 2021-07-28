using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class Ilceler
    {
        [Key]
        public int IlcelerId { get; set; }
        
        [Column(TypeName ="Varchar")]
        [StringLength(30)]
        public string IlceAdi { get; set; }

        public int IllerId { get; set; }
        public virtual Iller Iller { get; set; }

        public ICollection<Kurum> Kurums { get; set; }
        public ICollection<Firma> Firmas { get; set; }
    }
}