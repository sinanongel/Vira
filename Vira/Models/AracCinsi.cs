using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class AracCinsi
    {
        [Key]
        public int AracCinsiId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(15)]
        public string AracCinsiAdi { get; set; }

        public ICollection<Araclar> Araclars { get; set; }
    }
}