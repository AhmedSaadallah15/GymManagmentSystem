using GymManagementBLL.services.Interfaces;
using GymManagementBLL.ViewModels.AnalyticsViewModel;
using GymManagementDAL.UnitOfWork;
using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.services.Classes;

public class AnalyticsServices : IAnalyticsServices
{
    private readonly IUnitOfWork _unitOfWork;

    public AnalyticsServices(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public AnalyticsViewModel GetAnalyticsData()
    {
        var sessions = _unitOfWork.GetReporitory<Session>().GetAll();


        return new AnalyticsViewModel()
        {
            ActiveMembers = _unitOfWork.GetReporitory<MemberPlan>().GetAll(x => x.Status == "Active").Count(),
            TotalMembers = _unitOfWork.GetReporitory<Member>().GetAll().Count() ,
            Trainers = _unitOfWork.GetReporitory<Trainer>().GetAll().Count(),
            UpcomingSessions = sessions.Count(x => x.StartDate > DateTime.Now),
            OngoingSessions = sessions.Count(x => x.EndDate >= DateTime.Now && x.StartDate <= DateTime.Now),
            CompletedSessions = sessions.Count(x => x.EndDate < DateTime.Now),

        };
    }
}
