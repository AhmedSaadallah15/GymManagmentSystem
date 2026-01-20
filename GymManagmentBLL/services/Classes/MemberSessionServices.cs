using AutoMapper;
using GymManagementBLL.services.Interfaces;
using GymManagementBLL.ViewModels.MemberSessionViewModels;
using GymManagementBLL.ViewModels.MembershipViewModels;
using GymManagementBLL.ViewModels.SessionViewModel;
using GymManagementDAL.Entities;
using GymManagementDAL.UnitOfWork;
using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.services.Classes;

public class MemberSessionServices : IMemberSessionServices
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MemberSessionServices(IUnitOfWork unitOfWork , IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

  

    public IEnumerable<SessionViewModel> GetSessionWithCategoryAndTrainer()
    {
        var sessions = _unitOfWork.SessionRepository.GetAllSessionsAndTrainerAndcategory();

        var mappedSession = _mapper.Map<IEnumerable<SessionViewModel>>(sessions);
        foreach(var session in mappedSession)
        {
            session.AvailableSlots =session.Capacity - _unitOfWork.SessionRepository.GetCountOfBookingSlots(session.Id);
        }

        return mappedSession;
    }

    public IEnumerable<MembersForUpcomingSessionViewModel> GetMembersForUpcomingSession(int id)
    {
        var memberSession = _unitOfWork.MemberSessionRepository.GetMemberSessionsById(id);

        return _mapper.Map<IEnumerable<MembersForUpcomingSessionViewModel>>(memberSession);
    }

    public IEnumerable<MembersForOngoingSessionsViewModel> GetMembersForOngoingSessions(int id)
    {
        var memberSession = _unitOfWork.MemberSessionRepository.GetMemberSessionsById(id);
        return _mapper.Map<IEnumerable<MembersForOngoingSessionsViewModel>>(memberSession);

    }

    public bool CreateMemberSession(CreateMemberSessionViewModel model )
    {
        var session = _unitOfWork.SessionRepository.GetById(model.SessionId);
        if (session is null) return false;

        if (!IsSessionAvailableForBooking(session.StartDate , session.EndDate)) return false;
        if (!IsMemberHasActiveMembership(model.MemberId)) return false;
        if (!HasAvailableSlots(model.SessionId)) return false;
       var memberHasBooked = _unitOfWork.MemberSessionRepository.GetAll(x => x.SessionId == model.SessionId && x.MemberId == model.MemberId).Any();
        if(memberHasBooked) return false;
        try
        {
           var mappedMemberSession = _mapper.Map<MemberSession>(model);
            mappedMemberSession.IsAttended = false;
            _unitOfWork.MemberSessionRepository.Add(mappedMemberSession);
            return _unitOfWork.SaveChanges() > 0;
        }
        catch
        {

           return false;
        }
    }


    public IEnumerable<MemberSelectViewModel> GetMembersForSelect()
    {
        var members = _unitOfWork.GetReporitory<Member>().GetAll();

        return _mapper.Map<IEnumerable<MemberSelectViewModel>>(members);
    }

    public bool MarkAttendance(int memberId, int sessionId)
    {
        var session = _unitOfWork.SessionRepository.GetById(sessionId);
        if (session is null) return false;

        if (!IsSessionAvailableForBooking(session.StartDate, session.EndDate)) return false;

        var memberSession = _unitOfWork.MemberSessionRepository.GetAll(x => x.SessionId == sessionId && x.MemberId == memberId);
        if (!memberSession.Any()) return false;



        foreach (var item in memberSession)
        {
            _unitOfWork.MemberSessionRepository.Update(item);
            item.IsAttended = true;
        }

        return _unitOfWork.SaveChanges() > 0;
    }

    private bool IsSessionAvailableForBooking(DateTime stratDate , DateTime endDate)
    {
        //ongoing session
        return stratDate < DateTime.Now && endDate > DateTime.Now;
    }

    private bool IsMemberHasActiveMembership(int memberId)
    {
        //have active membership
        return _unitOfWork.MembershipRepository.GetAllmemberships(x => x.Status == "Active" && x.MemberId == memberId).Any();
    }

    private bool HasAvailableSlots(int SessionId)
    {
        var bookedSlots = _unitOfWork.SessionRepository.GetCountOfBookingSlots(SessionId);
        var session = _unitOfWork.SessionRepository.GetById(SessionId);

        if (session == null) return false;

        var availableSlots = session.Capacity - bookedSlots;
        return (availableSlots > 0) ;

    }

    public bool CancelBooking(int memberId, int sessionId)
    {
        var session = _unitOfWork.SessionRepository.GetById(sessionId);
        if (session is null) return false;

        if (DateTime.Now >= session.StartDate) return false;

      var booking = _unitOfWork.MemberSessionRepository.GetAll(x => x.SessionId == sessionId && x.MemberId == memberId);
        if (!booking.Any()) return false;


        foreach (var item in booking)
        {
            _unitOfWork.MemberSessionRepository.Delete(item);

        }

        return _unitOfWork.SaveChanges() > 0;


    }




    //Future   => DateTime.Now < StartDate
    //Ongoing  => StartDate <= Now <= EndDate
    //Past     => Now > EndDate




}
