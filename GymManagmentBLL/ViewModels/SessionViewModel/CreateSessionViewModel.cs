using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.SessionViewModel;

public class CreateSessionViewModel
{
    [Required(ErrorMessage = "Category Is Required")]
    [Display(Name = "Category")]

    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Trainer Is Required")]
    [Display(Name = "Trainer")]

    public int TrainerId { get; set; }

    [Required(ErrorMessage = "Capacity Is Required")]
    [Range(1,25, ErrorMessage = "Capacity must be between 0 and 25")]
    public int Capacity { get; set; }

    [Required(ErrorMessage = "Description Is Required")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 500 characters")]
    [Display(Name = "Description")]

    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "Start date Is Required")]
    [Display(Name = "Start Date & Time")]

    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "End date Is Required")]
    [Display(Name = "End Date & Time")]

    public DateTime EndDate { get; set; }


}
