using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStokTakip.Models.Entity;

namespace MvcStokTakip.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(string p)
        {
            var degerler = from d in db.tbl_Musteriler select d;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.MusteriAd.Contains(p));
            }
            return View(degerler.ToList());
            //var degerler = db.tbl_Musteriler.ToList();
            //return View(degerler);
        }

        // Yeni kategori ekleme sayfasını sadece görüntülemek için çalışır (Sayfa ilk açıldığında)
        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }

        // Ekle butonuna basıldığında (Form gönderildiğinde) çalışır
        [HttpPost]
        public ActionResult YeniMusteri(tbl_Musteriler p2)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            // Formdan gelen yeni kategori bilgilerini hafızaya ekler
            db.tbl_Musteriler.Add(p2);

            // Değişiklikleri veritabanına kalıcı olarak kaydeder
            db.SaveChanges();
            return View();

        }
        public ActionResult Sil(int id)
        {
            var musteri = db.tbl_Musteriler.Find(id);
            db.tbl_Musteriler.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriGetir(int id)
        {
            var mustr = db.tbl_Musteriler.Find(id);
            return View("MusteriGetir", mustr);
        }
        public ActionResult GuncelleMusteri(tbl_Musteriler p1)
        {
            var mustr = db.tbl_Musteriler.Find(p1.MusteriId);
            mustr.MusteriAd = p1.MusteriAd;
            mustr.MusteriSoyad = p1.MusteriSoyad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}