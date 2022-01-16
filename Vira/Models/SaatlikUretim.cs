using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class SaatlikUretim
    {
        [Key]
        public int SaatlikUretimId { get; set; }
        public DateTime SaatlikUretimTarih { get; set; }
        public string BasSaat { get; set; }
        public string BitSaat { get; set; }
        public int? SuYukseklik { get; set; }
        public decimal? Sepam1IlkEndeks { get; set; }
        public decimal? Sepam1SonEndeks { get; set; }
        public decimal? Sepam1TopUretim { get; set; }
        public decimal? Sepam2IlkEndeks { get; set; }
        public decimal? Sepam2SonEndeks { get; set; }
        public decimal? Sepam2TopUretim { get; set; }
        public decimal? Tug1Tug2TopUretim { get; set; }
        public decimal? Uni1IlkEndeks { get; set; }
        public decimal? Uni1SonEndeks { get; set; }
        public decimal? Uni1Uretim { get; set; }
        public decimal? Uni2IlkEndeks { get; set; }
        public decimal? Uni2SonEndeks { get; set; }
        public decimal? Uni2Uretim { get; set; }
        public decimal? Tug1TopUretim { get; set; }
        public decimal? Tug2TopUretim { get; set; }
        public decimal? AnlikMaksGuc { get; set; }
        public decimal? GucFaktoru { get; set; }
        public decimal? EnerjiNakHatGerilim { get; set; }
        public decimal? OrtamSicakligi { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(1)]
        public string Unite1 { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(1)]
        public string Unite2 { get; set; }
        public decimal? Uni1YatakSicY1 { get; set; }
        public decimal? Uni1YatakSicY2 { get; set; }
        public decimal? Uni1GenMakMSic { get; set; }
        public decimal? Uni1GovRBasinci { get; set; }
        public decimal? Uni2YatakSicY1 { get; set; }
        public decimal? Uni2YatakSicY2 { get; set; }
        public decimal? Uni2GenMakMSic { get; set; }
        public decimal? Uni2GovRBasinci { get; set; }
        public int? Uni1NozAcikYuzN1 { get; set; }
        public int? Uni1NozAcikYuzN2 { get; set; }
        public int? Uni2NozAcikYuzN1 { get; set; }
        public int? Uni2NozAcikYuzN2 { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(500)]
        public string ArizaNotu { get; set; }
    }
}