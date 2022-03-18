using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shopping.Interface;
using Shopping.Models;

namespace Shopping.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IUserHelper _userHelper;

        public AccountsController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
        }

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return  RedirectToAction(nameof(Index), "Home");
            }

            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.LoginAsync(login);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index), "Home");
                }
            }

            ModelState.AddModelError(String.Empty, "Email or password invalid.");

            return View(login);
        }
        public async Task<ActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction(nameof(Index), "Home");
        }

        public IActionResult NotAuthorized()
        {
            return View();
        }
    }
}
