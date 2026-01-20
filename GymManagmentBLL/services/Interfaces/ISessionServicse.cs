using GymManagementBLL.ViewModels.SessionViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.services.Interfaces;

public interface ISessionServicse
{

    IEnumerable<SessionViewModel> GetAllSessions();

    SessionViewModel? GetSessionDetails(int id);

    bool CreateSession(CreateSessionViewModel createSession);

    UpdateSessionViewModel? GetSessionToUpdate(int id);
    bool UpdateSession(int id , UpdateSessionViewModel updatedSession);
    bool DeleteSession(int id );

    IEnumerable<TrainerSelectViewModel> GetTarinersForSelect();
    IEnumerable<CategorySelectViewModel> GetCategorysForSelect();

}
