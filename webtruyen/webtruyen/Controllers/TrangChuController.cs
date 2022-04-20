using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webtruyen.Models;

namespace webtruyen.Controllers
{
    public class TrangChuController : Controller
    {
        // GET: TrangChu
        DB myDb = new DB();
        public ActionResult trangChu()
        {
            var LIS = (from L in myDb.TRUYENs where L.MATHELOAI == 1 select L).ToList();
            ViewBag.listTruyenMoiNhat = LIS;
            var LIS2 = (from L in myDb.TRUYENs where L.MATHELOAI == 1 select L).ToList();
            ViewBag.listTruyenFull = LIS2;
            var LIS3 = (from L in myDb.TRUYENs where L.MATHELOAI == 1 select L).ToList();
            ViewBag.listTruyenSangTac = LIS3;
            return View();
        }
        public ActionResult ds_theloai()
        {
            var lstTL = myDb.TRUYENs;
            return PartialView(lstTL);
        }
        public ActionResult truyenPartial()
        {
            return PartialView(myDb.TRUYENs);
        }
        public ActionResult Details(int matruyen)
        {
            TRUYEN tr = myDb.TRUYENs.FirstOrDefault(s => s.MATRUYEN == matruyen);
           
            myDb.SaveChanges();
            return View(tr);
        }
        public ActionResult lstTruyen(int matl)
        {
            var lstTruyen = myDb.TRUYENs.Where(n => n.MATHELOAI == matl);
            return View(lstTruyen);
        }
        public ActionResult lstTruyenPartial()
        {
            return PartialView(myDb.TRUYENs);
        }
        public ActionResult chTruyenPartial(int matruyen)
        {

            return PartialView(myDb.CHUONGTRUYENs.Where(n => n.MATRUYEN == matruyen));
        }
        public ActionResult DetailChuong(int machuong)
        {
            CHUONGTRUYEN DetailCh = myDb.CHUONGTRUYENs.FirstOrDefault(a => a.MACHUONG == machuong);
            if (DetailCh.LUOTXEM != null)
            {
                DetailCh.LUOTXEM += 1;
            }
            else
            {
                DetailCh.LUOTXEM = 1;
            }
            TRUYEN tr = myDb.TRUYENs.FirstOrDefault(n => n.MATRUYEN == DetailCh.MATRUYEN);
            if (tr.LUOTXEM != null)
            {
                tr.LUOTXEM += 1;
            }
            else
            {
                tr.LUOTXEM = 1;
            }
            myDb.SaveChanges();
            return View(DetailCh);
        }
        public ActionResult ds_theloai2()
        {
            var lstTL = myDb.TRUYENs;
            return PartialView(lstTL);
        }
    }
}