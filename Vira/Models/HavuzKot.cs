using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class HavuzKot
    {
        [Key]
        public int HavuzKotId { get; set; }
        public int NoktaNo { get; set; }
        public decimal? KotDegeri { get; set; }

        public int HavuzId { get; set; }
        public virtual  Havuz Havuz   { get; set; }

        public ICollection<HavuzOkuma> HavuzOkumas { get; set; }
    }
}