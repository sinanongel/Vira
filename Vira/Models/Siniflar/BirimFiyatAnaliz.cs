using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vira.Models.Siniflar
{
    public class BirimFiyatAnaliz
    {
        public int Yil { get; set; }
        public int Ay { get; set; }
        public int MalHizGrupId { get; set; }
        public int MalHizId { get; set; }
        public string AyAd { get; set; }
        public decimal BirimFiyat { get; set; }
        public decimal AylikOrtalamaBirimFiyat { get; set; }
        public decimal YillikOrtalamaBirimFiyat { get; set; }
        public string FaturaTarihi { get; set; }
    }
}