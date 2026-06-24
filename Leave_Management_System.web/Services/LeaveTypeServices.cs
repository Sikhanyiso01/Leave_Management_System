public class LeaveTypeServices(ApplicationDbContext context, IMapper mapper) : ILeaveTypeServices
{
    private ApplicationDbContext _context;
    private IMapper _mapper;

    public async Task<List<LeaveTypeDto>> GetAllLeaveTypesAsync()
    {
        //Gtetting data from the DB
        var data = await _context.LeaveTypes.ToListAsync();
        //converting the Datamodel to a viewModel via Mapper
        var viewData = _mapper.Map<List<LeaveTypeReadOnlyViewModel>>(data);
        return viewData;
    }

    public async Task<T> Get<T?>(int id) where T : class
    {
        var data =  await_context.LeaveTypes.FirstOrDefaultAsync(X => x.Id == id);
        if(data == null)
        {
            return null;
        }

        var viewData = _mapper.Map<T>(data);
        return viewData;
    }

    public async Task Delete(int id)
    {
        var data =  await_context.LeaveTypes.FirstOrDefaultAsync(X => x.Id == id);
        if(data != null)
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
    private bool LeaveTypeExists(int? id)
    {
        return _context.LeaveTypes.Any(e => e.Id == id);
    }

    private async Task<bool> CheckIfLeaveTypeNameExists( string name )
    {
        var lowercaseName = name.ToLower();
        return await _context.LeaveTypes.AnyAsync(q => q.name.ToLower().Equals(lowercaseName));
    }
    private  async Task<bool> CheckIfLeaveTypeNameExistsForEdit(LeaveTypeEditViewModel leavetype)
    {
        var lowercaseName = leavetype.Name.ToLower();
        return await _context.LeaveTypes.AnyAsync(q => q.name.ToLower().Equals(lowercaseName) && 
        q.Id != leavetype.Id);
    }
}