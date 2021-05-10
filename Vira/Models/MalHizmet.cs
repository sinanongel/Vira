using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class MalHizmet
    {
        [Key]
        public int MalHizmetId { get; set; }
        
        [Column(TypeName ="Varchar")]
        [StringLength(100)]
        public string MalHizmetAdi { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(5)]
        public string MalHizmetTuru { get; set; }

        public int BirimId { get; set; }
        public virtual Birim Birim { get; set; }
    }
}