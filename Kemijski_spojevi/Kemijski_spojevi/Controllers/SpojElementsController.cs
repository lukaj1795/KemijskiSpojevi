using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kemijski_spojevi.Database;

namespace Kemijski_spojevi.Controllers
{
    public class SpojElementsController : Controller
    {
        private readonly DatabaseContext _context;

        public SpojElementsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: SpojElements
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.SpojElement.Include(s => s.Element).Include(s => s.Spoj);
            return View(await databaseContext.ToListAsync());
        }

        // GET: SpojElements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spojElement = await _context.SpojElement
                .Include(s => s.Element)
                .Include(s => s.Spoj)
                .SingleOrDefaultAsync(m => m.SpojId == id);
            if (spojElement == null)
            {
                return NotFound();
            }

            return View(spojElement);
        }

        // GET: SpojElements/Create
        public IActionResult Create()
        {
            ViewData["ElementId"] = new SelectList(_context.Element, "Id", "Name");
            ViewData["SpojId"] = new SelectList(_context.Spoj, "Id", "Name");
            return View();
        }

        // POST: SpojElements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpojId,ElementId,Count")] SpojElement spojElement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(spojElement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ElementId"] = new SelectList(_context.Element, "Id", "Name", spojElement.ElementId);
            ViewData["SpojId"] = new SelectList(_context.Spoj, "Id", "Name", spojElement.SpojId);
            return View(spojElement);
        }

        // GET: SpojElements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spojElement = await _context.SpojElement.SingleOrDefaultAsync(m => m.SpojId == id);
            if (spojElement == null)
            {
                return NotFound();
            }
            ViewData["ElementId"] = new SelectList(_context.Element, "Id", "Name", spojElement.ElementId);
            ViewData["SpojId"] = new SelectList(_context.Spoj, "Id", "Name", spojElement.SpojId);
            return View(spojElement);
        }

        // POST: SpojElements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpojId,ElementId,Count")] SpojElement spojElement)
        {
            if (id != spojElement.SpojId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spojElement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpojElementExists(spojElement.SpojId))
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
            ViewData["ElementId"] = new SelectList(_context.Element, "Id", "Name", spojElement.ElementId);
            ViewData["SpojId"] = new SelectList(_context.Spoj, "Id", "Name", spojElement.SpojId);
            return View(spojElement);
        }

        // GET: SpojElements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spojElement = await _context.SpojElement
                .Include(s => s.Element)
                .Include(s => s.Spoj)
                .SingleOrDefaultAsync(m => m.SpojId == id);
            if (spojElement == null)
            {
                return NotFound();
            }

            return View(spojElement);
        }

        // POST: SpojElements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var spojElement = await _context.SpojElement.SingleOrDefaultAsync(m => m.SpojId == id);
            _context.SpojElement.Remove(spojElement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpojElementExists(int id)
        {
            return _context.SpojElement.Any(e => e.SpojId == id);
        }
    }
}
