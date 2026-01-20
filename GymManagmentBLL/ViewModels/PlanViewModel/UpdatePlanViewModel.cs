using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.PlanViewModel;

public class UpdatePlanViewModel
{

    public string Name { get; set; } = null!;


    [Required(ErrorMessage = "Description Is Required")]
    [StringLength(200 , ErrorMessage = "Description Must Be Less Than 201 Char")]
    public string Description { get; set; } = null!;


    [Required(ErrorMessage = "Duration Days Is Required")]
    [Range(1,365, ErrorMessage = "Duration Days Must Be Between 1 And 365")]
    public int DurationDays { get; set; }

    [Required(ErrorMessage = "Price Is Required")]
    [Range(1, 10000, ErrorMessage = "Price Must Be Between 1 And 10000")]

    public decimal Price { get; set; }



}
