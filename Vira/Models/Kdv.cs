using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class Kdv
    {
        [Key]
        public int KdvId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(2)]
        public string KdvOrani { get; set; }

        public ICollection<FaturaDetay> FaturaDetays { get; set; }
    }
}