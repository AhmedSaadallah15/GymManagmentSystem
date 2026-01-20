using GymManagmentDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Entities
{
    public class Trainer : GymUser
    {

        public Specialties Specialties { get; set; }

        //hiredate => createdAt from baseEntity

        public ICollection<Session> TrainerSessions { get; set; } = null!;

    }
}
