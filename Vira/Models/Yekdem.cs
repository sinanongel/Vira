using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class Yekdem
    {
        [Key]
        public int YekdemId { get; set; }
        public decimal YekdemTutar { get; set; }
        public decimal BirimFiyatUsd { get; set; }
        public DateTime DonemTarih { get; set; }

        public int AyId { get; set; }
        public virtual Ay Ay { get; set; }
        public int YillarId { get; set; }
        public virtual Yillar Yillar { get; set; }
    }
}