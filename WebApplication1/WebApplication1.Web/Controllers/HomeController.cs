using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Domain;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        
        // Действие для главной страницы
        public ActionResult Index()
        {
            return View();
        }

        // Действие для страницы "О нас"
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        // Действие для страницы контактов (форма обратной связи)
        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View(new ContactViewModel());
        }

        // Обработка данных формы обратной связи
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitContactForm(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var contactForm = new ContactForm
                {
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    Message = model.Message,
                    SubmittedAt = DateTime.Now
                };

                _context.ContactForms.Add(contactForm);
                _context.SaveChanges();

                return RedirectToAction("ThankYou");
            }

            ViewBag.Message = "Your contact page.";
            return View("Contact", model);
        }

        // Действие для страницы благодарности после отправки формы
        public ActionResult ThankYou()
        {
            return View();
        }

        // Действие для страницы с часами (пример)
        public ActionResult Watchs()
        {
            ViewBag.Message = "Your watches page.";
            return View();
        }
    }
}