using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces;

public interface IMemberSessionRepository : IGenericRepository<MemberSession>
{

    IEnumerable<MemberSession> GetMemberSessionsById(int id);



}
