using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities;

public class MemberSession : BaseEntity
{

    public int MemberId { get; set; }
    public int SessionId { get; set; }

    public Member Member { get; set; } = null!;
    public Session Session { get; set; } = null!;

    public bool IsAttended { get; set; }

    //bookingDate => createdAt in base 

}
