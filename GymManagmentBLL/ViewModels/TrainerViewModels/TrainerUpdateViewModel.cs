using GymManagmentDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.TrainerViewModels;

public class TrainerUpdateViewModel
{

    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Phone Is Required")]
    [Phone(ErrorMessage = "Invalid Phone")]
    [RegularExpression(@"^(010|012|015|011)\d{8}$", ErrorMessage = "Phone Must Be Egyptian Phone Number")]
    [DataType(DataType.PhoneNumber)]
    public string Phone { get; set; } = null!;

    [Required(ErrorMessage = "Email Is Required")]
    [EmailAddress(ErrorMessage = "Invalid Email")]
    [DataType(DataType.EmailAddress)]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "Email Must Be Between 5 And 100")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Building Number Is Required")]
    [Range(1, 500, ErrorMessage = "Building Number Must Be Between 1 And 500")]
    public int BuildingNumber { get; set; }


    [Required(ErrorMessage = "Street Is Required")]
    [StringLength(30, MinimumLength = 2, ErrorMessage = "Street Must Be Between 2 And 30 Chars")]
    public string Street { get; set; } = null!;

    [Required(ErrorMessage = "City Is Required")]
    [StringLength(30, MinimumLength = 2, ErrorMessage = "City Must Be Between 2 And 30 Chars")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City Can Contain Only Letters And Spaces")]
    public string City { get; set; } = null!;


    [Required(ErrorMessage = "Specialties Is Required")]
    public Specialties Specialties { get; set; }


}
