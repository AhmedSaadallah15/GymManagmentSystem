using GymManagementBLL.services.Interfaces;
using GymManagementBLL.ViewModels.TrainerViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers;
[Authorize(Roles = "SuperAdmin")]
public class TrainerController : Controller
{
    private readonly ITrainerServices _trainerServices;

    public TrainerController(ITrainerServices trainerServices)
    {
        _trainerServices = trainerServices;
    }


    public ActionResult Index()
    {
        var Trainers = _trainerServices.GetAllTrainers();

        return View(Trainers);
    }

    public ActionResult Details(int id)
    {
        if (id <= 0)
        {

            TempData["ErorrMessage"] = "Id Can Not Be 0 Or Negative";
            return RedirectToAction(nameof(Index));
        }

        var trainer = _trainerServices.GetTrainerDetails(id);

        if (trainer is null)
        {
            TempData["ErrorMessage"] = "Trainer Is Not Found";
            return RedirectToAction(nameof(Index));

        }

        return View(trainer);
    }


    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult CreateTrainer(CreateTrainerViewModel createTrainer)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("WrongData", "Check The Missing Fields");

            return View(nameof(Create) ,createTrainer);
        }

        var create = _trainerServices.CreateTrainer(createTrainer);
        if (create)
        {
            TempData["SuccessfulMessage"] = "Trainer Created Successfully";
        }
        else
        {
            TempData["ErorrMessage"] = "Trainer Failed To Create";
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
        var tarienr = _trainerServices.GetTrainerToUpdate(id);
        if(tarienr is null)
        {
            TempData["ErrorMessage"] = "Trainer Is Not Found";
            return RedirectToAction(nameof(Index));
        }
        return View(tarienr);
    }

    [HttpPost]
    public ActionResult Edit([FromRoute]int id , TrainerUpdateViewModel trainerUpdate)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("WrongData", "Check The Missing Fields");
            return RedirectToAction(nameof(Index));
        }

        var tariner = _trainerServices.UpdateTrainer(id , trainerUpdate);
        if (tariner)
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
        var tarienr = _trainerServices.GetTrainerDetails(id);
        if(tarienr is null)
        {
            TempData["ErorrMessage"] = "Member Not Found";
            return RedirectToAction(nameof(Index));
        }
        ViewBag.TrainerId = id;
        return View();
    }

    [HttpPost]
    public ActionResult DeleteConfirmed([FromForm]int id)
    {
        var deletedTrainer = _trainerServices.DeleteTrainer(id);
        if (deletedTrainer)
        {
            TempData["SuccessfulMessage"] = "Trainer Deleted Successfully";
        }
        else
        {
            TempData["ErorrMessage"] = "Trainer Failed To Delete";

        }

        return RedirectToAction(nameof(Index));
    }


}
