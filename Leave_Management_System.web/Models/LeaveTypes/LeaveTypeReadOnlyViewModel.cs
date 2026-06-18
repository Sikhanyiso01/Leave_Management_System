using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Leave_Management_System.web.Models.LeaveTypes
{
    public class LeaveTypeReadOnlyViewModel : BaseLeaveTypeViewModel
    {
            
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Number of Days")]
        public int numberOfDays { get; set; }
    }
   
}
