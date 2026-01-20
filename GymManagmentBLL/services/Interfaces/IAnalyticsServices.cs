using GymManagementBLL.ViewModels.AnalyticsViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.services.Interfaces;

public interface IAnalyticsServices
{

    AnalyticsViewModel GetAnalyticsData();


}
