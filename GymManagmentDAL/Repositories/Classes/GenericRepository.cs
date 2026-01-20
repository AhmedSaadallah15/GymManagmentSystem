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

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
{
    private readonly GymDbContext _dbContext;

    public GenericRepository(GymDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void Add(T T) => _dbContext.Set<T>().Add(T);
  

    public void Delete(T T) => _dbContext.Remove(T);
  

    public IEnumerable<T> GetAll(Func<T, bool>? condition = null)
    {
        if (condition is null) return _dbContext.Set<T>().AsNoTracking().ToList();

        else return _dbContext.Set<T>().AsNoTracking().Where(condition).ToList();
    }

    public T? GetById(int id) => _dbContext.Set<T>().Find(id);



    public void Update(T T) => _dbContext.Set<T>().Update(T);
  
}
