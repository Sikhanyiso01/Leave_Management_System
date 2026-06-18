using System.ComponentModel.DataAnnotations;

namespace Leave_Management_System.web.Models.LeaveTypes
{
    public class LeaveTypeEditViewModel : BaseLeaveTypeViewModel
    {
       

        [Required]
        [Length(4, 50, ErrorMessage = "You have violated the length requirement")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(1, 90)]
        [Display(Name = "Number of Days")]
        public int numberOfDays { get; set; }

    }
}
