using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Project_Tour_and_Travel.Models;
using System.IO;
using System.Data.Entity;

namespace Web_Project_Tour_and_Travel.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        cs c = new cs();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Login l)
        {
            var x = c.Logins.FirstOrDefault(a => a.Email.Equals(l.Email) && a.Password.Equals(l.Password));
            if (x != null)
            {
                return View("AdminDashboard");
            }
            else
            {
                ViewBag.m = "Wrong user id or password";
                return View();
            }
            //return View();
        }
        [HttpGet]
        public ActionResult Reg()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Reg(Login l)
        {
            c.Logins.Add(l);
            c.SaveChanges();
            return View();
        }
        
        public ActionResult AddPkg()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPkg(Pkg p, string Offers, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                string fileName = Path.GetFileName(image.FileName);
                string relativePath = "~/images/" + fileName; // Save relative path
                string absolutePath = Server.MapPath(relativePath);
                image.SaveAs(absolutePath);

                p.Image = relativePath; // Save relative path to the database
            }

            p.Offer = Offers;
            c.Packages.Add(p);
            c.SaveChanges();
            ViewBag.pk = "Package Added Successfully";
            ModelState.Clear();
            return View("AddPkg");
        }


       
        public ActionResult PackageList()
        {
            var packages = c.Packages.ToList();
            return View(packages);
        }

        [HttpPost]
        public ActionResult DeletePackage(int id)
        {
            var package = c.Packages.Find(id);
            if (package != null)
            {
                c.Packages.Remove(package);
                c.SaveChanges();
            }
            // Get the updated list of packages after deletion
            var packages = c.Packages.ToList();
            // Pass the updated list of packages to the view
            return View("PackageList", packages);
        }

        [HttpGet]
        public ActionResult UpdatePackage(int id)
        {
            var package = c.Packages.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        [HttpPost]
        public ActionResult UpdatePackage(Pkg package, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (image != null && image.ContentLength > 0)
                    {
                        // Save the image to the server and update the package with the image path
                        string fileName = Path.GetFileName(image.FileName);
                        string imagePath = Path.Combine(Server.MapPath("~/Images"), fileName);
                        image.SaveAs(imagePath);
                        package.Image = "~/Images/" + fileName;
                    }

                    c.Entry(package).State = EntityState.Modified;
                    c.SaveChanges();
                    return RedirectToAction("PackageList");
                }
                catch (Exception ex)
                {
                    // Handle exception
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the package.");
                    return View(package);
                }
            }
            // If ModelState is not valid, return the view with validation errors
            return View(package);
        }

        public ActionResult Services()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Services(Services ser, String service, HttpPostedFileBase image)
        {
            //string ImageTest = null;

            string PathM = "~/Images/" + image.FileName;
            string pic = Path.Combine(Server.MapPath("~/images"), image.FileName);
            image.SaveAs(pic);

            ser.Image = pic;
            ser.Image = PathM;
            ser.Service = service;
            c.Servicess.Add(ser);
            c.SaveChanges();
            ViewBag.sv = "Service Added Succesfully";
            ModelState.Clear();
            return View("Services");

        }

        public ActionResult ServiceList()
        {
            var services = c.Servicess.ToList();
            return View(services);
        }

        [HttpPost]
        public ActionResult DeleteService(int id)
        {
            var service = c.Servicess.Find(id);
            if (service != null)
            {
                c.Servicess.Remove(service);
                c.SaveChanges();
            }
            // Get the updated list of services after deletion
            var services = c.Servicess.ToList();
            // Pass the updated list of services to the view
            return View("ServiceList", services);
        }

        [HttpGet]
        public ActionResult UpdateService(int id)
        {
            var service = c.Servicess.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        [HttpPost]
        public ActionResult UpdateService(Services service22, HttpPostedFileBase image)
        {

            try
            {
                if (image != null && image.ContentLength > 0)
                {
                    // Save the image to the server and update the service with the image path
                    string fileName = Path.GetFileName(image.FileName);
                    string imagePath = Path.Combine(Server.MapPath("~/Images"), fileName);
                    image.SaveAs(imagePath);
                    service22.Image = "~/Images/" + fileName;
                }

                c.Entry(service22).State = EntityState.Modified;
                c.SaveChanges();
                return RedirectToAction("ServiceList"); // Assuming you have an action method named ServiceList to display the list of services
            }
            catch (Exception ex)
            {
                // Handle exception
                ModelState.AddModelError(string.Empty, "An error occurred while updating the service.");
                return View(service22);
            }

            // If ModelState is not valid, return the view with validation errors
            return View(service22);
        }

        public ActionResult Logout()
        {
            // Clear session variables
            Session.Clear(); // This will clear all session variables

            // Redirect to the login page
            return RedirectToAction("Home", "User"); // Redirects to the Home action method in the User controller
        }


        public ActionResult ForgotPassword()
        {
            return View();
        }

        // POST: ForgotPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(string email)
        {
            var user = c.Logins.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                ViewBag.Message = "Email not found.";
                return View();
            }

            TempData["UserId"] = user.id; // Store the user's ID in TempData
            return RedirectToAction("ResetPassword");
        }

        // GET: ResetPassword
        public ActionResult ResetPassword()
        {
            // Check if UserId exists in TempData
            if (TempData["UserId"] == null)
            {
                // Redirect to ForgotPassword if UserId is not found in TempData
                return RedirectToAction("ForgotPassword");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(string password)
        {
            // Check if UserId exists in TempData
            if (TempData["UserId"] == null)
            {
                // Redirect to ForgotPassword if UserId is not found in TempData
                return RedirectToAction("ForgotPassword");
            }

            // Retrieve the user based on the stored user ID
            int userId = (int)TempData["UserId"];
            var user = c.Logins.Find(userId);

            if (user != null)
            {
                // Update the user's password
                user.Password = password;

                // Save changes to the database
                c.SaveChanges();

                ViewBag.Message = "Password updated successfully.";
            }
            else
            {
                ViewBag.Message = "User not found."; // Or handle error appropriately
            }

            return View();
        }
    }
}