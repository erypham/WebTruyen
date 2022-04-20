using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webtruyen.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
       
        public ActionResult Index()
        {
            if (Session["loai"] == null)
            {
                return RedirectToAction("TrangChu", "trangChu", new { area = "" });
            }
            else
            {
                return View();
            }
        }

        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("TrangChu", "trangChu", new { area = "" });
        }
    }
}