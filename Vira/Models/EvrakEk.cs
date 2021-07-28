using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class EvrakEk
    {
        [Key]
        public int EvrakEkId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(500)]
        public string EvrakEkAdi { get; set; }

        public int? GelenEvrakId { get; set; }
        public virtual GelenEvrak GelenEvrak { get; set; }

        public int? GidenEvrakId { get; set; }
        public virtual GidenEvrak GidenEvrak { get; set; }
    }
}