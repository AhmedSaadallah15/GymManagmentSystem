using GymManagmentDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.MemberViewModels;

public class HealthRecordViewModel
{
    [Required(ErrorMessage = "Height Is Required")]
    [Range(1,300 , ErrorMessage = "Height Must Be Between 1 And 300")]
    public decimal Height { get; set; }

    [Required(ErrorMessage = "Weight Is Required")]
    [Range(1, 300, ErrorMessage = "Weight Must Be Between 1 And 300")]
    public decimal Weight { get; set; }

    [Required(ErrorMessage = "Blood Type Is Required")]
    public BloodType BloodType { get; set; }

    public string? Note { get; set; }




}
