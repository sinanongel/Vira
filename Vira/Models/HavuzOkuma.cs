using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class HavuzOkuma
    {
        [Key]
        public int HavuzOkumaId { get; set; }
        public DateTime OkumaTarihi { get; set; }
        public decimal? OkumaDegeri { get; set; }
        public decimal? DegisiklikMiktari { get; set; }

        public int HavuzId { get; set; }
        public virtual Havuz Havuz { get; set; }
        public int HavuzKotId { get; set; }
        public virtual HavuzKot HavuzKot { get; set; }
        public int HavaDurumuId { get; set; }
        public virtual HavaDurumu HavaDurumu { get; set; }
    }
}