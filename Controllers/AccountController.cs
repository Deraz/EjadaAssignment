using System.Threading.Tasks;
using EjadaAssignment.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EjadaAssignment.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        
        //Dependency Injection for the Identity Module
        public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            //Register View
            return View();
        }
        
        [HttpPost]
        public async Task< IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid) //Checking received model status
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                //Register new user in database
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //SignIn after registration
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Employees", "Home");

                }

                foreach (var error in result.Errors)
                {
                    // return description of this error
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task< IActionResult> Logout()
        {
            //Logout for current user
            await _signInManager.SignOutAsync();
            //Redirecting to home page (Login)
            return RedirectToAction("Index","Home");
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            //Login View
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid) //Checking received model status
            {
                //Login for received user
                var result = await _signInManager.PasswordSignInAsync(model.Email,model.Password,false, false);
                if (result.Succeeded)
                {
                    //Redirect to homepage after Login
                    return RedirectToAction("Index", "Home");
                }
                //return description for the error
                ModelState.AddModelError("","Invalid User");
                

            }
            return View(model);
        }
    }
}