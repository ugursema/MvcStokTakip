using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStokTakip.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStokTakip.Controllers
{
    public class KategoriController : Controller
    {
        // Veritabanı bağlantı nesnesini oluşturuyoruz (Entity Framework)
        MvcDbStokEntities db = new MvcDbStokEntities();

        // Kategorileri listeleyen ana sayfa
        public ActionResult Index(int sayfa=1)
        {
            // Veritabanındaki tüm kategorileri çekip listeye dönüştürür
            //var degerler = db.tbl_Kategoriler.ToList();
            var degerler = db.tbl_Kategoriler.ToList().ToPagedList(sayfa, 5);

            // Çekilen kategori listesini ekrana (View) gönderir
            return View(degerler);
        }

        // Yeni kategori ekleme sayfasını sadece görüntülemek için çalışır (Sayfa ilk açıldığında)
        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }

        // Ekle butonuna basıldığında (Form gönderildiğinde) çalışır
        [HttpPost]
        public ActionResult YeniKategori(tbl_Kategoriler p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniKategori");
            }
            // Formdan gelen yeni kategori bilgilerini hafızaya ekler
            db.tbl_Kategoriler.Add(p1);

            // Değişiklikleri veritabanına kalıcı olarak kaydeder
            db.SaveChanges();

            // Ekranı tekrar gösterir
            return View();
        }

        public ActionResult Sil(int id)
        {
            var kategori = db.tbl_Kategoriler.Find(id);
            db.tbl_Kategoriler.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.tbl_Kategoriler.Find(id);
            return View("KategoriGetir", ktgr);
        }

        public ActionResult Guncelle(tbl_Kategoriler p1)
        {
            var ktg = db.tbl_Kategoriler.Find(p1.KategoriId);
            ktg.KategoriAd = p1.KategoriAd;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}