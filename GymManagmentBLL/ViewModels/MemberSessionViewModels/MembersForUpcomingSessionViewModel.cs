using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.MemberSessionViewModels;

public class MembersForUpcomingSessionViewModel
{
    public int MemberId { get; set; }
    public string MemberName { get; set; } = null!;

    public DateTime BookingDate { get; set; }


}
