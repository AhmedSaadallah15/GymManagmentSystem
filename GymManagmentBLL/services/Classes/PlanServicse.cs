using AutoMapper;
using GymManagementBLL.services.Interfaces;
using GymManagementBLL.ViewModels.PlanViewModel;
using GymManagementDAL.UnitOfWork;
using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.services.Classes;

public class PlanServicse : IPlanServicse
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PlanServicse(IUnitOfWork unitOfWork , IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }



    public IEnumerable<PlanViewModel> GetAllPlans()
    {
        var plans = _unitOfWork.GetReporitory<Plan>().GetAll();
        if (plans is null || !plans.Any()) return [];

        #region ManualMap
        //var PlanViewModel = plans.Select(p => new PlanViewModel()
        //{
        //    Id = p.Id,
        //    Name = p.Name,
        //    Description = p.Description,
        //    DurationDays = p.DurationDays,
        //    IsActive = p.IsActive,
        //    Price = p.Price,

        //});

        //return PlanViewModel;
        #endregion

        return _mapper.Map<IEnumerable<PlanViewModel>>(plans);

    }

    public PlanViewModel? GetPlanDetails(int id)
    {
        var plan = _unitOfWork.GetReporitory<Plan>().GetById(id);
        if (plan is null) return null;

        #region ManualMap
        //var planViewModel = new PlanViewModel()
        //{
        //    Name = plan.Name,
        //    Description = plan.Description,
        //    DurationDays = plan.DurationDays,
        //    IsActive = plan.IsActive,
        //    Price = plan.Price,
        //};
        // return planViewModel;
        #endregion

        return _mapper.Map<Plan, PlanViewModel>(plan);


    }

    public UpdatePlanViewModel? GetPlanToUpdate(int id)
    {
        var plan = _unitOfWork.GetReporitory<Plan>().GetById(id);
        if (plan is null || plan.IsActive == false || HasActiveMemberShip(id)) return null;

        #region ManualMapping 
        //var planViewModel = new UpdatePlanViewModel()
        //{
        //    Description = plan.Description,
        //    Price = plan.Price ,
        //    DurationDays = plan.DurationDays,

        //};
        //return planViewModel;
        #endregion

        return _mapper.Map<UpdatePlanViewModel>(plan);
    }

    public bool UpdatePlan(int id, UpdatePlanViewModel UpdatedPlan)
    {
        var plan = _unitOfWork.GetReporitory<Plan>().GetById(id);
        if (plan is null || HasActiveMemberShip(id)) return false;

        try
        {
            #region manual Map
            //(plan.Description, plan.Price, plan.DurationDays, plan.UpdatedAt) =
            //    (UpdatedPlan.Description, UpdatedPlan.Price, UpdatedPlan.DurationDays, DateTime.Now);

            #endregion
            _mapper.Map(UpdatedPlan , plan);

            _unitOfWork.GetReporitory<Plan>().Update(plan);
            return _unitOfWork.SaveChanges() > 0;
        }
        catch (Exception)
        {

            return false;
        }


    }

    public bool ToggleStatusPlan(int id)
    {
        var plan = _unitOfWork.GetReporitory<Plan>().GetById(id);
        if (plan is null || HasActiveMemberShip(id)) return false;

        plan.IsActive = plan.IsActive == true ? false : true;
        plan.UpdatedAt = DateTime.Now;

        try
        {
            _unitOfWork.GetReporitory<Plan>().Update(plan);
           return _unitOfWork.SaveChanges() > 0;
        }
        catch (Exception)
        {

            return false;
        }

    }



    private bool HasActiveMemberShip(int id)
    {
        var activeMemberShip =
            _unitOfWork.GetReporitory<MemberPlan>().GetAll(x=>x.PlanId == id && x.Status == "Active");

        return activeMemberShip.Any();

    }

}
