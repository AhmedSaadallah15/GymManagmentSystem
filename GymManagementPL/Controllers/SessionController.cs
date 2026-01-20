using GymManagementBLL.services.Interfaces;
using GymManagementBLL.ViewModels.SessionViewModel;
using GymManagmentDAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers;
[Authorize]
public class SessionController : Controller
{
    private readonly ISessionServicse _sessionServicse;

    //SessionViewModel
    public SessionController(ISessionServicse sessionServicse)
    {
        _sessionServicse = sessionServicse;
    }

    public ActionResult Index()
    {
        var sessions = _sessionServicse.GetAllSessions();

        return View(sessions);
    }


    public ActionResult Details(int id)
    {
        if (id <= 0)
        {
            TempData["ErorrMessage"] = "Id Can Not Be 0 Or Negative";
            return RedirectToAction(nameof(Index));
        }

        var session = _sessionServicse.GetSessionDetails(id);
        if (session is null)
        {
            TempData["ErorrMessage"] = "Session Is Not Found";
            return RedirectToAction(nameof(Index));
        }

        return View(session);   
    }

    public ActionResult Create()
    {
        CategorySelect();
        TrainersSelect();
        return View();
    }

    [HttpPost]
    public ActionResult Create(CreateSessionViewModel createSession)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("WrongData", "Check The Missing Fields");
            CategorySelect();
            TrainersSelect();
            return View( createSession);
        }
        var createdSession = _sessionServicse.CreateSession(createSession);
        if (createdSession)
        {
            TempData["SuccessMessage"] = "Session Created Successfully";
        }
        else
        {
            TempData["ErrorMessage"] = "Session Failed To Create";
        }

        return RedirectToAction(nameof(Index));
    }

    public ActionResult Edit(int id)
    {
        if (id <= 0)
        {
            TempData["ErorrMessage"] = "Id Can Not Be 0 Or Negative";
            return RedirectToAction(nameof(Index));
        }
        var session = _sessionServicse.GetSessionToUpdate(id);
        if (session is null)
        {
            TempData["ErorrMessage"] = "Session Is Not Found";
            return RedirectToAction(nameof(Index));
        }
        TrainersSelect();
        return View(session);
    }

    [HttpPost]

    public ActionResult Edit([FromRoute]int id , UpdateSessionViewModel updateSession)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("WrongData", "Check The Missing Fields");
            TrainersSelect();
            return RedirectToAction(nameof(Edit));

        }
        var sessionUpdated = _sessionServicse.UpdateSession(id, updateSession);
        if (sessionUpdated)
        {
            TempData["SuccessMessage"] = "Session Updated Successfully";
        }
        else
        {
            TempData["ErorrMessage"] = "Session Failed To Update";
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
        var session = _sessionServicse.GetSessionDetails(id);
        if (session is null )
        {
            TempData["ErorrMessage"] = "Session Is Not Found";
            return RedirectToAction(nameof(Index));
        }
        @ViewBag.SessionId = id;
        return View();
    }

    [HttpPost]
    public ActionResult DeleteConfirmed([FromForm]int id)
    {
        var deleteSession = _sessionServicse.DeleteSession(id);
        if (deleteSession)
        {
            TempData["SuccessMessage"] = "Member Deleted Successfully";
        }
        else
        {
            TempData["ErorrMessage"] = "Member Failed To Delete";
        }

        return RedirectToAction(nameof(Index));
    }


    private void CategorySelect()
    {
        var categorys = _sessionServicse.GetCategorysForSelect();
        ViewBag.Categorys = new SelectList(categorys, "Id", "Name");
       
    }

    private void TrainersSelect()
    {
        var Trainers = _sessionServicse.GetTarinersForSelect();
        ViewBag.Trainers = new SelectList(Trainers, "Id", "Name");
       
    }

}
