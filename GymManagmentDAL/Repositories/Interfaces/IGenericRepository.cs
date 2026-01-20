using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity , new()
{
    //GetAll
    IEnumerable<T> GetAll(Func<T, bool>? condition = null);

    //GetById

    T GetById(int id);

    //Add 

    void Add(T T);
    //Update
    void Update(T T);

    //Delete 
    void Delete(T T);




}
