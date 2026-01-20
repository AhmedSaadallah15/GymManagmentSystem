using GymManagementBLL.ViewModels.MembershipViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.services.Interfaces;

public interface IMembershipServices
{

    IEnumerable<MembershipViewModel> GetAllMemberships();

    bool CreateMembership(CreateMembershipViewModel model);
    bool CancelMembership(int memberId);
    IEnumerable<PlanSelectViewModel> GetPlansForSelect();
    IEnumerable<MemberSelectViewModel> GetMembersForSelect();
}
