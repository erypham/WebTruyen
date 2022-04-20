using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webtruyen.Controllers
{
    public class KhangTestController : Controller
    {
        // GET: KhangTest
        public ActionResult Index()
        {
            return View();
        }

        // GET: KhangTest/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: KhangTest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KhangTest/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: KhangTest/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: KhangTest/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: KhangTest/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: KhangTest/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
