using AutoMapper;
using GymManagementBLL.services.Interfaces;
using GymManagementBLL.ViewModels.MembershipViewModels;
using GymManagementDAL.UnitOfWork;
using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.services.Classes;

public class MembershipServices : IMembershipServices
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MembershipServices(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public IEnumerable<MembershipViewModel> GetAllMemberships()
    {
        //m=>m.Status.ToLower() == "active"
        var memberships = _unitOfWork.MembershipRepository.GetAllmemberships();
        if (memberships is null || !memberships.Any()) return [];


        return _mapper.Map<IEnumerable<MembershipViewModel>>(memberships);
    }

 
    public bool CreateMembership(CreateMembershipViewModel createMembership)
    {
        // if plan exist
        // if member exist
        // if plan is active 
        // if meber have more than one active membership at the same time
        if (!PlanExists(createMembership.PlanId)) return false;
        if(!MemberExists(createMembership.MemberId)) return false;
        if(!IsPlanActive(createMembership.PlanId)) return false;
        if(HasActiveMembership(createMembership.MemberId)) return false;

        try
        {
            var plan = _unitOfWork.GetReporitory<Plan>().GetById(createMembership.PlanId);
            var membership = _mapper.Map<MemberPlan>(createMembership);
            membership.EndDate = DateTime.Now.AddDays(plan.DurationDays);
       

            _unitOfWork.GetReporitory<MemberPlan>().Add(membership);
            return _unitOfWork.SaveChanges() > 0;

        }catch
        {
            return false;
        }

    }

   
    public bool CancelMembership(int memberId)
    {
        var membership = _unitOfWork.MembershipRepository.GetMemberPlan(x=>x.MemberId == memberId && x.Status.ToLower() == "active");
        if (membership is null) return false;

        try
        {
            _unitOfWork.MembershipRepository.Delete(membership);
          return  _unitOfWork.SaveChanges() > 0;

        }
        catch 
        {

            return false;
        }
    }

    public IEnumerable<PlanSelectViewModel> GetPlansForSelect()
    {
        var plans = _unitOfWork.GetReporitory<Plan>().GetAll();

        return _mapper.Map<IEnumerable<PlanSelectViewModel>>(plans);
    }
    public IEnumerable<MemberSelectViewModel> GetMembersForSelect()
    {
        var members = _unitOfWork.GetReporitory<Member>().GetAll();

        return _mapper.Map<IEnumerable<MemberSelectViewModel>>(members);
    }


    private bool HasActiveMembership(int id)
    {
        return _unitOfWork.MembershipRepository.GetAllmemberships(m=>m.Status == "Active" && m.MemberId == id).Any();
    }


    private bool MemberExists(int id)
    {
        return _unitOfWork.GetReporitory<Member>().GetById(id) is not null;
    }
    private bool PlanExists(int id)
    {
        return _unitOfWork.GetReporitory<Plan>().GetById(id) is not null;
    }

   private bool IsPlanActive(int id)
    {
        return _unitOfWork.GetReporitory<Plan>().GetAll(p => p.IsActive == true).Any();
    }

  
}
