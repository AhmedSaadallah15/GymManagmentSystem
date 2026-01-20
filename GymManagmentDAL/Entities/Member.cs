using GymManagementDAL.Entities;

namespace GymManagmentDAL.Entities;






public class Member : GymUser
{
    //Join Date => CreatedAt from BaseEntity
    public string Photo { get; set; } = null!;


    public HealthRecord HealthRecord { get; set; } = null!;

    public ICollection<MemberPlan> MemberPlans { get; set; } = null!;

    public ICollection<MemberSession> MemberSessions { get; set; } = null!;

}
