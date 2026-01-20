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

public class SessionRepository : GenericRepository<Session> , ISessionRepository
{
    private readonly GymDbContext _dbContext;
    public SessionRepository(GymDbContext dbContext) :base(dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Session> GetAllSessionsAndTrainerAndcategory()
    {
        return  _dbContext.Sessions
                            .Include(t => t.SessionTrainer)
                            .Include(t => t.Category)
                            .ToList();


    }

    public int GetCountOfBookingSlots(int id)
    {
        return _dbContext.MemberSessions.Count(x => x.SessionId == id);
    }

    public Session? GetSessionWithTrainerAndCategory(int id)
    {
        return _dbContext.Sessions
                         .Include(t => t.SessionTrainer)
                         .Include(t => t.Category)
                         .FirstOrDefault(x=>x.Id == id);
    }
}
