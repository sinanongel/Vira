using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vira.Models.Siniflar
{
    public class YakitAnaliz
    {
        public int AracId { get; set; }
        public int Yil { get; set; }
        public int Ay { get; set; }
        public string AyAd { get; set; }
        public decimal KmBilgisi { get; set; }
        public decimal AlimMiktari { get; set; }
        public decimal AylikOrtalamaTuketim { get; set; }
        public decimal YillikOrtalamaTuketim { get; set; }
    }
}