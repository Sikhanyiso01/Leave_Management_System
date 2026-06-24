using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Leave_Management_System.web.Data;
using Leave_Management_System.web.Models.LeaveTypes;
using AutoMapper;

public class LeaveTypesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private const string NameExistValidationMessage = "This Leave Type Already exists";

    public LeaveTypesController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        this._mapper = mapper;
    }

    // GET: LEAVETYPES
    public async Task<IActionResult> Index()    
    {
        var data = await _context.LeaveTypes.ToListAsync();

    
        var viewData = _mapper.Map<List<LeaveTypeReadOnlyViewModel>>(data);
        return View(viewData);
    }

    // GET: LEAVETYPES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leavetype = await _context.LeaveTypes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (leavetype == null)
        {
            return NotFound();
        }

        var viewData = _mapper.Map<LeaveTypeReadOnlyViewModel>(leavetype);
        return View(viewData);
    }

    // GET: LEAVETYPES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: LEAVETYPES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LeaveTypeCreateViewModel LeaveTypeCreate)
    {
        if(await CheckIfLeaveTypeNameExists(LeaveTypeCreate.Name))
        {
            ModelState.AddModelError(nameof(LeaveTypeCreate.Name), 
            NameExistValidationMessage);
        }

        if (ModelState.IsValid)
        {
            var leavetype = _mapper.Map<LeaveType>(LeaveTypeCreate);
            _context.Add(leavetype);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(LeaveTypeCreate);
    }
   

    // GET: LEAVETYPES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leavetype = await _context.LeaveTypes.FindAsync(id);
        if (leavetype == null)
        {
            return NotFound();
        }
        var viewData = _mapper.Map<LeaveTypeEditViewModel>(leavetype);
        return View(viewData);
    }

    // POST: LEAVETYPES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, LeaveTypeEditViewModel leavetypeEdit)
    {
        if (id != leavetypeEdit.Id)
        {
            return NotFound();
        }

         if(await CheckIfLeaveTypeNameExistsForEdit(leavetypeEdit))
        {
            ModelState.AddModelError(nameof(leavetypeEdit.Name), 
            NameExistValidationMessage);
        }

        if (ModelState.IsValid)
        {
            try
            {
                var leaveType = _mapper.Map<LeaveType>(leavetypeEdit);
                _context.Update(leaveType);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveTypeExists(leavetypeEdit.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(leavetypeEdit);
    }

    // GET: LEAVETYPES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leavetype = await _context.LeaveTypes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (leavetype == null)
        {
            return NotFound();
        }
        var viewData = _mapper.Map<LeaveTypeReadOnlyViewModel>(leavetype);


        return View(viewData);
    }

    // POST: LEAVETYPES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var leavetype = await _context.LeaveTypes.FindAsync(id);
        if (leavetype != null)
        {
            _context.LeaveTypes.Remove(leavetype);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
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
