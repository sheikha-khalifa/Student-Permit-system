using Microsoft.AspNetCore.Mvc;
using student_permit_system.PL.Models;
using student_permit_system.PL.Data;
using student_permit_system.PL.Helper;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace student_permit_system.PL.Controllers
{
    public class RequestsController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager; // Inject UserManager<ApplicationUser>
		private readonly SignInManager<ApplicationUser> _signInManager;

		public RequestsController(ApplicationDBContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager; // Initialize _userManager
            _signInManager = signInManager;

		}


        public IActionResult Index()
        {
            var requests = _context.Requests.ToList();
            return View(requests);
        }
		public IActionResult StudentsView()
		{
			if (User.IsInRole("Student"))
			{
				// Retrieve requests for the current student user
				var studentId = _userManager.GetUserId(User); // Get the current user's ID
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



		public IActionResult AddRequest()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddRequest(Requests model)
        {
            var studentId = _userManager.GetUserId(User); // Get the current user's ID

            if (ModelState.IsValid)
            {
                return View(model); // Return the view with the model to display validation errors
            }

            // Handle file upload and save image
            model.ImageUrl = DocumentConf.DocumentUpload(model.Image, "images");
            model.Id = studentId;


            // Save the data
            _context.Requests.Add(model);
            _context.SaveChanges();

            return RedirectToAction("StudentsView");
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
            
                var existingRequest = _context.Requests.FirstOrDefault(r => r.RequestID == model.RequestID);

                if (existingRequest != null)
                {
                    existingRequest.DateTime = model.DateTime;
                    existingRequest.Status = model.Status;
                    existingRequest.CarNumber = model.CarNumber;

                    // Check if a new image is uploaded
                    if (model.Image != null)
                    {
                        // Update the ImageUrl using the uploaded file
                        existingRequest.ImageUrl = DocumentConf.DocumentUpload(model.Image, "images");
                    }

                    // Save changes to the database
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle the case where the request is not found
                    return NotFound();
                }
            

            // If ModelState is not valid, return to the Edit view with the current model
            return View(model);
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
