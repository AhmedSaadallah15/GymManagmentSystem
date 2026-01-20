using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using GymManagmentDAL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Classes;

public class MemberSessionRepository : GenericRepository<MemberSession> , IMemberSessionRepository
{
    private readonly GymDbContext _dbContext;

    public MemberSessionRepository(GymDbContext dbContext):base(dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<MemberSession> GetMemberSessionsById(int id)
    {
        return _dbContext.MemberSessions
                                .Where(x=>x.SessionId == id)
                                .Include(x=>x.Member)
                                .ToList();
       
    }
}
