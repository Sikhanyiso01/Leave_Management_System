using AutoMapper;
using Leave_Management_System.web.Data;
using Leave_Management_System.web.Models.LeaveTypes;
using Microsoft.EntityFrameworkCore;

namespace Leave_Management_System.web.Services
{
    public class LeaveTypeServices(ApplicationDbContext context, IMapper mapper) : ILeaveTypesService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<List<LeaveTypeReadOnlyViewModel>> GetAll()
        {
            var data = await _context.LeaveTypes.ToListAsync();
            var viewData = _mapper.Map<List<LeaveTypeReadOnlyViewModel>>(data);
            return viewData;
        }

        public async Task<T?> Get<T>(int id) where T : class
        {
            var data = await _context.LeaveTypes.FirstOrDefaultAsync(x => x.Id == id);
            if (data == null)
            {
                return null;
            }

            var viewData = _mapper.Map<T>(data);
            return viewData;
        }

        public async Task Remove(int id)
        {
            var data = await _context.LeaveTypes.FirstOrDefaultAsync(x => x.Id == id);
            if (data != null)
            {
                _context.Remove(data);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Create(LeaveTypeCreateViewModel model)
        {
            var leavetype = _mapper.Map<LeaveType>(model);
            _context.Add(leavetype);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(LeaveTypeEditViewModel model)
        {
            var leaveType = _mapper.Map<LeaveType>(model);
            _context.Update(leaveType);
            await _context.SaveChangesAsync();
        }

        public bool LeaveTypeExists(int id)
        {
            return _context.LeaveTypes.Any(e => e.Id == id);
        }

        public async Task<bool> CheckIfLeaveTypeNameExists(string name)
        {
            var lowercaseName = name.ToLower();
            return await _context.LeaveTypes.AnyAsync(q => q.name.ToLower().Equals(lowercaseName));
        }

        public async Task<bool> CheckIfLeaveTypeNameExistsForEdit(LeaveTypeEditViewModel leavetype)
        {
            var lowercaseName = leavetype.Name.ToLower();
            return await _context.LeaveTypes.AnyAsync(q => q.name.ToLower().Equals(lowercaseName) &&
                q.Id != leavetype.Id);
        }
    }
}
