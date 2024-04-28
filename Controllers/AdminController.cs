using Microsoft.AspNetCore.Mvc;
using student_permit_system.PL.Data;
using student_permit_system.PL.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // Add this namespace for Include method

namespace student_permit_system.PL.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDBContext _context;

        public AdminController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDBContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Admin/AddEmployee
        public IActionResult AddEmployee()
        {
            return View();
        }

        // POST: Admin/AddEmployee
        [HttpPost]
        public async Task<IActionResult> AddEmployee(ApplicationUser model)
        {
            if (ModelState.IsValid)
            {
                var user = new Employee
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                    PhoneNo = model.PhoneNo,
                    Password = model.Password,
                    Address = model.Address
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Assign role based on your logic
                    await _userManager.AddToRoleAsync(user, "Employee");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
        }


        // GET: Admin/AddEmployee
        public IActionResult AddAdmin()
        {
            return View();
        }

        // POST: Admin/AddEmployee
        [HttpPost]
        public async Task<IActionResult> AddAdmin(ApplicationUser model)
        {
            if (ModelState.IsValid)
            {
                var user = new Admin
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                    PhoneNo = model.PhoneNo,
                    Password = model.Password,
                    Address = model.Address
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Assign role based on your logic
                    await _userManager.AddToRoleAsync(user, "Admin");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
        }


        // GET: Admin/AddStudent
        public IActionResult AddStudent()
        {
            return View();
        }

        // POST: Admin/AddEmployee
        [HttpPost]
        public async Task<IActionResult> AddStudent(ApplicationUser model)
        {
            if (ModelState.IsValid)
            {
                var user = new Students
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                    PhoneNo = model.PhoneNo,
                    Password = model.Password,
                    Address = model.Address
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Assign role based on your logic
                    await _userManager.AddToRoleAsync(user, "Student");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
        }

        // GET: Admin/EditEmployee/{id}
        public async Task<IActionResult> EditEmployee(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Admin/EditEmployee/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmployee(string id, ApplicationUser model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    if (user == null)
                    {
                        return NotFound();
                    }

                    user.Name = model.Name;
                    user.Email = model.Email;
                    user.PhoneNo = model.PhoneNo;
                    user.Address = model.Address;

                    // Update the user
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(ViewUsers));
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View(model);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(model);
        }
        // POST: Admin/DeleteEmployee/{id}


        public IActionResult DeleteEmployee(String id)
        {
            var DeleteEmployee = _context.ApplicationUsers.FirstOrDefault(r => r.Id == id);
            if (DeleteEmployee == null)
            {
                return NotFound();
            }

            _context.ApplicationUsers.Remove(DeleteEmployee);
            _context.SaveChanges();

            return RedirectToAction("ViewUsers");
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteEmployee(string id)
        //{
        //    var user = await _userManager.FindByIdAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    var result = await _userManager.DeleteAsync(user);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction(nameof(ViewUsers));
        //    }
        //    else
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError("", error.Description);
        //        }
        //        return View("ViewUsers", await _context.Users.ToListAsync());
        //    }
        //}


        // Method to check if a user exists
        private bool UserExists(string id)
        {
            return _context.Users.Any(u => u.Id == id);
        }

        // GET: Admin/ViewUsers
        public IActionResult ViewUsers()
        {
            var users = _context.Users.ToList(); // Fetch all users
            return View(users);
        }
    }
}
