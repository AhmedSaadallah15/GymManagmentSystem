using GymManagementDAL.Repositories.Interfaces;
using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.UnitOfWork;

public interface IUnitOfWork
{

    IGenericRepository<TEntity> GetReporitory<TEntity>() where TEntity : BaseEntity , new();
    public ISessionRepository SessionRepository { get; }
    public IMembershipRepository MembershipRepository { get; }
    public IMemberSessionRepository MemberSessionRepository { get; }
    int SaveChanges();

}
