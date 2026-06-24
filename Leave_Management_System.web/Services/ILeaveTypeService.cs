using Leave_Management_System.web.Models.LeaveTypes;

namespace Leave_Management_System.web.Services
{
    public interface ILeaveTypesService
    {
        Task Create(LeaveTypeCreateViewModel model);

        Task Edit(LeaveTypeEditViewModel model);

        Task<T?> Get<T>(int id) where T : class;

        Task<List<LeaveTypeReadOnlyViewModel>> GetAll();

        Task Remove(int id);
    }
}
