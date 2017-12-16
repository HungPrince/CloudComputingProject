using HelloWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloWorld.Controllers
{
    public class SoDienThoaiController : Controller
    {
        private QLXeKhachContext db = new QLXeKhachContext();
        public SoDienThoai SoDThoai;
        // GET: DienThoai
        public ActionResult Index()
        {
            return View(db.SoDienThoais.ToList());
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(FormCollection frm)
        {
            SoDThoai = new SoDienThoai();
            SoDThoai.SoDT = frm["SoDT"].ToString();
            if (SoDThoai != null)
            {
                db.SoDienThoais.Add(SoDThoai);
                db.SaveChanges();
                return View("Index");
            }
            return View();
        }
    }
}