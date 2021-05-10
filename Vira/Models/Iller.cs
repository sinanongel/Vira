using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class Iller
    {
        [Key]
        public int IllerId { get; set; }

        [Column(TypeName="Varchar")]
        [StringLength(30)]
        public string Sehir { get; set; }

        public ICollection<Ilceler> Ilcelers { get; set; }
        public ICollection<Kurum> Kurums { get; set; }
    }
}