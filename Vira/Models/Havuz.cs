using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class Havuz
    {
        [Key]
        public int HavuzId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(10)]
        public string HavuzAdi { get; set; }

        public ICollection<HavuzKot> HavuzKots { get; set; }
        public ICollection<HavuzOkuma> HavuzOkumas { get; set; }
    }
}