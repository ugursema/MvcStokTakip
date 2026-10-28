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
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            // var degerler = db.tbl_Urunler.ToList();
            var degerler = db.tbl_Urunler.ToList().ToPagedList(1, 5);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {

            List<SelectListItem> degerler = (from i in db.tbl_Kategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd,
                                                 Value = i.KategoriId.ToString()

                                             }).ToList();

            ViewBag.dgr = degerler;
            return View();

        }


        [HttpPost]
        public ActionResult YeniUrun(tbl_Urunler p1)
        {
            var ktg = db.tbl_Kategoriler.Where(m => m.KategoriId == p1.tbl_Kategoriler.KategoriId).FirstOrDefault();
            p1.tbl_Kategoriler = ktg;
            db.tbl_Urunler.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Sil(int id)
        {
            var urun = db.tbl_Urunler.Find(id);
            db.tbl_Urunler.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult UrunGetir(int id)
        {
            var urun = db.tbl_Urunler.Find(id);
            List<SelectListItem> degerler = (from i in db.tbl_Kategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd,
                                                 Value = i.KategoriId.ToString()

                                             }).ToList();

            ViewBag.dgr = degerler;
            return View("UrunGetir", urun);
        }
        public ActionResult GuncelleUrun(tbl_Urunler p)
        {
            var urun = db.tbl_Urunler.Find(p.UrunId);
            urun.UrunAd = p.UrunAd;
            urun.UrunMarkasi = p.UrunMarkasi;
            //urun.UrunKategori = p.UrunKategori;
            var ktg = db.tbl_Kategoriler.Where(m => m.KategoriId == p.tbl_Kategoriler.KategoriId).FirstOrDefault();
            urun.UrunKategori = ktg.KategoriId;
            urun.UrunFiyat = p.UrunFiyat;
            urun.UrunStok = p.UrunStok;
         
         
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}