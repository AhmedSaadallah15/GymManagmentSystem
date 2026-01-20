using GymManagementBLL.services.Classes;
using GymManagementBLL.services.Interfaces;
using GymManagementBLL.ViewModels.MembershipViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers;
[Authorize]
public class MembershipController : Controller
{
    private readonly IMembershipServices _membershipServices;

    public MembershipController(IMembershipServices membershipServices)
    {
        _membershipServices = membershipServices;
    }

    public ActionResult Index()
    {
        var memberships = _membershipServices.GetAllMemberships();
        return View(memberships);
    }


    public ActionResult Create()
    {
        PlansSelect();
        MembersSelect();
        return View();
    }

    [HttpPost]
    public ActionResult Create(CreateMembershipViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "A member cannot have more than one Active membership at the same time.";
            PlansSelect();
            MembersSelect();
            return View(model);
        }

        var createMembership = _membershipServices.CreateMembership(model);

        if (createMembership)
        {
            TempData["SuccessMessage"] = "Membership Added Successfully";
        }
        else
        {
            TempData["ErrorMessage"] = "Membership Can not Added";
        }

        return RedirectToAction(nameof(Index));
    }

    public ActionResult Cancel([FromForm]int memberId)
    {
        if (memberId <= 0)
        {
            TempData["ErrorMessage"] = "Check your data";
            return RedirectToAction(nameof(Index));
        }
        var membership = _membershipServices.CancelMembership(memberId);

        if (membership)
        {
            TempData["SuccessMessage"] = "Membership has Deleted Successfully";
        }
        else
        {
            TempData["ErrorMessage"] = "Membership Can not Delete";
        }
        return RedirectToAction(nameof(Index));
    }

    private void PlansSelect()
    {
        var Plans = _membershipServices.GetPlansForSelect();
        ViewBag.Plans = new SelectList(Plans, "Id", "Name");

    }

    private void MembersSelect()
    {
        var members = _membershipServices.GetMembersForSelect();
        ViewBag.Members = new SelectList(members, "Id", "Name");

    }


}
