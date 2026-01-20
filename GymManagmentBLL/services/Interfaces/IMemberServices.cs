using GymManagementBLL.ViewModels.MemberViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.services.Interfaces;

public interface IMemberServices
{

    IEnumerable<MemberViewModel> GetAllMembers ();

    bool CreateMember(CreateMemberViewModel createMember);


    MemberViewModel? GetMemberDetails(int id);

     HealthRecordViewModel? GetHealthrecordMemberDetails(int id );

    MemberUpdateViewModel? GetMemberToUpdate(int id);

    bool UpdateMember(int id ,MemberUpdateViewModel updateMember);


    bool RemoveMember(int id);


}
