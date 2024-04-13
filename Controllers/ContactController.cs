using Microsoft.AspNetCore.Mvc;
using student_permit_system.PL.Data;
using student_permit_system.PL.Helper;
using student_permit_system.PL.Models;

namespace student_permit_system.PL.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ContactController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Contact
        public IActionResult Index()
        {
            var contacts = _context.Contact.ToList();
            return View(contacts);
        }

        // GET: Contact/AddComment
        public IActionResult AddComment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddComment(Contact model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Return the view with the model to display validation errors
            }

            // Save the data
            _context.Contact.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        
    }



        // GET: Contact/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = _context.Contact.FirstOrDefault(m => m.ContactID == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var contact = _context.Contact.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contact.Remove(contact);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
