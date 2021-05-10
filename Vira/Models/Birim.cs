using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class Birim
    {
        [Key]
        public int BirimId { get; set; }

        [Column(TypeName ="Varchar")]
        [StringLength(10)]
        public string BirimAdi { get; set; }

        public ICollection<MalHizmet> MalHizmets { get; set; }
    }
}