using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class Doviz
    {
        [Key]
        public int DovizId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(10)]
        public string DovizCinsi { get; set; }

        public ICollection<FaturaDetay> FaturaDetays { get; set; }
    }
}