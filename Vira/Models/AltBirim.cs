using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class AltBirim
    {
        [Key]
        public int AltBirimId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(200)]
        public string AltBirimAdi { get; set; }

        public int KurumId { get; set; }
        public virtual Kurum Kurum { get; set; }

        public ICollection<GelenEvrak> GelenEvraks { get; set; }
        public ICollection<GidenEvrak> GidenEvraks { get; set; }
    }
}