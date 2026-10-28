using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStokTakip.Models.Entity;


namespace MvcStokTakip.Controllers
{
    public class SatislarController : Controller
    {
        // GET: Satislar
        MvcDbStokEntities db = new MvcDbStokEntities();

        public ActionResult Index()
        {
            // 1. Satış Listesi
            var satislar = db.tbl_Satislar.ToList();

            // 2. Ürünler Dropdown Listesi
            List<SelectListItem> urunler = (from u in db.tbl_Urunler.ToList()
                                            select new SelectListItem
                                            {
                                                Text = u.UrunAd,
                                                Value = u.UrunId.ToString()
                                            }).ToList();
            ViewBag.Urunler = urunler;

            // 3. Müşteriler Dropdown Listesi (Ad + Soyad)
            List<SelectListItem> musteriler = (from m in db.tbl_Musteriler.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = m.MusteriAd + " " + m.MusteriSoyad,
                                                   Value = m.MusteriId.ToString()
                                               }).ToList();
            ViewBag.Musteriler = musteriler;

            return View(satislar);
        }
        [HttpPost]
        public ActionResult YeniSatis(tbl_Satislar p1, int Urun, int Musteri)
        {
            // Formdan gelen 'Urun' ve 'Musteri' ID değerleriyle veritabanından nesneleri buluyoruz
            var urun = db.tbl_Urunler.Find(Urun);
            var musteri = db.tbl_Musteriler.Find(Musteri);

            p1.tbl_Urunler = urun;
            p1.tbl_Musteriler = musteri;

            db.tbl_Satislar.Add(p1);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}