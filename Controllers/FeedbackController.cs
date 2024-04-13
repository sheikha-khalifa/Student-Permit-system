using Microsoft.AspNetCore.Mvc;
using student_permit_system.PL.Data;
using student_permit_system.PL.Helper;
using student_permit_system.PL.Models;

namespace student_permit_system.PL.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly ApplicationDBContext _context;

        public FeedbackController(ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var feedback = _context.Feedback.ToList();
            return View(feedback);
        }
        public IActionResult AddFeedback()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddFeedback(Feedback model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Return the view with the model to display validation errors
            }

            // Save the data
            _context.Feedback.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var feedback = _context.Feedback.FirstOrDefault(f => f.FeedbackID == id);
            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        [HttpPost]
        public IActionResult Edit(Feedback model)
        {
            var existingFeedback = _context.Feedback.FirstOrDefault(f => f.FeedbackID == model.FeedbackID);

            if (existingFeedback != null)
            {
                existingFeedback.dateTime = model.dateTime;
                existingFeedback.Feadback = model.Feadback;
                existingFeedback.Rating = model.Rating;
                existingFeedback.RequestID = model.RequestID;
                existingFeedback.StudentID = model.StudentID;

                // Save changes to the database
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                // Handle the case where the feedback is not found
                return NotFound();
            }
        }

         public IActionResult Delete(int id)
        {
            var Feedback = _context.Feedback.FirstOrDefault(r => r.FeedbackID == id);
            if (Feedback == null)
            {
                return NotFound();
            }

            _context.Feedback.Remove(Feedback);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}