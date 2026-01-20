using GymManagementBLL.services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace GymManagementPL.Controllers;
[Authorize]
public class HomeController : Controller
{
    private readonly IAnalyticsServices _analyticsServices;

    public HomeController(IAnalyticsServices analyticsServices)
    {
        _analyticsServices = analyticsServices;
    }

    public ActionResult Index()
    {
       var analytics = _analyticsServices.GetAnalyticsData();
        return View(analytics);
    }

}
