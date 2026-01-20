using GymManagementBLL.ViewModels.TrainerViewModels;
using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.services.Interfaces;

public interface ITrainerServices
{

    IEnumerable<TrainerViewModel> GetAllTrainers();

    bool CreateTrainer(CreateTrainerViewModel CreateTrainerViewModel);

    TrainerDetailsViewModel? GetTrainerDetails(int id);

    TrainerUpdateViewModel? GetTrainerToUpdate(int id);

    bool UpdateTrainer(int id, TrainerUpdateViewModel trainerUpdate);


    bool DeleteTrainer(int id);


}
