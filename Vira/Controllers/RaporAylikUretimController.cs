using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vira.Models;
using Vira.Models.Siniflar;

namespace Vira.Controllers
{
    public class RaporAylikUretimController : Controller
    {
        // GET: RaporAylikUretim
        Context c = new Context();
        public ActionResult Index()
        {
            List<AylikUretim> aylikUretim = new List<AylikUretim>();

            var miktarListe = c.FaturaDetays
                .Where(x => x.MalHizmetId == 4)
                .GroupBy(y => new { y.Fatura.Yillar.Yil, y.Fatura.AyId, y.Fatura.Ay.AyAd })
                .OrderBy(z => new { z.Key.Yil, z.Key.AyId })
                .Select(y => new { Yil = y.Key.Yil, AyAd = y.Key.AyAd, Miktar = y.Sum(z => z.FdMiktar) })
                .ToList();

            int kayitSayisi = miktarListe.Count();
            int eklenekKayit = 12 - kayitSayisi / 12;

            List<AylikUretimListe> aylikUretimListe = new List<AylikUretimListe>();

            for (int i = 0; i < kayitSayisi; i++)
            {
                aylikUretimListe.Add(new AylikUretimListe
                {
                    Yil = miktarListe[i].Yil,
                    AyAd = miktarListe[i].AyAd,
                    Miktar = miktarListe[i].Miktar
                });
            }

            if (eklenekKayit != 0)
            {
                string sonYil = miktarListe.Last().Yil;

                for (int i = 0; i < eklenekKayit; i++)
                {
                    aylikUretimListe.Add(new AylikUretimListe
                    {
                        Yil = sonYil,
                        AyAd = "",
                        Miktar = 0
                    });
                }
            }

            for (int i = 0; i < aylikUretimListe.Count(); i = i + 12)
            {
                string yil = aylikUretimListe[i].Yil;
                decimal ocak = aylikUretimListe[i].Miktar;
                decimal subat = aylikUretimListe[i + 1].Miktar;
                decimal mart = aylikUretimListe[i + 2].Miktar;
                decimal nisan = aylikUretimListe[i + 3].Miktar;
                decimal mayis = aylikUretimListe[i + 4].Miktar;
                decimal haziran = aylikUretimListe[i + 5].Miktar;
                decimal temmuz = aylikUretimListe[i + 6].Miktar;
                decimal agustos = aylikUretimListe[i + 7].Miktar;
                decimal eylul = aylikUretimListe[i + 8].Miktar;
                decimal ekim = aylikUretimListe[i + 9].Miktar;
                decimal kasim = aylikUretimListe[i + 10].Miktar;
                decimal aralik = aylikUretimListe[i + 11].Miktar;

                aylikUretim.Add(new AylikUretim
                {
                    Yil = yil,
                    Ocak = ocak,
                    Subat = subat,
                    Mart = mart,
                    Nisan = nisan,
                    Mayis = mayis,
                    Haziran = haziran,
                    Temmuz = temmuz,
                    Agustos = agustos,
                    Eylul = eylul,
                    Ekim = ekim,
                    Kasim = kasim,
                    Aralik = aralik,
                });

                aylikUretim.Add(new AylikUretim
                {
                    Yil = yil + " (Kümülatif)",
                    Ocak = ocak,
                    Subat = ocak + subat,
                    Mart = ocak + subat + mart,
                    Nisan = ocak + subat + mart + nisan,
                    Mayis = ocak + subat + mart + nisan + mayis,
                    Haziran = ocak + subat + mart + nisan + mayis + haziran,
                    Temmuz = ocak + subat + mart + nisan + mayis + haziran + temmuz,
                    Agustos = ocak + subat + mart + nisan + mayis + haziran + temmuz + agustos,
                    Eylul = ocak + subat + mart + nisan + mayis + haziran + temmuz + agustos + eylul,
                    Ekim = ocak + subat + mart + nisan + mayis + haziran + temmuz + agustos + eylul + ekim,
                    Kasim = ocak + subat + mart + nisan + mayis + haziran + temmuz + agustos + eylul + ekim + kasim,
                    Aralik = ocak + subat + mart + nisan + mayis + haziran + temmuz + agustos + eylul + ekim + kasim + aralik,
                });
            }

            decimal ocakTop = 0,
                subatTop = 0,
                martTop = 0,
                nisanTop = 0,
                mayisTop = 0,
                haziranTop = 0,
                temmuztop = 0,
                agustosTop = 0,
                eylulTop = 0,
                ekimTop = 0,
                kasimTop = 0,
                aralikTop = 0;
            int sayi = aylikUretim.Count() / 2;

            for (int i = 0; i < aylikUretim.Count(); i = i + 2)
            {
                ocakTop += Convert.ToDecimal(aylikUretim[i].Ocak);
                subatTop += Convert.ToDecimal(aylikUretim[i].Subat);
                martTop += Convert.ToDecimal(aylikUretim[i].Mart);
                nisanTop += Convert.ToDecimal(aylikUretim[i].Nisan);
                mayisTop += Convert.ToDecimal(aylikUretim[i].Mayis);
                haziranTop += Convert.ToDecimal(aylikUretim[i].Haziran);
                temmuztop += Convert.ToDecimal(aylikUretim[i].Temmuz);
                agustosTop += Convert.ToDecimal(aylikUretim[i].Agustos);
                eylulTop += Convert.ToDecimal(aylikUretim[i].Eylul);
                ekimTop += Convert.ToDecimal(aylikUretim[i].Ekim);
                kasimTop += Convert.ToDecimal(aylikUretim[i].Kasim);
                aralikTop += Convert.ToDecimal(aylikUretim[i].Aralik);
            }

            aylikUretim.Add(new AylikUretim
            {
                Yil = "Ortalama",
                Ocak = Math.Round((ocakTop / sayi), 2),
                Subat = Math.Round((subatTop / sayi), 2),
                Mart = Math.Round((martTop / sayi), 2),
                Nisan = Math.Round((nisanTop / sayi), 2),
                Mayis = Math.Round((mayisTop / sayi), 2),
                Haziran = Math.Round((haziranTop / sayi), 2),
                Temmuz = Math.Round((temmuztop / sayi), 2),
                Agustos = Math.Round((agustosTop / sayi), 2),
                Eylul = Math.Round((eylulTop / sayi), 2),
                Ekim = Math.Round((ekimTop / sayi), 2),
                Kasim = Math.Round((kasimTop / sayi), 2),
                Aralik = Math.Round((aralikTop / sayi), 2)
            });

            decimal? ocakKum= aylikUretim.Last().Ocak;
            decimal? subatKum = aylikUretim.Last().Subat;
            decimal? martKum = aylikUretim.Last().Mart;
            decimal? nisanKum = aylikUretim.Last().Nisan;
            decimal? mayisKum = aylikUretim.Last().Mayis;
            decimal? haziranKum = aylikUretim.Last().Haziran;
            decimal? temmuzKum = aylikUretim.Last().Temmuz;
            decimal? agustosKum = aylikUretim.Last().Agustos;
            decimal? eylulKum = aylikUretim.Last().Eylul;
            decimal? ekimKum = aylikUretim.Last().Ekim;
            decimal? kasimKum = aylikUretim.Last().Kasim;
            decimal? aralikKum = aylikUretim.Last().Aralik;

            aylikUretim.Add(new AylikUretim
            {
                Yil = "Ortalama Kümülatif",
                Ocak = ocakKum,
                Subat = ocakKum + subatKum,
                Mart = ocakKum + subatKum + martKum,
                Nisan = ocakKum + subatKum + martKum + nisanKum,
                Mayis = ocakKum + subatKum + martKum + nisanKum + mayisKum,
                Haziran = ocakKum + subatKum + martKum + nisanKum + mayisKum + haziranKum,
                Temmuz = ocakKum + subatKum + martKum + nisanKum + mayisKum + haziranKum + temmuzKum,
                Agustos = ocakKum + subatKum + martKum + nisanKum + mayisKum + haziranKum + temmuzKum + agustosKum,
                Eylul = ocakKum + subatKum + martKum + nisanKum + mayisKum + haziranKum + temmuzKum + agustosKum + eylulKum,
                Ekim = ocakKum + subatKum + martKum + nisanKum + mayisKum + haziranKum + temmuzKum + agustosKum + eylulKum + eylulKum,
                Kasim = ocakKum + subatKum + martKum + nisanKum + mayisKum + haziranKum + temmuzKum + agustosKum + eylulKum + eylulKum + kasimKum,
                Aralik = ocakKum + subatKum + martKum + nisanKum + mayisKum + haziranKum + temmuzKum + agustosKum + eylulKum + eylulKum + kasimKum + aralikKum,
            });

            return View(aylikUretim);
        }
    }
}