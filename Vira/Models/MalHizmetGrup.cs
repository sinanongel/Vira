using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class MalHizmetGrup
    {
        [Key]
        public int MalHizmetGrupId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string MalHizmetGrupAdi { get; set; }

        public ICollection<MalHizmet> MalHizmets { get; set; }
    }
}