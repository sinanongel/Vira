using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vira.Models.Siniflar
{
    public class UretimAnaliz
    {
        public int? Yil { get; set; }
        public string Donem { get; set; }
        public decimal AylikUretim { get; set; }
        public decimal Kumulatif { get; set; }
        public decimal YekdemTutari { get; set; }
        public decimal YekdemTutariUsd { get; set; }
        public decimal OrtalamaAylikUsdTl { get; set; }
        public decimal ViraFaturaDegeri { get; set; }
        public decimal EpiaşFaturaDegeri { get; set; }
        public decimal FaturaFarkıTl { get; set; }
        public decimal FaturaFarkıUsd { get; set; }
        public decimal KarZarar { get; set; }
    }
}