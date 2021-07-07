using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class Sozlesme
    {
        [Key]
        public int SozlesmeId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string SozlesmeKonusu { get; set; }
        public DateTime SozlesmeTarihi { get; set; }
        public int SozlesmeSuresi { get; set; }
        public decimal SozlesmeBedeli { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string Dosya { get; set; }

        public int YukleniciKurumId { get; set; }
        public int IsverenKurumId { get; set; }


        [ForeignKey("YukleniciKurumId")]
        [InverseProperty("YukleniciKurumSozlesmes")]
        public virtual Kurum YukleniciKurum { get; set; }

        [ForeignKey("IsverenKurumId")]
        [InverseProperty("IsverenKurumSozlesmes")]
        public virtual Kurum IsverenKurum { get; set; }
    }
}