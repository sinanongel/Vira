using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vira.Models.Siniflar
{
    public class FaturaRapor
    {
        public int? BasYil { get; set; }
        public int? BitYil { get; set; }
        public int? BasAy { get; set; }
        public int? BitAy { get; set; }
        public DateTime BasTarih { get; set; }
        public DateTime BitTarih { get; set; }
        public int? KurumId { get; set; }
        public int? MalHizmet { get; set; }
        public string Detay { get; set; }
        public DateTime Tarih1 { get; set; }
        public DateTime Tarih2 { get; set; }
    }
}