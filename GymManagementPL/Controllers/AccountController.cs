using GymManagementBLL.services.Interfaces;
using GymManagementBLL.ViewModels.AccountViewModel;
using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GymManagementPL.Controllers;

public class AccountController : Controller
{
    private readonly IAccountServices _accountServices;
    private readonly SignInManager<ApplicationUser> _signInManager;


    public AccountController(IAccountServices accountServices , SignInManager<ApplicationUser> signInManager)
    {
        _accountServices = accountServices;
        _signInManager = signInManager;
    }

    public ActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<ActionResult> Login(LoginViewModel model)
    {

        if (!ModelState.IsValid) return View(model);

        var user = await _accountServices.ValidateUser(model);
        if(user is null)
        {
            ModelState.AddModelError("InvalidLogin" , "Invalied Email Or Password");
            return View(model);
        }

        var result =
       await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

        if (result.IsNotAllowed)
        {
            ModelState.AddModelError("InvalidLogin", "Your Account Is Not Allowed");
          
        }
        if (result.IsLockedOut)
        {
            ModelState.AddModelError("InvalidLogin", "Your Account Is Locked Out");
        }

        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }

        return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> Logout()
    {
         await  _signInManager.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }

    public ActionResult AccessDenied()
    {
        return View();
    }

}
//login view
//login action 
//logout 
//access denied