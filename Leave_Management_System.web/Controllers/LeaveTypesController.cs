using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Leave_Management_System.web.Data;
using Leave_Management_System.web.Models.LeaveTypes;
using Leave_Management_System.web.Services;
using AutoMapper;

public class LeaveTypesController(ILeaveTypesService leaveTypesService) : Controller
{
   
    private const string NameExistValidationMessage = "This Leave Type Already exists";
    private readonly ILeaveTypesService _leaveTypesService = leaveTypesService;
  

    // GET: LEAVETYPES
    public async Task<IActionResult> Index()    
    {
        var viewData = await _leaveTypesService.GetAll();
        return View(viewData);
    }

    // GET: LEAVETYPES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leavetype = await _leaveTypesService.Get<LeaveTypeReadOnlyViewModel>(id.Value);
        if (leavetype == null)
        {
            return NotFound();
        }

       
        return View(leavetype);
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
        if(await _leaveTypesService.CheckIfLeaveTypeNameExists(LeaveTypeCreate.Name))
        {
            ModelState.AddModelError(nameof(LeaveTypeCreate.Name), 
            NameExistValidationMessage);
        }

        if (ModelState.IsValid)
        {
            await _leaveTypesService.Create(LeaveTypeCreate);
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

        var leavetype = await _leaveTypesService.Get<LeaveTypeEditViewModel>(id.Value);
        if (leavetype == null)
        {
            return NotFound();
        }
        return View(leavetype);
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

         if(await _leaveTypesService.CheckIfLeaveTypeNameExistsForEdit(leavetypeEdit))
        {
            ModelState.AddModelError(nameof(leavetypeEdit.Name), 
            NameExistValidationMessage);
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _leaveTypesService.Edit(leavetypeEdit);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_leaveTypesService.LeaveTypeExists(leavetypeEdit.Id))
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

            var leavetype = await _leaveTypesService.Get<LeaveTypeReadOnlyViewModel>(id.Value);
        if (leavetype == null)
        {
            return NotFound();
        }

        return View(leavetype);
    }

    // POST: LEAVETYPES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        await _leaveTypesService.Remove(id.Value);
        return RedirectToAction(nameof(Index));
    }

   
}
