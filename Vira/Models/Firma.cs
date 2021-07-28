using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class Firma
    {
        [Key]
        public int FirmaId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string FirmaAdi { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string FirmaUnvani { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string FirmaAdresi { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(11)]
        public string FirmaTelefon { get; set; }

        public int IllerId { get; set; }
        public virtual Iller Iller { get; set; }
        public int IlcelerId { get; set; }
        public virtual Ilceler Ilceler { get; set; }

        public ICollection<GelenEvrak> GelenEvraks { get; set; }
        public ICollection<GidenEvrak> GidenEvraks { get; set; }
    }
}