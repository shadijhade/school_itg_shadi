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
    public class AssociationsController : Controller
    {
        private readonly school_shadiContext _context;

        public AssociationsController(school_shadiContext context)
        {
            _context = context;
        }

        // GET: Associations
        public async Task<IActionResult> Index()
        {
            var school_shadiContext = _context.Associations.Include(a => a.Class).Include(a => a.Client);
            return View(await school_shadiContext.ToListAsync());
        }

        // GET: Associations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Associations == null)
            {
                return NotFound();
            }

            var association = await _context.Associations
                .Include(a => a.Class)
                .Include(a => a.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (association == null)
            {
                return NotFound();
            }

            return View(association);
        }

        // GET: Associations/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassName");
            ViewData["ClientId"] = new SelectList(_context.Clients, "UserId" , "Name");
            return View();
        }

        // POST: Associations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientId,ClassId")] Association association)
        {
            if (ModelState.IsValid)
            {
                _context.Add(association);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId", association.ClassId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "UserId", "Name", association.ClientId);
            return View(association);
        }

        // GET: Associations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Associations == null)
            {
                return NotFound();
            }

            var association = await _context.Associations.FindAsync(id);
            if (association == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId", association.ClassId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "UserId", "Name", association.ClientId);
            return View(association);
        }

        // POST: Associations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,ClassId")] Association association)
        {
            if (id != association.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(association);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssociationExists(association.Id))
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
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId", association.ClassId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "UserId", "Name", association.ClientId);
            return View(association);
        }

        // GET: Associations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Associations == null)
            {
                return NotFound();
            }

            var association = await _context.Associations
                .Include(a => a.Class)
                .Include(a => a.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (association == null)
            {
                return NotFound();
            }

            return View(association);
        }

        // POST: Associations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Associations == null)
            {
                return Problem("Entity set 'school_shadiContext.Associations'  is null.");
            }
            var association = await _context.Associations.FindAsync(id);
            if (association != null)
            {
                _context.Associations.Remove(association);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssociationExists(int id)
        {
          return (_context.Associations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
