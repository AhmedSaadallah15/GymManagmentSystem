using GymManagementBLL.ViewModels.PlanViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.services.Interfaces;

public interface IPlanServicse
{
    IEnumerable<PlanViewModel> GetAllPlans();


    PlanViewModel? GetPlanDetails(int id);

    UpdatePlanViewModel? GetPlanToUpdate(int id);

    bool UpdatePlan(int id , UpdatePlanViewModel UpdatedPlan );

    bool ToggleStatusPlan(int id);


}
