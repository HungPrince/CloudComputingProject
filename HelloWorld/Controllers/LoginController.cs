using ASPSnippets.GoogleAPI;
using System.Web.Script.Serialization;
using System.Net;
using System.Web.Mvc;
using HelloWorld.Models;
using System.Linq;
using System;
using System.Net.Http;
using System.Web.Helpers;

namespace HelloWorld.Controllers
{
    public class LoginController : Controller
    {
        HttpClient client;
        public LoginController()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
        private QLXeKhachContext db = new QLXeKhachContext();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void LoginWithGooglePlus()
        {

            GoogleConnect.ClientId = "963927118796-8at56h23jslrn3efdm544ehh11j2n1el.apps.googleusercontent.com";
            GoogleConnect.ClientSecret = "JtGwNPMJPnCnLF9gxTFIRqln";
            GoogleConnect.RedirectUri = Request.Url.AbsoluteUri.Split('?')[0];
            GoogleConnect.Authorize("profile", "email");
        }

        [ActionName("LoginWithGooglePlus")]
        public ActionResult LoginWithGooglePlusConfirmed()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["code"]))
            {
                string code = Request.QueryString["code"];
                string json = GoogleConnect.Fetch("me", code);
                GoogleUser profile = new JavaScriptSerializer().Deserialize<GoogleUser>(json);
                try
                {
                    var nvien = db.NhanViens.Where(nv => nv.IdGG == profile.Id).SingleOrDefault();

                    if (nvien != null)
                    {
                        Session["nhanvien"] = nvien;
                        Session["TaiKhoan"] = nvien.HoTen;
                        Session["image"] = nvien.ImageURL;
                        return RedirectToAction("Index", "Home");
                    }
                    NhanVien nhanvien = new NhanVien();
                    nhanvien.IdGG = profile.Id.ToString();
                    nhanvien.Email = profile.Emails.Find(email => email.Type == "account").Value;
                    nhanvien.HoTen = profile.DisplayName;
                    nhanvien.ImageURL = profile.Id + System.IO.Path.GetExtension(profile.Image.Url.Split('?')[0]);

                    using (var client = new WebClient())
                    {
                        client.DownloadFile(profile.Image.Url, Server.MapPath("~/Images/Users/" + nhanvien.ImageURL));
                    }
                    Session["nhanvien"] = nhanvien;
                    Session["image"] = nhanvien.ImageURL;
                    Session["TaiKhoan"] = nhanvien.HoTen;
                    db.NhanViens.Add(nhanvien);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }

            }
            if (Request.QueryString["error"] == "access_denied")
            {
                return Content("access_denied");
            }
            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public JsonResult Login(string email, string password)
        {
            var Password = Utility.ToSHA512(password);
            var user = db.NhanViens.Where(u => u.Email == email && u.MatKhau == Password).SingleOrDefault();
            if(user != null)
            {
                Session["TaiKhoan"] = user.HoTen;
                Session["image"] = user.ImageURL != null ? user.ImageURL : "";
                return Json(new { status = true, message = "Đăng nhập thành công!" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false, message = "Sai mật khẩu hoặc password!" }, JsonRequestBehavior.AllowGet);
        }
    }
}