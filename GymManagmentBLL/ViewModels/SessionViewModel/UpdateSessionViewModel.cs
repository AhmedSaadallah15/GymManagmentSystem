using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.SessionViewModel;

public class UpdateSessionViewModel
{

    [Required(ErrorMessage = "Trainer Is Required")]
    [Display(Name = "Trainer")]

    public int TrainerId { get; set; }

    [Required(ErrorMessage = "Description Is Required")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 500 characters")]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "Start date Is Required")]
    [Display(Name = "Start Date & Time")]

    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "End date Is Required")]
    [Display(Name = "End Date & Time")]

    public DateTime EndDate { get; set; }




}
