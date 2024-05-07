using System;
using System.Web.Mvc;
using WebApplication1.Domain;

namespace WebApplication1.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Contact
        public ActionResult Index()
        {
            return View(new ContactForm());
        }

        // POST: Contact/SubmitContactForm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitContactForm(ContactForm model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                ModelState.AddModelError("", "You must be logged in to submit the form.");
                return View("Index", model);
            }

            if (ModelState.IsValid)
            {
                model.SubmittedAt = DateTime.Now;
                _context.ContactForms.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View("Index", model);
        }
    }
}