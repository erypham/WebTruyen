using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webtruyen.Models;

namespace webtruyen.Areas.Admin.Controllers
{
    public class ChuongTruyenController : Controller
    {
        // GET: Admin/ChuongTruyen
        DB myDb = new DB();
        public ActionResult ds_chuong()
        {
            List<CHUONGTRUYEN> listChuong = myDb.CHUONGTRUYENs.ToList();
            return View(listChuong);
        }
        public ActionResult Details(int machuong)
        {
            CHUONGTRUYEN chtruyen = myDb.CHUONGTRUYENs.FirstOrDefault(s => s.MACHUONG == machuong);
            return View(chtruyen);
        }
        public ActionResult Create()
        {
            if (Request.Form.Count > 0)
            {
                CHUONGTRUYEN chtr = new CHUONGTRUYEN();
    
                chtr.TENCHUONG = Request.Form["TENCHUONG"];
                chtr.TENPHU =Request.Form["TENPHU"];
                chtr.NOIDUNG = Request.Form["NOIDUNG"];
                chtr.LUOTXEM = int.Parse(Request.Form["LUOTXEM"]);
                chtr.MATRUYEN = int.Parse(Request.Form["MATRUYEN"]);
                myDb.CHUONGTRUYENs.Add(chtr);
                myDb.SaveChanges();
                return RedirectToAction("ds_chuong");
            }
            return View();
        }
        public ActionResult Edit(int machuong)
        {
            CHUONGTRUYEN chtr = myDb.CHUONGTRUYENs.FirstOrDefault(s => s.MACHUONG == machuong);
            if (Request.Form.Count > 0)
            {
  
                chtr.TENCHUONG = Request.Form["TENCHUONG"];
                chtr.TENPHU = Request.Form["TENPHU"];
                chtr.NOIDUNG = Request.Form["NOIDUNG"];
                chtr.LUOTXEM = int.Parse(Request.Form["LUOTXEM"]);
                chtr.MATRUYEN = int.Parse(Request.Form["MATRUYEN"]);
                myDb.SaveChanges();
                return RedirectToAction("ds_chuong");
            }
            return View(chtr);
        }
        public ActionResult Delete(int machuong)
        {
            CHUONGTRUYEN chtr = myDb.CHUONGTRUYENs.FirstOrDefault(s => s.MACHUONG == machuong);
            if (chtr != null)
            {
                myDb.CHUONGTRUYENs.Remove(chtr);
                myDb.SaveChanges();
            }
            return RedirectToAction("ds_chuong");
        }
    }
}