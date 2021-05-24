using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class Fatura
    {
        [Key]
        public int FaturaId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(16)]
        public string FaturaNo { get; set; }
        public DateTime FaturaTarihi { get; set; }
        public decimal FaturaToplami { get; set; }
        public decimal FaturaKdvToplami { get; set; }
        public decimal FaturaOdenecekTutar { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(5)]
        public string FaturaTipi { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(5)]
        public string FaturaTuru { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(12)]
        public string FaturaDonemi { get; set; }
        public bool FaturaOdemeDurumu { get; set; }

        public int KurumId { get; set; }
        public virtual Kurum Kurum { get; set; }
        public int? AyId { get; set; }
        public virtual Ay Ay { get; set; }
        public int? YillarId { get; set; }
        public virtual Yillar Yillar { get; set; }

        public ICollection<FaturaDetay> FaturaDetays { get; set; }
        public ICollection<Dosya> Dosyas { get; set; }
    }
}