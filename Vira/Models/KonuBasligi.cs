using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class KonuBasligi
    {
        [Key]
        public int KonuBasligiId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string BaslikAdi { get; set; }

        public ICollection<GidenEvrak> GidenEvraks { get; set; }
        public ICollection<GelenEvrak> GelenEvraks { get; set; }
    }
}