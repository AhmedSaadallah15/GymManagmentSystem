using GymManagementBLL.services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers;
[Authorize(Roles = "SuperAdmin")]
public class MemberController : Controller
{
    private readonly IMemberServices _memberServices;

    public MemberController(IMemberServices memberServices)
    {
        _memberServices = memberServices;
    }
    public ActionResult Index()
    {
        var members = _memberServices.GetAllMembers();


        return View(members);
    }


    public ActionResult MemberDetails(int id)
    {
        if (id <= 0)
        {
            TempData["ErorrMessage"] = "Id Can Not Be 0 Or Negative";
            return RedirectToAction(nameof(Index));  
        }

        var member = _memberServices.GetMemberDetails(id);
        if(member is null)
        {
            TempData["ErorrMessage"] = "Member Is Not Found";
            return RedirectToAction(nameof(Index));
        }

        return View(member);
    }

    public ActionResult HealthRecordDetails(int id)
    {
        if (id <= 0)
        {
            TempData["ErorrMessage"] = "Id Can Not Be 0 Or Negative";
            return RedirectToAction(nameof(Index));
        }
        var healthrecord = _memberServices.GetHealthrecordMemberDetails(id);

        if (healthrecord is null)
        {
            TempData["ErorrMessage"] = "Health Record Is Not Found";
            return RedirectToAction(nameof(Index));
        }

        return View(healthrecord);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult CreateMember(CreateMemberViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("InValidData", "Check The Missing Fields");
            return RedirectToAction(nameof(Create) , model);
        }

        var createdMember = _memberServices.CreateMember(model);
        if (createdMember)
        {
            TempData["SuccessfulMessage"] = "Member Created Successfully";
        }
        else
        {
            TempData["ErorrMessage"] = "Member Failed To Create";
            
        }
        return RedirectToAction(nameof(Index));
    }


    public ActionResult EditMember(int id)
    {
        if (id <= 0)
        {
            TempData["ErorrMessage"] = "Id Can Not Be 0 Or Negative";
            return RedirectToAction(nameof(Index));
        }
        var member = _memberServices.GetMemberToUpdate(id);
        if(member is null)
        {
            TempData["ErorrMessage"] = "Member Is Not Found";
            return RedirectToAction(nameof(Index));
        }

        return View(member);
    }

    [HttpPost]
    public ActionResult EditMember([FromRoute]int id , MemberUpdateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("WrongData", "Check The Missing Fields");
            return RedirectToAction(nameof(Index));

        }
        var member = _memberServices.UpdateMember(id, model);

        if (member)
        {
            TempData["SuccessfulMessage"] = "Member Updated Successfully";

        }
        else
        {
            TempData["ErorrMessage"] = "Member Failed To Update";

        }


        return RedirectToAction(nameof(Index));

    }


    public ActionResult Delete(int id)
    {
        if (id <= 0)
        {
            TempData["ErorrMessage"] = "Id Can Not Be 0 Or Negative";
            return RedirectToAction(nameof(Index));
        }
        var member = _memberServices.GetMemberDetails(id);
        if (member is null)
        {
            TempData["ErorrMessage"] = "Member Not Found";
            return RedirectToAction(nameof(Index));
        }
        ViewBag.MemberId = id;
        return View();
    }

    [HttpPost]
    public ActionResult DeleteConfirmed([FromForm]int id)
    {

        var member = _memberServices.RemoveMember(id);
        if(member)
        {
            TempData["SuccessfulMessage"] = "Member Deleted Successfully";

        }
        else
        {
            TempData["ErorrMessage"] = "Member Failed To Delete";
        }


        return RedirectToAction(nameof(Index));
    }



}
