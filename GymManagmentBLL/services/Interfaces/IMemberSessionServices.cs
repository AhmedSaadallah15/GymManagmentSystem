using GymManagementBLL.ViewModels.MemberSessionViewModels;
using GymManagementBLL.ViewModels.MembershipViewModels;
using GymManagementBLL.ViewModels.SessionViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.services.Interfaces;

public interface IMemberSessionServices
{

    IEnumerable<SessionViewModel> GetSessionWithCategoryAndTrainer();

    IEnumerable<MembersForUpcomingSessionViewModel> GetMembersForUpcomingSession(int id);
    IEnumerable<MembersForOngoingSessionsViewModel> GetMembersForOngoingSessions(int id);

    bool MarkAttendance(int memberId, int sessionId);

    bool CancelBooking(int memberId, int sessionId);
    IEnumerable<MemberSelectViewModel> GetMembersForSelect();
    bool CreateMemberSession(CreateMemberSessionViewModel model);


}
