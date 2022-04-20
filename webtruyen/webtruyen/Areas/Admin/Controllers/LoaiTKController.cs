using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webtruyen.Models;

namespace webtruyen.Areas.Admin.Controllers
{
    public class LoaiTKController : Controller
    {
        // GET: Admin/LoaiTK

        private DB myDb = new DB();
        public ActionResult ds_loaiTK()
        {
            List<LOAITAIKHOAN> listLoaiTK = myDb.LOAITAIKHOANs.ToList();
            return View(listLoaiTK);
        }

        public ActionResult Create()
        {
            if (Request.Form.Count > 0)
            {
                
                string tenloaiTK = Request.Form["TENLOAITAIKHOAN"];

                LOAITAIKHOAN loaiTK = new LOAITAIKHOAN();
               
                loaiTK.TENLOAITAIKHOAN = tenloaiTK;
                myDb.LOAITAIKHOANs.Add(loaiTK);
                myDb.SaveChanges();
                return RedirectToAction("ds_loaiTK");

            }
            return View();
        }
        public ActionResult Edit(int maLoaiTK)
        {
            LOAITAIKHOAN loaiTK = myDb.LOAITAIKHOANs.FirstOrDefault(p => p.MALOAITK == maLoaiTK);
            if (Request.Form.Count > 0)
            {
                string tenLoaiTK = Request.Form["TENLOAITAIKHOAN"];
                loaiTK.TENLOAITAIKHOAN = tenLoaiTK;
                myDb.SaveChanges();
                return RedirectToAction("ds_loaiTK");
            }
            return View(loaiTK);
        }
        public ActionResult Delete(int maLoaiTK)
        {
            LOAITAIKHOAN loaiTK = myDb.LOAITAIKHOANs.FirstOrDefault(p => p.MALOAITK == maLoaiTK);
            if (loaiTK != null)
            {
                myDb.LOAITAIKHOANs.Remove(loaiTK);
                myDb.SaveChanges();
            }
            return RedirectToAction("ds_loaiTK");
        }
    }
}