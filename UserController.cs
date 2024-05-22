using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Project_Tour_and_Travel.Models;


namespace Web_Project_Tour_and_Travel.Controllers
{
    public class UserController : Controller
    {
        cs c = new cs();
        // GET: User
        public ActionResult Home()
        {
            var packages = c.Packages.ToList();
            return View(packages);
            
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Packages()
        {
            var x = c.Packages.ToList();
            return View(x);
        }

        public ActionResult Services()
        {
            var services = c.Servicess.ToList(); // Assuming 'c' is your DbContext and 'Services' is your DbSet<Service>
            return View(services);
        }

        public ActionResult PrivacyPolicy()
        {
            return View();
        }

        public ActionResult RefundPolicy()
        {
            return View();
        }

        public ActionResult Terms()
        {
            return View();
        }

        public ActionResult Reserve()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Reserve(Reservation r)
        {
            c.Reservations.Add(r);
            c.SaveChanges();
            Session["rid"] = r.RegId;
            return Json(new { success = true });
        }


        public ActionResult Payment()
        {
            return View();
               
        }

        [HttpPost]

        public ActionResult Payment(string cnum)
        {
            int id = (int)Session["rid"];
            Payment p = new Payment();
            p.paymentMode = cnum;
            p.RegId = id;
            c.Payments.Add(p);
            c.SaveChanges();
            //Session.Remove("rid");
            ViewBag.ShowPopup = true;
            return RedirectToAction("Packages");
        }

        public ActionResult Deal1()
        {
            return View();
        }
        public ActionResult Deal2()
        {
            return View();
        }
        public ActionResult Deal3()
        {
            return View();
        }
    }
}