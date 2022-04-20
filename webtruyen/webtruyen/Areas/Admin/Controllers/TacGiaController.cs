using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webtruyen.Models;

namespace webtruyen.Areas.Admin.Controllers
{
    public class TacGiaController : Controller
    {
        // GET: Admin/TacGia
        DB myDb = new DB();
        public ActionResult ds_Tg()
        {
            List<TACGIA> Tg = myDb.TACGIAs.ToList();
            return View(Tg);
        }
        public ActionResult Create()
        {
            if (Request.Form.Count > 0)
            {
                
                string tenTg = Request.Form["TEN_TG"];
                string bidanh = Request.Form["BIDANH"];
                string mota = Request.Form["MOTA"];
                TACGIA Tg = new TACGIA();
               
                Tg.TEN_TG = tenTg;
                Tg.BIDANH = bidanh;
                Tg.MOTA = mota;
                myDb.TACGIAs.Add(Tg);
                myDb.SaveChanges();
                return RedirectToAction("ds_Tg");

            }
            return View();
        }
        public ActionResult Edit(int maTg)
        {
            TACGIA Tg = myDb.TACGIAs.FirstOrDefault(p => p.MATG == maTg);
            if (Request.Form.Count > 0)
            {
                string tenTg = Request.Form["TEN_TG"];
                string bidanh = Request.Form["BIDANH"];
                string mota = Request.Form["MOTA"];
                Tg.MATG = maTg;
                Tg.TEN_TG = tenTg;
                Tg.BIDANH = bidanh;
                Tg.MOTA = mota;
                myDb.SaveChanges();
                return RedirectToAction("ds_Tg");
            }
            return View(Tg);
        }
        public ActionResult Delete(int maTg)
        {
            TACGIA Tg = myDb.TACGIAs.FirstOrDefault(p => p.MATG == maTg);
            if (Tg != null)
            {
                myDb.TACGIAs.Remove(Tg);
                myDb.SaveChanges();
            }
            return RedirectToAction("ds_Tg");
        }
    }
}