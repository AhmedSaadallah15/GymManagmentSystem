using AutoMapper;
using GymManagementBLL.services.Interfaces;
using GymManagementBLL.ViewModels.SessionViewModel;
using GymManagementDAL.UnitOfWork;
using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.services.Classes;

public class SessionServicse : ISessionServicse
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SessionServicse(IUnitOfWork unitOfWork , IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public bool CreateSession(CreateSessionViewModel createSession)
    {


        if (!IsTrainerExist(createSession.TrainerId)) return false;
        if (!IsCategoryExist(createSession.CategoryId)) return false;
        if (!IsdateValid(createSession.StartDate , createSession.EndDate)) return false;
        if(createSession.Capacity > 25 || createSession.Capacity < 0) return false;

        try
        {
         

            var mappedSession = _mapper.Map<Session>(createSession);
            mappedSession.CreatedAt = DateTime.Now;

            _unitOfWork.GetReporitory<Session>().Add(mappedSession);
            return _unitOfWork.SaveChanges() > 0;
        }
        catch
        {

            return false;
        }
     
    }

    public IEnumerable<SessionViewModel> GetAllSessions()
    {
        var sessions = _unitOfWork.SessionRepository.GetAllSessionsAndTrainerAndcategory();
        if (sessions is null || !sessions.Any()) return [];

        #region manualMap
        //return sessions.Select(x => new SessionViewModel
        // {
        //     Id = x.Id,
        //     Description = x.Description,
        //     StartDate = x.StartDate,
        //     EndDate = x.EndtDate,
        //     Capacity = x.Capacity,
        //     TrainerName = x.SessionTrainer.Name,
        //     CategoryName = x.Category.CategoryName,
        //     AvailableSlots = x.Capacity - _unitOfWork.SessionRepository.GetCountOfBookingSlots(x.Id)

        // });
        #endregion

        var mappedSessions = _mapper.Map<IEnumerable<Session> , IEnumerable<SessionViewModel>> (sessions);
        foreach (var session in mappedSessions)
        {
            session.AvailableSlots = session.Capacity - _unitOfWork.SessionRepository.GetCountOfBookingSlots(session.Id);
        }

        return mappedSessions;
    }

    public SessionViewModel? GetSessionDetails(int id)
    {
        var session = _unitOfWork.SessionRepository.GetSessionWithTrainerAndCategory(id);

        if (session is null) return null;

        #region Mapp

        //return new SessionViewModel()
        //{
        //    EndDate = session.EndtDate,
        //    StartDate = session.StartDate,
        //    Description = session.Description,
        //    Capacity = session.Capacity,
        //    CategoryName = session.Category.CategoryName,
        //    TrainerName = session.SessionTrainer.Name,
        //    AvailableSlots = session.Capacity - _unitOfWork.SessionRepository.GetCountOfBookingSlots(session.Id)

        //};
        #endregion

        var mappedSessions = _mapper.Map<Session, SessionViewModel>(session);
        mappedSessions.AvailableSlots = session.Capacity - _unitOfWork.SessionRepository.GetCountOfBookingSlots(mappedSessions.Id);

        return mappedSessions;


    }




    public UpdateSessionViewModel? GetSessionToUpdate(int id)
    {
        var session = _unitOfWork.SessionRepository.GetById(id);
    
        if(!IsSessionAvalibaleForUpdate(session)) return null;

        var mappedSession = _mapper.Map<UpdateSessionViewModel>(session);

        return mappedSession;
    }

    public bool UpdateSession(int id, UpdateSessionViewModel updatedSession)
    {
        var session = _unitOfWork.SessionRepository.GetById(id);

        try
        {

            if (!IsSessionAvalibaleForUpdate(session)) return false;
            if (!IsTrainerExist(updatedSession.TrainerId)) return false;
            if (!IsdateValid(updatedSession.StartDate, updatedSession.EndDate)) return false;

            _mapper.Map(updatedSession, session);
            session.UpdatedAt = DateTime.Now;

            _unitOfWork.SessionRepository.Update(session);

            return _unitOfWork.SaveChanges() > 0;
        }
        catch (Exception)
        {

            return false;
        }

    }

    public bool DeleteSession(int id)
    {
       

        try
        {
            var session = _unitOfWork.SessionRepository.GetById(id);

            if (!IsSessionAvalibaleForDelete(session)) return false;

            _unitOfWork.SessionRepository.Delete(session);

            return _unitOfWork.SaveChanges() > 0;
        }
        catch (Exception)
        {

            return false;
        }


    }

    private bool IsTrainerExist (int id)
    {
        return _unitOfWork.GetReporitory<Trainer>().GetById(id) is not null;
    }

    private bool IsCategoryExist(int id)
    {
        return _unitOfWork.GetReporitory<Category>().GetById(id) is not null;
    }

    private bool IsdateValid(DateTime startDate , DateTime endDate)
    {
        return endDate > startDate && DateTime.Now < startDate;
    }

    private bool IsSessionAvalibaleForUpdate(Session session)
    {
        if(session is null) return false;
        //if completed
        if(session.EndDate < DateTime.Now) return false;
        // if started
        if (session.StartDate <= DateTime.Now ) return false;

        // has active 
        var HasActiveBooking = _unitOfWork.SessionRepository.GetCountOfBookingSlots(session.Id) > 0;
        if (HasActiveBooking) return false;

        return true;
    }

    private bool IsSessionAvalibaleForDelete(Session session)
    {
        // is not null
        if (session is null) return false;
        // upcoming
        if (session.StartDate > DateTime.Now ) return false;
        //started - ongoing
        if (session.StartDate <= DateTime.Now && session.EndDate > DateTime.Now) return false;


        var HasActiveBooking = _unitOfWork.SessionRepository.GetCountOfBookingSlots(session.Id) > 0;
        if (HasActiveBooking) return false;

        return true;
    }

    public IEnumerable<TrainerSelectViewModel> GetTarinersForSelect()
    {
        var trainers = _unitOfWork.GetReporitory<Trainer>().GetAll();
                           

        return _mapper.Map< IEnumerable<TrainerSelectViewModel>>(trainers);
    }

    public IEnumerable<CategorySelectViewModel> GetCategorysForSelect()
    {
        var Categoryss = _unitOfWork.GetReporitory<Category>().GetAll();

        return _mapper.Map<IEnumerable<CategorySelectViewModel>>(Categoryss);
    }
}



