using Microsoft.AspNetCore.Mvc;
using student_permit_system.PL.Models;
using student_permit_system.PL.Data;
using student_permit_system.PL.Helper;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;


namespace student_permit_system.PL.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager; // Inject UserManager<ApplicationUser>

        public StudentsController(ApplicationDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager; // Initialize _userManager
        }



        public IActionResult StudentsView()
        {
            if (User.IsInRole("Student"))
            {
                // Retrieve requests for the current student user
                var studentId = _userManager.GetUserId(User);
                var studentRequests = _context.Requests.Where(r => r.Id == studentId).ToList();
                return View(studentRequests);
            }
            else if (User.IsInRole("Admin") || User.IsInRole("Employee"))
            {
                // Retrieve all requests for admins and employees
                var allRequests = _context.Requests.ToList();
                return View(allRequests);
            }
            else
            {
                // Unauthorized access for users without roles
                return Unauthorized();
            }
        }
        public IActionResult Index()
        {
            // Get the currently logged-in student's ID
            string studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Fetch requests associated with the current student
            var requests = _context.Requests.Where(r => r.Id == studentId).ToList();

            return View(requests);
        }
        public IActionResult AddRequest()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRequest(Requests model)
        {
            model.ImageUrl = DocumentConf.DocumentUpload(model.Image, "images");
            if (ModelState.IsValid)
            {
                _context.Requests.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }
        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Edit(int id)
        {
            var request = _context.Requests.FirstOrDefault(r => r.RequestID == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        [HttpPost]
        public IActionResult Edit(Requests model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existingRequest = _context.Requests.FirstOrDefault(r => r.RequestID == model.RequestID);
            if (existingRequest == null)
            {
                return NotFound();
            }

            // Update request properties
            existingRequest.DateTime = model.DateTime;
            existingRequest.Status = model.Status;
            existingRequest.CarNumber = model.CarNumber;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var request = _context.Requests.FirstOrDefault(r => r.RequestID == id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}