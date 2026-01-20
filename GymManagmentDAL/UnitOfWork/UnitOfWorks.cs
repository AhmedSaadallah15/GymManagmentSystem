using GymManagementDAL.Repositories.Classes;
using GymManagementDAL.Repositories.Interfaces;
using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.UnitOfWork;

public class UnitOfWorks : IUnitOfWork
{
    private readonly Dictionary<Type, object> _repositories = new();
    private readonly GymDbContext _dbContext;

    public UnitOfWorks(GymDbContext dbContext , 
        ISessionRepository sessionRepository , 
        IMembershipRepository membershipRepository ,
        IMemberSessionRepository memberSessionRepository)
    {
        _dbContext = dbContext;
        SessionRepository = sessionRepository;
        MembershipRepository = membershipRepository;
        MemberSessionRepository = memberSessionRepository;
    }

    public ISessionRepository SessionRepository { get; }
    public IMembershipRepository MembershipRepository { get; }

    public IMemberSessionRepository MemberSessionRepository { get; }

    public IGenericRepository<TEntity> GetReporitory<TEntity>() where TEntity : BaseEntity, new()
    {
        var entityType = typeof(TEntity);
        if (_repositories.TryGetValue(entityType, out var repo))
            return (IGenericRepository<TEntity>) repo;

        var newRepo = new GenericRepository<TEntity>(_dbContext);

        _repositories[entityType] = newRepo;

        return newRepo;
    }

    public int SaveChanges()
    {
       return _dbContext.SaveChanges();   
    }
}
