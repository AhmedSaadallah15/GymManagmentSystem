using GymManagementBLL.services.Interfaces;
using GymManagementBLL.ViewModels.PlanViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace GymManagementPL.Controllers;
[Authorize]
public class PlanController : Controller
{
    private readonly IPlanServicse _planServicse;

    public PlanController(IPlanServicse planServicse)
    {
        _planServicse = planServicse;
    }


    public ActionResult Index()
    {
        var Plans = _planServicse.GetAllPlans();

        return View(Plans);
    }

    public ActionResult Details(int id)
    {
        if (id <= 0)
        {
            TempData["ErorrMessage"] = "Id Can Not Be 0 Or Negative";
            return RedirectToAction(nameof(Index));
        }
        var plan = _planServicse.GetPlanDetails(id);
        if(plan == null)
        {
            TempData["ErorrMessage"] = "Plan Is Not Found";
            return RedirectToAction(nameof(Index));
        }
        return View(plan);
    }

    public ActionResult Edit(int id)
    {
        if (id <= 0)
        {
            TempData["ErorrMessage"] = "Id Can Not Be 0 Or Negative";
            return RedirectToAction(nameof(Index));
        }

        var plan = _planServicse.GetPlanToUpdate(id);
        if (plan is null)
        {
            TempData["ErorrMessage"] = "Plan Is Not Found";
            return RedirectToAction(nameof(Index));
        }

        return View(plan);
    }

    [HttpPost]
    public ActionResult Edit([FromRoute]int id , UpdatePlanViewModel updatePlan)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("WrongData", "Check The Missing Fields");

            return View(updatePlan);
        }

        var planUpdated = _planServicse.UpdatePlan(id , updatePlan);
        if (planUpdated)
        {
            TempData["SuccessMessage"] = "Plan Updated Successfully";
        }
        else
        {
            TempData["ErrorMessage"] = "Plan Failed To Update";
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public ActionResult Activate(int id)
    {
        var planStatusChanged = _planServicse.ToggleStatusPlan(id);
        if (planStatusChanged)
        {
            TempData["SuccessMessage"] = "Plan Status Changed Successfully";
        }
        else
        {
            TempData["ErorrMessage"] = "Can Not Change Status";
        }
        return RedirectToAction(nameof(Index));
    }
}
