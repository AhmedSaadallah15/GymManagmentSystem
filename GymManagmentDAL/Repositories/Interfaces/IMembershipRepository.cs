using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces;

public interface IMembershipRepository : IGenericRepository<MemberPlan>
{

    IEnumerable<MemberPlan> GetAllmemberships(Func<MemberPlan, bool>?filter = null);

    MemberPlan? GetMemberPlan(Func<MemberPlan, bool>? filter = null);


}
