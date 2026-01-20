using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.MembershipViewModels;

public class MembershipViewModel
{

    public int PlanId { get; set; }
    public int MemberId { get; set; }

    public string PlanName { get; set; } = null!;
    public string MemberName { get; set; } = null!;

    public DateTime EndDate { get; set; }
    public DateTime StartDate { get; set; }



}
