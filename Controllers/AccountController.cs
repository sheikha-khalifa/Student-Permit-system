using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using student_permit_system.PL.Models;
using student_permit_system.PL.Helper;

namespace student_permit_system.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(ApplicationUser model)
        {
            if (ModelState.IsValid)
            {
                var user = new Students
                {
                    UserName = model.Name,
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
                    return RedirectToAction("AddRequest", "Requests");
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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(email, password, true, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    //var user = await _userManager.FindByEmailAsync(email);
                    var user = await _userManager.FindByNameAsync(email);

                    if (user != null)
                    {
                        var roles = await _userManager.GetRolesAsync(user);

                        if (roles.Contains("Admin"))
                        {
                            return RedirectToAction("ViewUsers", "Admin");
                        }
                        else if (roles.Contains("Student"))
                        {
                            return RedirectToAction("StudentsView", "Requests");
                        }
                        else if (roles.Contains("Employee"))
                        {
                            return RedirectToAction("Index", "Requests");
                        }
                    }
                    else
                    {
                        // Handle case where user is not found
                        ModelState.AddModelError(string.Empty, "User not found.");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }

            }

            // If login fails or ModelState is not valid, return to the login view
            return View();
        }


        [HttpPost]
public async Task<IActionResult> Logout()
{
    await _signInManager.SignOutAsync();
    return RedirectToAction("Index", "Home");
}


        //ForgetPassword

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(string Email)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Email);
                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resetLink = Url.Action("ResetPassword", "Account", new { email = user.Email, token = token }, Request.Scheme);
                    var em = new Email()
                    {
                        To = user.Email,
                        title = "Reset Password",
                        Body = resetLink

                    };
                    //Email Configuration Pasword Function will be Called here
                    EmailConf.ResetPasswordEmail(em);
                    return RedirectToAction("EmailSent");
                }
                ModelState.AddModelError(String.Empty, "No User Found With this Email");

            }
            ModelState.AddModelError(string.Empty, "Email is not Valid");

            return View();
        }

        public IActionResult EmailSent()
        {
            return View();
        }
    }
}
