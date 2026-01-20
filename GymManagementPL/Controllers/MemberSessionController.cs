using GymManagementBLL.services.Interfaces;
using GymManagementBLL.ViewModels.MemberSessionViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers;
[Authorize]
public class MemberSessionController : Controller
{
    private readonly IMemberSessionServices _memberSession;

    public MemberSessionController(IMemberSessionServices memberSession)
    {
        _memberSession = memberSession;
    }

    public ActionResult Index()
    {
        var sessions = _memberSession.GetSessionWithCategoryAndTrainer();

        return View(sessions);
    }

    public ActionResult Create(int sessionId)
    {
        if (sessionId <= 0)
        {
            TempData["ErorrMessage"] = "This Session Is Not Found";
            return RedirectToAction(nameof(GetMembersForUpcomingSession));
        }

        MemberSelect();
        return View(new CreateMemberSessionViewModel
        {
            SessionId = sessionId
        });
    }

    [HttpPost]
    public ActionResult Create(CreateMemberSessionViewModel model)
    {
        if (!ModelState.IsValid)
        {
                TempData["ErrorMessage"] = "This Member Has Already Booked";
                MemberSelect();
                return View(model);
        }
        var createMemberSession = _memberSession.CreateMemberSession(model);
        if (createMemberSession)
        {
            TempData["SuccessMessage"] = "Member Session Added Successfully";
        }
        else
        {
            TempData["ErrorMessage"] = "Member Session Can not Added";
        }
        return RedirectToAction(nameof(GetMembersForUpcomingSession) , new { id = model.SessionId });
    }



    public ActionResult GetMembersForUpcomingSession(int id)
    {
        var members = _memberSession.GetMembersForUpcomingSession(id);
        @ViewBag.SessionId = id;
        return View(members);
    }

    public ActionResult GetMembersForOngoingSessions(int id)
    {
        var members = _memberSession.GetMembersForOngoingSessions(id);

        return View(members);
    }


    [HttpPost]
    public ActionResult Cancel(int memberId, int sessionId)
    {
        var result = _memberSession.CancelBooking(memberId, sessionId);

        if (result)
        {
            TempData["SuccessMessage"] = "Booking cancelled successfully.";
        }
        else
        {
            TempData["ErrorMessage"] = "Cancellation is not allowed for this session.";
        }

        return RedirectToAction(nameof(GetMembersForUpcomingSession),
            new { id = sessionId });
    }
    [HttpPost]
    public ActionResult Attended(int memberId , int sessionId)
    {
        if(memberId < 0 || sessionId < 0)
        {
            TempData["ErorrMessage"] = "This Member Is Not Found";
            return RedirectToAction(nameof(GetMembersForOngoingSessions));
        }

       var memberAttend = _memberSession.MarkAttendance(memberId, sessionId);

        if (memberAttend)
        {
            TempData["SuccessMessage"] = "Plan Updated Successfully";
        }
        else
        {
            TempData["ErrorMessage"] = "Plan Failed To Update";
        }

        return RedirectToAction(nameof(GetMembersForOngoingSessions), new { id = sessionId });
    }


    private void MemberSelect()
    {
        var members= _memberSession.GetMembersForSelect();

        ViewBag.Members = new SelectList(members, "Id", "Name");
    }
}



//Create => View
//Create => Action
//GetMembersForUpcomingSession
//GetMembersForOngoingSessions
//Cancel