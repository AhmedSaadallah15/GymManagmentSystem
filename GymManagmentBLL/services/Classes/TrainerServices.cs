using AutoMapper;
using GymManagementBLL.services.Interfaces;
using GymManagementBLL.ViewModels.TrainerViewModels;
using GymManagementDAL.UnitOfWork;
using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.services.Classes;

public class TrainerServices : ITrainerServices
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TrainerServices(IUnitOfWork unitOfWork , IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public bool CreateTrainer(CreateTrainerViewModel CreateTrainerViewModel)
    {
        if (CheckEmail(CreateTrainerViewModel.Email) || CheckPhone(CreateTrainerViewModel.Phone))
            return false;
        try
        {
            #region manualMap
            //var newTrainer = new Trainer()
            //{
            //    Name = CreateTrainerViewModel.Name,
            //    Phone = CreateTrainerViewModel.Phone,
            //    Email = CreateTrainerViewModel.Email,
            //    DateOfBirth = CreateTrainerViewModel.DateOfBirth,
            //    Gender = CreateTrainerViewModel.Gender,
            //    Specialties = CreateTrainerViewModel.Specialties,
            //    Address = new Address()
            //    {
            //        City = CreateTrainerViewModel.City,
            //        Street = CreateTrainerViewModel.Street,
            //        BuildingNumber = CreateTrainerViewModel.BuildingNumber,
            //    }
            //};
            #endregion
            var newTrainer = _mapper.Map<Trainer>(CreateTrainerViewModel);


            _unitOfWork.GetReporitory<Trainer>().Add(newTrainer);
            return _unitOfWork.SaveChanges() > 0;
        }
        catch (Exception)
        {

            return false; 
        }
    }

    public bool DeleteTrainer(int id)
    {
        try
        {
            var trainer = _unitOfWork.GetReporitory<Trainer>().GetById(id);

            if (trainer is null) return false;

            var haveFutureSession = _unitOfWork.GetReporitory<Session>()
                                    .GetAll(x => x.TrainerId == id && x.StartDate > DateTime.Now).Any();

            if (haveFutureSession) return false;



            _unitOfWork.GetReporitory<Trainer>().Delete(trainer);
            return _unitOfWork.SaveChanges() > 0;
        }
        catch (Exception)
        {

            return false;
        }

    }

    public IEnumerable<TrainerViewModel> GetAllTrainers()
    {
        var trainers = _unitOfWork.GetReporitory<Trainer>().GetAll();

        if (trainers is null || !trainers.Any()) return [];
        #region manualMap

        //var trainerViewModel = trainers.Select(t => new TrainerViewModel
        //{
        //    Id = t.Id,
        //    Name = t.Name,
        //    Email = t.Email,
        //    Phone = t.Phone,
        //    Specialties = t.Specialties.ToString()
        //});



        //return trainerViewModel;
        #endregion
        return _mapper.Map<IEnumerable<TrainerViewModel>>(trainers);
    }

    public TrainerDetailsViewModel? GetTrainerDetails(int id)
    {
        var trainer = _unitOfWork.GetReporitory<Trainer>().GetById(id);

        if(trainer is null) return null;
        #region manualMap
        //var trainerView = new TrainerDetailsViewModel()
        //{
        //    Name = trainer.Name ,
        //    Email = trainer.Email ,
        //    DateOfBirth = trainer.DateOfBirth.ToShortDateString(),
        //    Phone = trainer.Phone ,
        //    Specialties = trainer.Specialties.ToString(),
        //    Address = $"{trainer.Address.BuildingNumber} - {trainer.Address.Street} - {trainer.Address.City}"

        //};

        //return trainerView;

        #endregion
        return _mapper.Map<TrainerDetailsViewModel>(trainer);
    }

    public TrainerUpdateViewModel? GetTrainerToUpdate(int id )
    {
        var trainer = _unitOfWork.GetReporitory<Trainer>().GetById(id);
        if (trainer is null) return null;

        #region manualMap
        //return new TrainerUpdateViewModel()
        //{
        //    Name = trainer.Name,
        //    Email = trainer.Email,
        //    Phone = trainer.Phone,
        //    City = trainer.Address.City,
        //    Specialties = trainer.Specialties,
        //    BuildingNumber = trainer.Address.BuildingNumber,
        //    Street = trainer.Address.Street,


        //};
        #endregion


        return _mapper.Map<TrainerUpdateViewModel>(trainer);

    }

    public bool UpdateTrainer(int id, TrainerUpdateViewModel trainerUpdate)
    {

        try
        {
            var EmailExist = _unitOfWork.GetReporitory<Trainer>().GetAll(x => x.Email == trainerUpdate.Email && x.Id != id).Any();
            var PhoneExist = _unitOfWork.GetReporitory<Trainer>().GetAll(x => x.Phone == trainerUpdate.Phone && x.Id != id).Any();

            if (EmailExist || PhoneExist) return false;


            var trainer = _unitOfWork.GetReporitory<Trainer>().GetById(id);

            if (trainer is null) return false;
            #region manaulMap

            //trainer.Email = trainerUpdate.Email;
            //trainer.Phone = trainerUpdate.Phone;
            //trainer.Specialties = trainerUpdate.Specialties;
            //trainer.Address.City = trainerUpdate.City;
            //trainer.Address.Street = trainerUpdate.Street;
            //trainer.Address.BuildingNumber = trainerUpdate.BuildingNumber;
            //trainer.UpdatedAt = DateTime.Now;
            #endregion

            _mapper.Map(trainerUpdate, trainer);

            _unitOfWork.GetReporitory<Trainer>().Update(trainer);
            return _unitOfWork.SaveChanges() > 0;
        }
        catch (Exception)
        {

            return false;
        }




    }






    private bool CheckEmail(string email) // trainerId
    {
        return _unitOfWork.GetReporitory<Trainer>().GetAll(x => x.Email == email).Any();

        //x.id != trainerId 

    }
    private bool CheckPhone(string phone)
    {
        return _unitOfWork.GetReporitory<Trainer>().GetAll(x => x.Phone == phone).Any();

    }

}
