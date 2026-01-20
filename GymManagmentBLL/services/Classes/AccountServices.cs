using GymManagementBLL.services.Interfaces;
using GymManagementBLL.ViewModels.AccountViewModel;
using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.services.Classes;

public class AccountServices : IAccountServices
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountServices(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }


    public async Task<ApplicationUser?> ValidateUser(LoginViewModel loginViewModel)
    {
        var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

        if (user is null) return null;

        var isPasswordValid = await _userManager.CheckPasswordAsync(user,loginViewModel.Password);

        return isPasswordValid ? user : null;

    }
}
