using GymManagementBLL.ViewModels.AccountViewModel;
using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.services.Interfaces;

public interface IAccountServices
{

     Task<ApplicationUser?> ValidateUser(LoginViewModel loginViewModel);



}
