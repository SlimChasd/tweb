using System.Linq;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Admin
        public ActionResult Index()
        {
            var contactForms = _context.ContactForms.ToList();
            return View(contactForms);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
