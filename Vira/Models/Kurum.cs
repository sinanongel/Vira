using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class Kurum
    {
        [Key]
        public int KurumId { get; set; }

        [Column(TypeName ="Varchar")]
        [StringLength(100)]
        public string KurumAdi { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string KurumIlgiliKisi { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string KurumAdres { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(11)]
        public string KurumTelefon { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(11)]
        public string KurumGsm { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string KurumEPosta { get; set; }

        public int IllerId { get; set; }
        public virtual Iller Iller { get; set; }
        public int IlcelerId { get; set; }
        public virtual Ilceler Ilceler { get; set; }

        public ICollection<Fatura> Faturas { get; set; }
    }
}