using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using school_itg_shadi.Models;
using school_itg_shadi.data;

namespace school_itg_shadi.Controllers
{
    public class StudentMarksController : Controller
    {
        private readonly school_shadiContext _context;

        public StudentMarksController(school_shadiContext context)
        {
            _context = context;
        }

        // GET: StudentMarks
       
        public async Task<IActionResult> Index(string searchString)
        {
            var Marks = from m in _context.StudentMarks.Include(s => s.Student).Include(s => s.StudentNameNavigation)
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                Marks = Marks.Where(s => s.StudentNameNavigation.Name.Contains(searchString));
            }

            return View(await Marks.ToListAsync());
        }





        public async Task<IActionResult> List(string searchString)
        {
            var Marks = from m in _context.StudentMarks.Include(s => s.Student).Include(s => s.StudentNameNavigation)
            select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                Marks = Marks.Where(s => s.StudentNameNavigation.Name.Contains(searchString));
            }

            return View(await Marks.ToListAsync());
        }



        public async Task<IActionResult> List_Details(int? id)
        {
            if (id == null || _context.StudentMarks == null)
            {
                return NotFound();
            }

            var studentMark = await _context.StudentMarks
                .Include(s => s.Student)
                .Include(s => s.StudentNameNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentMark == null)
            {
                return NotFound();
            }

            return View(studentMark);
        }

        // GET: StudentMarks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StudentMarks == null)
            {
                return NotFound();
            }

            var studentMark = await _context.StudentMarks
                .Include(s => s.Student)
                .Include(s => s.StudentNameNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentMark == null)
            {
                return NotFound();
            }

            return View(studentMark);
        }

        // GET: StudentMarks/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Clients, "UserId", "UserId");
            ViewData["StudentName"] = new SelectList(_context.Clients, "Name", "Name");
            return View();
        }

        // POST: StudentMarks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentName,StudentId,Physics,Math,Chemistry,English,History")] StudentMark studentMark)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentMark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Clients, "UserId", "Name", studentMark.StudentId);
            ViewData["StudentName"] = new SelectList(_context.Clients, "Name", "Name", studentMark.StudentName);
            return View(studentMark);
        }

        // GET: StudentMarks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StudentMarks == null)
            {
                return NotFound();
            }

            var studentMark = await _context.StudentMarks.FindAsync(id);
            if (studentMark == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.Clients, "UserId", "Name", studentMark.StudentId);
            ViewData["StudentName"] = new SelectList(_context.Clients, "Name", "Name", studentMark.StudentName);
            return View(studentMark);
        }

        // POST: StudentMarks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentName,StudentId,Physics,Math,Chemistry,English,History")] StudentMark studentMark)
        {
            if (id != studentMark.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentMark);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentMarkExists(studentMark.Id))
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
            ViewData["StudentId"] = new SelectList(_context.Clients, "UserId", "Name", studentMark.StudentId);
            ViewData["StudentName"] = new SelectList(_context.Clients, "Name", "Name", studentMark.StudentName);
            return View(studentMark);
        }

        // GET: StudentMarks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StudentMarks == null)
            {
                return NotFound();
            }

            var studentMark = await _context.StudentMarks
                .Include(s => s.Student)
                .Include(s => s.StudentNameNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentMark == null)
            {
                return NotFound();
            }

            return View(studentMark);
        }

        // POST: StudentMarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StudentMarks == null)
            {
                return Problem("Entity set 'school_shadiContext.StudentMarks'  is null.");
            }
            var studentMark = await _context.StudentMarks.FindAsync(id);
            if (studentMark != null)
            {
                _context.StudentMarks.Remove(studentMark);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentMarkExists(int id)
        {
          return (_context.StudentMarks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
