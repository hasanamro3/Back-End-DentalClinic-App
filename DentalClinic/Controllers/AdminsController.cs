using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DentalClinic.Data;
using DentalClinic.Models;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

namespace DentalClinic.Controllers
{
     [Authorize]
    public class AdminsController : Controller
    {
        private readonly DataContext _context;
        
        public AdminsController(DataContext context)
        {
            _context = context;
        }
        
        [AllowAnonymous]
       
        // GET: Admins
        public async Task<IActionResult> Index(string? serachQuery,string? sortBy)
        {
            List<Admin> dataContext = await _context.tblAdmins.
                Include(d => d.Patient).
                Include(d => d.Treatment).ToListAsync();

            if (string.IsNullOrEmpty(serachQuery) == false)
            {
                dataContext = dataContext.Where(d => d.Patient.PatientName.ToLower()
                .Contains(serachQuery.ToLower())).ToList();
            }

            if (sortBy == "tddesc")
            {
                dataContext = dataContext.OrderByDescending(d => d.AppointmentDate).ToList();
            }
            else if (sortBy == "tdasc")
            {
                dataContext = dataContext.OrderBy(d => d.AppointmentDate).ToList();
            }
            ViewData["o"]=sortBy;
            return View(dataContext);
        }

        // GET: Admins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.tblAdmins
                .Include(a => a.Patient)
                .Include(a => a.Treatment)
                .FirstOrDefaultAsync(m => m.AdminId == id);
           
            if (admin == null)
            {
                return NotFound();
            }
     
            return View(admin);
        }

        // GET: Admins/Create
        public IActionResult Create()
        {
            ViewData["PatientId"] = new SelectList(_context.tblPatients, "PatientId", "PatientName");
            ViewData["TreatmentId"] = new SelectList(_context.tblTreatments, "TreatmentId", "TretmentType");
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdminId,PatientId,TreatmentId,AppointmentDate,TreatmentStatus,Notes")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientId"] = new SelectList(_context.tblPatients, "PatientId", "PatientName", admin.PatientId);
            ViewData["TreatmentId"] = new SelectList(_context.tblTreatments, "TreatmentId", "TreatmentId", admin.TreatmentId);
            return View(admin);
        }

        // GET: Admins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.tblAdmins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            ViewData["PatientId"] = new SelectList(_context.tblPatients, "PatientId", "PatientName", admin.PatientId);
            ViewData["TreatmentId"] = new SelectList(_context.tblTreatments, "TreatmentId", "TreatmentId", admin.TreatmentId);
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdminId,PatientId,TreatmentId,AppointmentDate,TreatmentStatus,Notes")] Admin admin)
        {
            if (id != admin.AdminId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.AdminId))
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
            ViewData["PatientId"] = new SelectList(_context.tblPatients, "PatientId", "PatientName", admin.PatientId);
            ViewData["TreatmentId"] = new SelectList(_context.tblTreatments, "TreatmentId", "TreatmentId", admin.TreatmentId);
            return View(admin);
        }

        // GET: Admins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.tblAdmins
                .Include(a => a.Patient)
                .Include(a => a.Treatment)
                .FirstOrDefaultAsync(m => m.AdminId == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admin = await _context.tblAdmins.FindAsync(id);
            if (admin != null)
            {
                _context.tblAdmins.Remove(admin);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminExists(int id)
        {
            return _context.tblAdmins.Any(e => e.AdminId == id);
        }
       
    }
}
