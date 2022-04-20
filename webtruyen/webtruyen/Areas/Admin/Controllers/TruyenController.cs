using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webtruyen.Models;

namespace webtruyen.Areas.Admin.Controllers
{
    public class TruyenController : Controller
    {
        // GET: Admin/Truyen
        DB myDb = new DB();
        public ActionResult ds_truyen()
        {
            List<TRUYEN> listTruyen = myDb.TRUYENs.ToList();
            return View(listTruyen);
        }
        public ActionResult Details (int matruyen)
        {
            TRUYEN truyen = myDb.TRUYENs.FirstOrDefault(s => s.MATRUYEN == matruyen);
            return View(truyen);
        }
        public ActionResult Create()
        {
            if (Request.Form.Count > 0)
            {
                TRUYEN tr = new TRUYEN();
                tr.TENTRUYEN = Request.Form["TENTRUYEN"];
                tr.MATG = int.Parse(Request.Form["MATG"]);
                tr.MATHELOAI = int.Parse(Request.Form["MATHELOAI"]);
                tr.LUOTXEM = int.Parse(Request.Form["LUOTXEM"]);
                tr.NGUON = Request.Form["NGUON"];
                HttpPostedFileBase file = Request.Files["HINHANH"];
                if (file != null)
                {
                    string serverPath = HttpContext.Server.MapPath("~/Content/HINH");
                    string filePath = serverPath + "/" + file.FileName;
                    try
                    {
                        file.SaveAs(filePath);
                        tr.HINHANH = file.FileName;
                    }
                    catch (Exception) { }
                }
                else
                {
                    tr.HINHANH = "rỗng";
                }
                tr.MOTA = Request.Form["MOTA"];
                myDb.TRUYENs.Add(tr);
                myDb.SaveChanges();
                return RedirectToAction("ds_truyen");

            }
            return View();
        }
        public ActionResult Edit(int matruyen)
        {
            TRUYEN tr = myDb.TRUYENs.FirstOrDefault(s => s.MATRUYEN == matruyen);
            if (Request.Form.Count > 0)
            {

                
                tr.TENTRUYEN = Request.Form["TENTRUYEN"];
                tr.MATG = int.Parse(Request.Form["MATG"]);
                tr.MATHELOAI = int.Parse(Request.Form["MATHELOAI"]);
                tr.LUOTXEM = int.Parse(Request.Form["LUOTXEM"]);
                tr.NGUON = Request.Form["NGUON"];
                HttpPostedFileBase file = Request.Files["HINHANH"];
                if (file != null)
                {
                    string serverPath = HttpContext.Server.MapPath("~/Content/HINH");
                    string filePath = serverPath + "/" + file.FileName;
                    try
                    {
                        file.SaveAs(filePath);
                        tr.HINHANH = file.FileName;
                    }
                    catch (Exception) { }
                }
                else
                {
                    tr.HINHANH = "rỗng";
                }
                tr.MOTA = Request.Form["MOTA"];
                myDb.SaveChanges();
                return RedirectToAction("ds_truyen");
            }
            return View(tr);
        }
        public ActionResult Delete (int matruyen)
        {
            TRUYEN tr = myDb.TRUYENs.FirstOrDefault(s => s.MATRUYEN == matruyen);
            if (tr != null)
            {
                myDb.TRUYENs.Remove(tr);
                myDb.SaveChanges();
            }
            return RedirectToAction("ds_truyen");
        }
    }
}