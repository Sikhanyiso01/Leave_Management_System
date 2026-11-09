using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Leave_Management_System.web.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<LeaveType> LeaveTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                   
                },
                new IdentityRole
                {
                    Id = "2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e",
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE",
                  
                },
                new IdentityRole
                {
                    Id = "3c4d5e6f-7a8b-9c0d-1e2f-3a4b5c6d7e8f",
                    Name = "Supervisor",
                    NormalizedName = "SUPERVISOR",
                  
                }
            );

            var hash = new PasswordHasher<ApplicationUser>();
            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "4d5e6f7a-8b9c-0d1e-2f3a-4b5c6d7e8f9a",
                    UserName = "admin@localhost.com",
                    NormalizedUserName = "ADMIN@LOCALHOST.COM",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "ADMIN@LOCALHOST.COM",
                    EmailConfirmed = true,
                    FirstName = "System",
                    LastName = "Admin",
                    DateOfBirth = new DateOnly(1990, 1, 1),
                    PasswordHash = hash.HashPassword(null, "Password123!")
                });

            // Assign Admin to Administrator Role
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d",
                    UserId = "4d5e6f7a-8b9c-0d1e-2f3a-4b5c6d7e8f9a"
                });
        }
    }
}
