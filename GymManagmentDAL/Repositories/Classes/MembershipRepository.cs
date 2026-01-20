using GymManagementDAL.Repositories.Interfaces;
using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Classes;

public class MembershipRepository : GenericRepository<MemberPlan>, IMembershipRepository
{
    private readonly GymDbContext _dbContext;

    public MembershipRepository(GymDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<MemberPlan> GetAllmemberships(Func<MemberPlan, bool>? filter = null)
    {
        if(filter is null)
        {
            return _dbContext.MemberPlans
                     .Include(e => e.Plan)
                     .Include(e => e.Member)
                     .ToList();
        }
        else
        {
            return _dbContext.MemberPlans
                   .Include(e => e.Plan)
                   .Include(e => e.Member)
                   .Where(filter)
                   .ToList();
        }
    }

    public MemberPlan? GetMemberPlan(Func<MemberPlan, bool>? filter = null)
    {
        var memberships = _dbContext.MemberPlans.FirstOrDefault(filter ?? (_ => true));

        return memberships;
    }
}
