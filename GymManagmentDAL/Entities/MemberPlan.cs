using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Entities
{
    public class MemberPlan : BaseEntity
    {

        public int MemberId { get; set; }
        public int PlanId { get; set; }

        public Member Member { get; set; } = null!;
        public Plan Plan { get; set; } = null!;

        //startDate will be CreatedAt from Base 
        public DateTime EndDate { get; set; }

        public string Status { 
            get 
            { 
                if(EndDate < DateTime.Now)
                {
                    return "Expired";
                }
                else
                {
                    return "Active";
                }
            } }



    }
}
