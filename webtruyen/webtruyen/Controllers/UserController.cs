using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using webtruyen.Models;
using static webtruyen.Controllers.SendEmailController;

namespace webtruyen.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        DB myDb = new DB();

        public static string Encrypt(string toEncrypt)
        {
            byte[] input = Encoding.ASCII.GetBytes(toEncrypt);
            var sha = new SHA512Managed();
            var hash = sha.ComputeHash(input);
            StringBuilder sb = new StringBuilder();

            foreach (byte l in hash)
            {
                sb.Append(l.ToString("X2"));
            }

            return sb.ToString();
        }

        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy([Bind(Include = "EMAIL,TEN_ND,PASSWORD_USER,MALOAITK,OTB")] FormCollection userlog, TAIKHOAN model)
        {
            if (ModelState.IsValid)
            {
                string check = userlog["Email"].ToString();
                var userExist = myDb.TAIKHOANs.SingleOrDefault(x => x.EMAIL.Equals(check));
                if (userExist == null)
                {
                    string x = Request.Form["Password"];
                    string y = Request.Form["ConfirmPassword"];
                    if (x == y)
                    {
                        model.EMAIL = Request.Form["Email"];
                        model.TEN_ND = Request.Form["DisplayName"];
                        model.PASSWORD_USER = Encrypt(x);
                        model.MALOAITK = 5;

                        Random rand = new Random((int)DateTime.Now.Ticks);
                        int numIterations = 0;
                        numIterations = rand.Next(100000, 999999);
                        model.OTB = numIterations.ToString();

                        myDb.TAIKHOANs.Add(model);
                        myDb.SaveChanges();

                        try
                        {
                            if (ModelState.IsValid)
                            {
                                EmailService service = new EmailService();
                                bool kq = service.SendEmail(model.EMAIL, "Mã xác thực đăng nhập", numIterations.ToString());
                                TAIKHOAN tk = myDb.TAIKHOANs.FirstOrDefault(p => p.EMAIL == model.EMAIL);
                                return RedirectToAction("VerifyOTB", "User", new { matk = tk.MATK });
                            }
                            return View("DangKy");
                        }
                        catch
                        {
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Fail = "Mật khẩu xác nhận không đúng!";
                        return View("DangKy");
                    }
                }
                else
                {
                    ViewBag.Fail = "Email đã tồn tại!";
                    return View("DangKy");
                }
            }
            return View(model);
        }

        public ActionResult VerifyOTB(FormCollection userlog, int matk)
        {
            TAIKHOAN tk = myDb.TAIKHOANs.FirstOrDefault(s => s.MATK == matk);
            if (Request.Form.Count > 0)
            {
                string otb = userlog["OTB"].ToString();
                var verify = myDb.TAIKHOANs.SingleOrDefault(x => x.OTB.Equals(otb));
                if (verify != null)
                {
                    tk.MALOAITK = 2;
                    tk.OTB = "1";
                    myDb.SaveChanges();
                    return RedirectToAction("trangChu", "TrangChu");
                }
            }
            return View(tk);
        }

        public ActionResult DangNhap()
        {
            if (Session["use"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("trangChu", "TrangChu");
            }
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection userlog)
        {
            string userMail = userlog["userMail"].ToString();
            string password = userlog["password"].ToString();

            string en = Encrypt(password);

            var islogin = myDb.TAIKHOANs.SingleOrDefault(x => x.EMAIL.Equals(userMail) && x.PASSWORD_USER.Equals(en));
            if (islogin != null)
            {
                var checkVerified = myDb.TAIKHOANs.SingleOrDefault(x => x.EMAIL.Equals(userMail) && x.MALOAITK == 1);
                var checkVerified2 = myDb.TAIKHOANs.SingleOrDefault(x => x.EMAIL.Equals(userMail) && x.MALOAITK == 2);
                if (checkVerified != null || checkVerified2 != null)
                {
                    var noAdmin = myDb.TAIKHOANs.SingleOrDefault(x => x.EMAIL.Equals(userMail) && x.PASSWORD_USER.Equals(en) && x.MALOAITK == 1);
                    if (noAdmin != null)
                    {
                        Session["use"] = islogin;
                        Session["loai"] = noAdmin;
                        Session["email"] = userMail;
                        return RedirectToAction("Index", "Admin/Admin");
                    }
                    else
                    {
                        Session["use"] = islogin;
                        Session["email"] = userMail;
                        return RedirectToAction("trangChu", "TrangChu");
                    }
                }
                else
                {
                    TAIKHOAN model = myDb.TAIKHOANs.FirstOrDefault(s => s.EMAIL == userMail);
                    Random rand = new Random((int)DateTime.Now.Ticks);
                    int numIterations = 0;
                    numIterations = rand.Next(100000, 999999);
                    model.OTB = numIterations.ToString();

                    myDb.SaveChanges();

                    EmailService service = new EmailService();
                    bool kq = service.SendEmail(model.EMAIL, "Mã xác thực đăng nhập", numIterations.ToString());

                    return RedirectToAction("VerifyOTB", "User", new { matk = model.MATK });
                }
            }
            else
            {
                ViewBag.Fail = "Đăng nhập thất bại";
                return View("DangNhap");
            }
        }

        public ActionResult DoiMatKhau()
        {
            if (Session["use"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("trangChu", "TrangChu");
            }
        }

        [HttpPost]
        public ActionResult DoiMatKhau(FormCollection userlog)
        {
            var userMail = Session["email"];
            string password = userlog["Password"].ToString();
            string newPassword = userlog["newPassword"].ToString();
            string confirmNewPassword = userlog["confirmNewPassword"].ToString();

            string enOld = Encrypt(password);

            var isEmail = myDb.TAIKHOANs.SingleOrDefault(x => x.EMAIL.Equals(userMail.ToString()));
            if (isEmail != null)
            {
                var checkPass = myDb.TAIKHOANs.SingleOrDefault(x => x.EMAIL.Equals(userMail.ToString()) && x.PASSWORD_USER.Equals(enOld));
                if (checkPass != null)
                {
                    if (password != newPassword)
                    {
                        if (newPassword == confirmNewPassword)
                        {
                            TAIKHOAN tk = myDb.TAIKHOANs.FirstOrDefault(p => p.EMAIL == userMail.ToString());

                            string enNew = Encrypt(newPassword);
                            tk.PASSWORD_USER = enNew;
                            myDb.SaveChanges();
                            return RedirectToAction("trangChu", "TrangChu");
                        }
                        else
                        {
                            ViewBag.Fail = "Mật khẩu xác nhận không đúng!";
                            return View("DoiMatKhau");
                        }
                    }
                    else
                    {
                        ViewBag.Fail = "Mật khẩu mới không được trùng với mật khẩu cũ!";
                        return View("DoiMatKhau");
                    }
                }
                else
                {
                    ViewBag.Fail = "Mật khẩu cũ của bạn không đúng!";
                    return View("DoiMatKhau");
                }
            }
            else
            {
                ViewBag.Fail = "Lỗi của tui!";
                return View("DoiMatKhau");
            }
        }

        public ActionResult QuenMK()
        {
            return View();
        }

        [HttpPost]
        public ActionResult QuenMK(FormCollection userlog)
        {
            string userMail = userlog["userMail"].ToString();

            var gettk = myDb.TAIKHOANs.FirstOrDefault(s => s.EMAIL == userMail);
            if (gettk != null)
            {
                TAIKHOAN model = myDb.TAIKHOANs.FirstOrDefault(s => s.EMAIL == userMail);
                Random rand = new Random((int)DateTime.Now.Ticks);
                int numIterations = 0;
                numIterations = rand.Next(100000, 999999);
                model.OTB = numIterations.ToString();

                myDb.SaveChanges();

                EmailService service = new EmailService();
                bool kq = service.SendEmail(model.EMAIL, "Mã xác thực đăng nhập", numIterations.ToString());

                return RedirectToAction("QuenMKDoi", "User", new { matk = model.MATK });
            }
            else
            {
                ViewBag.Fail = "Không tồn tại Email này trong hệ thống!";
                return View();
            }
        }

        public ActionResult QuenMKDoi(FormCollection userlog, int matk)
        {
            TAIKHOAN tkMa = myDb.TAIKHOANs.FirstOrDefault(s => s.MATK == matk);
            if (Request.Form.Count > 0)
            {
                string OTB = userlog["OTB"].ToString();
                string newPassword = userlog["newPassword"].ToString();
                string confirmNewPassword = userlog["confirmNewPassword"].ToString();


                var isOTB = myDb.TAIKHOANs.SingleOrDefault(x => x.OTB.Equals(OTB));
                if (isOTB != null)
                {
                    if (newPassword == confirmNewPassword)
                    {
                        TAIKHOAN tk = myDb.TAIKHOANs.FirstOrDefault(p => p.MATK == matk);

                        string enOld = Encrypt(newPassword);
                        tk.PASSWORD_USER = enOld;
                        tk.OTB = "1";
                        myDb.SaveChanges();
                        return RedirectToAction("trangChu", "TrangChu");
                    }
                    else
                    {
                        ViewBag.Fail = "Mật khẩu xác nhận không đúng!";
                        return View("QuenMKDoi");
                    }
                }
                else
                {
                    ViewBag.Fail = "Mã xác nhận bạn nhập không chính xác!";
                    return View("QuenMKDoi");
                }
            }
            return View(tkMa);
        }

        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("trangChu", "TrangChu");
        }
    }
}