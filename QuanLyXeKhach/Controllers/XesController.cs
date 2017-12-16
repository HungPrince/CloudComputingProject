using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HelloWorld.Models;

namespace HelloWorld.Controllers
{
    public class XesController : Controller
    {
        private QLXeKhachContext db = new QLXeKhachContext();

        public ActionResult DanhSachXe()
        {
            var xes = db.Xes.Include(x => x.NhanVien);
            return View(xes.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xe xe = db.Xes.Find(id);
            if (xe == null)
            {
                return HttpNotFound();
            }
            return View(xe);
        }

        public ActionResult Create()
        {
            ViewBag.ChuSoHuu = new SelectList(db.NhanViens, "MaNV", "HoTen");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Maxe,Ten,BienSo,Mota,SoDienThoai,ChuSoHuu,GioDi,GioVe,DiemDi,DiemVe")] Xe xe)
        {
            if (ModelState.IsValid)
            {
                db.Xes.Add(xe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ChuSoHuu = new SelectList(db.NhanViens, "MaNV", "HoTen", xe.ChuSoHuu);
            return View(xe);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xe xe = db.Xes.Find(id);
            if (xe == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChuSoHuu = new SelectList(db.NhanViens, "MaNV", "HoTen", xe.ChuSoHuu);
            return View(xe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Maxe,Ten,BienSo,Mota,SoDienThoai,ChuSoHuu,GioDi,GioVe,DiemDi,DiemVe")] Xe xe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(xe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ChuSoHuu = new SelectList(db.NhanViens, "MaNV", "HoTen", xe.ChuSoHuu);
            return View(xe);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xe xe = db.Xes.Find(id);
            if (xe == null)
            {
                return HttpNotFound();
            }
            return View(xe);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Xe xe = db.Xes.Find(id);
            db.Xes.Remove(xe);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
