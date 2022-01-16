using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vira.Models;

namespace Vira.Controllers
{
    public class SaatlikUretimController : Controller
    {
        // GET: SaatlikUretim
        Context c = new Context();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ExcelYukleme(HttpPostedFileBase ExcelDosya, DateTime SaatlikUretimTarih)
        {
            DataSet ds = new DataSet();

            if (Request.Files["ExcelDosya"].ContentLength > 0)
            {
                string dosyaUzanti = Path.GetExtension(Request.Files["ExcelDosya"].FileName);
                if (dosyaUzanti == ".xls" || dosyaUzanti == ".xlsx")
                {
                    string dosyaYolu = "D:\\Dosya\\Temp\\" + Request.Files["ExcelDosya"].FileName;
                    if (System.IO.File.Exists(dosyaYolu))
                    {
                        System.IO.File.Delete(dosyaYolu);
                    }
                    Request.Files["ExcelDosya"].SaveAs(dosyaYolu);
                    string excelBaglanti = string.Empty;
                    excelBaglanti = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dosyaYolu + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";

                    OleDbConnection excBgl = new OleDbConnection(excelBaglanti);
                    excBgl.Open();
                    DataTable dt = new DataTable();
                    dt = excBgl.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    if (dt == null)
                    {
                        return null;
                    }

                    String[] excelSayfa = new String[dt.Rows.Count];
                    int sayac = 0;

                    foreach (DataRow row in dt.Rows)
                    {
                        excelSayfa[sayac] = row["TABLE_NAME"].ToString();
                        sayac++;
                    }

                    OleDbConnection excBgl1 = new OleDbConnection(excelBaglanti);
                    string sorgu = string.Format("Select * from[{0}]", excelSayfa[0]);

                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(sorgu, excBgl1))
                    {
                        dataAdapter.Fill(ds);
                    }

                    for (int i = 3; i < ds.Tables[0].Rows.Count; i++)
                    {
                        SaatlikUretim p = new SaatlikUretim();

                        string baSaat = ds.Tables[0].Rows[i][2].ToString();
                        string biSaat = ds.Tables[0].Rows[i][3].ToString();
                        string suYukseklik = ds.Tables[0].Rows[i][4].ToString();
                        string sepam1IlkEndeks= ds.Tables[0].Rows[i][5].ToString();
                        string sepam1SonEndeks = ds.Tables[0].Rows[i][6].ToString();
                        string sepam1TopUretim = ds.Tables[0].Rows[i][7].ToString();
                        string sepam2IlkEndeks = ds.Tables[0].Rows[i][8].ToString();
                        string sepam2SonEndeks = ds.Tables[0].Rows[i][9].ToString();
                        string sepam2TopUretim = ds.Tables[0].Rows[i][10].ToString();
                        string tug1Tug2TopUretim = ds.Tables[0].Rows[i][11].ToString();
                        string uni1IlkEndeks = ds.Tables[0].Rows[i][12].ToString();
                        string uni1SonEndeks = ds.Tables[0].Rows[i][13].ToString();
                        string uni1Uretim = ds.Tables[0].Rows[i][14].ToString();
                        string uni2IlkEndeks = ds.Tables[0].Rows[i][15].ToString();
                        string uni2SonEndeks = ds.Tables[0].Rows[i][16].ToString();
                        string uni2Uretim = ds.Tables[0].Rows[i][17].ToString();
                        string tug1TopUretim = ds.Tables[0].Rows[i][18].ToString();
                        string tug2TopUretim = ds.Tables[0].Rows[i][19].ToString();
                        string anlikMaksGuc = ds.Tables[0].Rows[i][20].ToString();
                        string gucFaktoru = ds.Tables[0].Rows[i][21].ToString();
                        string enerjiNakHatGerilim = ds.Tables[0].Rows[i][22].ToString();
                        string ortamSicakligi = ds.Tables[0].Rows[i][23].ToString();
                        //string unite1 = ds.Tables[0].Rows[i][24].ToString();
                        //string unite2 = ds.Tables[0].Rows[i][25].ToString();
                        string uni1YatakSicY1 = ds.Tables[0].Rows[i][26].ToString();
                        string uni1YatakSicY2 = ds.Tables[0].Rows[i][27].ToString();
                        string uni1GenMakMSic = ds.Tables[0].Rows[i][28].ToString();
                        string Uni1GovRBasinci = ds.Tables[0].Rows[i][29].ToString();
                        string uni2YatakSicY1 = ds.Tables[0].Rows[i][30].ToString();
                        string uni2YatakSicY2 = ds.Tables[0].Rows[i][31].ToString();
                        string uni2GenMakMSic = ds.Tables[0].Rows[i][32].ToString();
                        string Uni2GovRBasinci = ds.Tables[0].Rows[i][33].ToString();
                        string Uni1NozAcikYuzN1 = ds.Tables[0].Rows[i][34].ToString();
                        string Uni1NozAcikYuzN2 = ds.Tables[0].Rows[i][35].ToString();
                        string Uni2NozAcikYuzN1 = ds.Tables[0].Rows[i][36].ToString();
                        string Uni2NozAcikYuzN2 = ds.Tables[0].Rows[i][37].ToString();
                        string ArizaNotu = ds.Tables[0].Rows[i][38].ToString();

                        p.BasSaat = Convert.ToDateTime(baSaat).ToString("HH:mm");
                        p.BitSaat = Convert.ToDateTime(biSaat).ToString("HH:mm");
                        if (suYukseklik == "") { p.SuYukseklik = 0; } else { p.SuYukseklik = Convert.ToInt32(suYukseklik); }
                        if (sepam1IlkEndeks == "") { p.Sepam1IlkEndeks = 0; } else { p.Sepam1IlkEndeks = Convert.ToDecimal(sepam1IlkEndeks); }
                        if (sepam1SonEndeks == "") { p.Sepam1SonEndeks = 0; } else { p.Sepam1SonEndeks = Convert.ToDecimal(sepam1SonEndeks); }
                        if (sepam1TopUretim == "") { p.Sepam1TopUretim = 0; } else { p.Sepam1TopUretim = Convert.ToDecimal(sepam1TopUretim); }
                        if (sepam2IlkEndeks == "") { p.Sepam2IlkEndeks = 0; } else { p.Sepam2IlkEndeks = Convert.ToDecimal(sepam2IlkEndeks); }
                        if (sepam2SonEndeks == "") { p.Sepam2SonEndeks = 0; } else { p.Sepam2SonEndeks = Convert.ToDecimal(sepam2SonEndeks); }
                        if (sepam2TopUretim == "") { p.Sepam2TopUretim = 0; } else { p.Sepam2TopUretim = Convert.ToDecimal(sepam2TopUretim); }
                        if (tug1Tug2TopUretim == "") { p.Tug1Tug2TopUretim = 0; } else { p.Tug1Tug2TopUretim = Convert.ToDecimal(tug1Tug2TopUretim); }
                        if (uni1IlkEndeks == "") { p.Uni1IlkEndeks = 0; } else { p.Uni1IlkEndeks = Convert.ToDecimal(uni1IlkEndeks); }
                        if (uni1SonEndeks == "") { p.Uni1SonEndeks = 0; } else { p.Uni1SonEndeks = Convert.ToDecimal(uni1SonEndeks); }
                        if (uni2IlkEndeks == "") { p.Uni2IlkEndeks = 0; } else { p.Uni2IlkEndeks = Convert.ToDecimal(uni2IlkEndeks); }
                        if (uni2SonEndeks == "") { p.Uni2SonEndeks = 0; } else { p.Uni2SonEndeks = Convert.ToDecimal(uni2SonEndeks); }
                        if (uni2Uretim == "") { p.Uni2Uretim = 0; } else { p.Uni2Uretim = Convert.ToDecimal(uni2Uretim); }
                        if (tug1TopUretim == "") { p.Tug1TopUretim = 0; } else { p.Tug1TopUretim = Convert.ToDecimal(tug1TopUretim); }
                        if (tug2TopUretim == "") { p.Tug2TopUretim = 0; } else { p.Tug2TopUretim = Convert.ToDecimal(tug2TopUretim); }
                        if (anlikMaksGuc == "") { p.AnlikMaksGuc = 0; } else { p.AnlikMaksGuc = Convert.ToDecimal(anlikMaksGuc); }
                        if (gucFaktoru == "") { p.GucFaktoru = 0; } else { p.GucFaktoru = Convert.ToDecimal(gucFaktoru); }
                        if (enerjiNakHatGerilim == "") { p.EnerjiNakHatGerilim = 0; } else { p.EnerjiNakHatGerilim = Convert.ToDecimal(enerjiNakHatGerilim); }
                        if (ortamSicakligi == "") { p.OrtamSicakligi = 0; } else { p.OrtamSicakligi = Convert.ToDecimal(ortamSicakligi); }
                        p.Unite1 = ds.Tables[0].Rows[i][24].ToString();
                        p.Unite2 = ds.Tables[0].Rows[i][25].ToString();

                    }
                }
            }

            return View("Index");
        }
    }
}