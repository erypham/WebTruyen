using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webtruyen.Models;

namespace webtruyen.Areas.Admin.Controllers
{
    public class TaiKhoanController : Controller
    {
        // GET: Admin/TaiKhoan
        DB myDb = new DB();
        public ActionResult ds_Tk()
        {
            List<TAIKHOAN> listTK = myDb.TAIKHOANs.ToList();
            return View(listTK);
        }
        public ActionResult Details(int maTK)
        {
            TAIKHOAN tk = myDb.TAIKHOANs.FirstOrDefault(a => a.MATK == maTK);
            return View(tk);
        }
        public ActionResult Create()
        {
            if (Request.Form.Count > 0)
            {
               
                int maLoaiTK = Convert.ToInt32(Request.Form["MALOAITK"]);
                string tenNguoiDung = Request.Form["TEN_ND"];
                string email = Request.Form["EMAIL"];
                string sdt = Request.Form["SDT_ND"];
                string matkhau = Request.Form["PASSWORD_USER"];
                string otb = Request.Form["OTB"];
                TAIKHOAN TK = new TAIKHOAN();
               
                TK.MALOAITK = maLoaiTK;
                TK.TEN_ND = tenNguoiDung;
                TK.EMAIL = email;
                TK.SDT_ND = sdt;
                TK.PASSWORD_USER = matkhau;
                TK.OTB = otb;
                myDb.TAIKHOANs.Add(TK);
                myDb.SaveChanges();
                return RedirectToAction("ds_Tk");

            }
            return View();
        }
        public ActionResult Edit(int maTK)
        {
            TAIKHOAN TK = myDb.TAIKHOANs.FirstOrDefault(p => p.MATK == maTK);
            if (Request.Form.Count > 0)
            {
                int maLoaiTK = Convert.ToInt32(Request.Form["MALOAITK"]);
                string tenNguoiDung = Request.Form["TEN_ND"];
                string email = Request.Form["EMAIL"];
                string sdt = Request.Form["SDT_ND"];
                string matkhau = Request.Form["PASSWORD_USER"];
                string otb = Request.Form["OTB"];
                TK.MATK = maTK;
                TK.MALOAITK = maLoaiTK;
                TK.TEN_ND = tenNguoiDung;
                TK.EMAIL = email;
                TK.SDT_ND = sdt;
                TK.PASSWORD_USER = matkhau;
                TK.OTB = otb;
                myDb.SaveChanges();
                return RedirectToAction("ds_Tk");
            }
            return View(TK);
        }
        public ActionResult Delete(int maTK)
        {
            TAIKHOAN TK = myDb.TAIKHOANs.FirstOrDefault(p => p.MATK == maTK);
            if (TK != null)
            {
                myDb.TAIKHOANs.Remove(TK);
                myDb.SaveChanges();
            }
            return RedirectToAction("ds_Tk");
        }
    }
}