using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IPORevamp.Core.ViewModels;
using IPORevamp.Data.UserManagement.Model;

namespace IPORevamp.WebAPI.Controllers
{
    public class AccountController : Controller
    {

        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;



        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }


        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginUser, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginUser.Username, loginUser.Password, loginUser.RememberMe, false);

                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Login successfully";
                    return Redirect(returnUrl);
                }
                else
                {                    
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    return View(loginUser);                    
                }
            }            
            return View(loginUser);                        
        } 
    }
}