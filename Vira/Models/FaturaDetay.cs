using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vira.Models
{
    public class FaturaDetay
    {
        [Key]
        public int FaturaDetayId { get; set; }
        public decimal FdMiktar { get; set; }

        public decimal FdBirimFiyat { get; set; }
        public decimal FdKur { get; set; }

        public decimal FdBirimFiyatTl { get; set; }
        public decimal KdvTutar { get; set; }
        public decimal FdTutar { get; set; }

        public int FaturaId { get; set; }
        public virtual Fatura Fatura { get; set; }
        public int MalHizmetId { get; set; }
        public virtual MalHizmet MalHizmet { get; set; }
        public int BirimId { get; set; }
        public virtual Birim Birim { get; set; }
        public int KdvId { get; set; }
        public virtual Kdv Kdv { get; set; }
        public int DovizId { get; set; }
        public virtual Doviz Doviz { get; set; }

    }
}