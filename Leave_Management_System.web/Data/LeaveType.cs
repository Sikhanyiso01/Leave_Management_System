using System.ComponentModel.DataAnnotations.Schema;

namespace Leave_Management_System.web.Data
{
    public class LeaveType
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string name { get; set; }
        public int numberOfDays { get; set; }
    }
    
}
