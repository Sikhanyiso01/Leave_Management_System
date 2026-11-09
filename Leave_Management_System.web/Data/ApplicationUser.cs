using Microsoft.AspNetCore.Identity;

namespace Leave_Management_System.web.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
        
        public DateOnly DateOfBirth { get; set; }
    }
}
