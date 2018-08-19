using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kemijski_spojevi.Database;
using Kemijski_spojevi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kemijski_spojevi.Controllers
{
    public class SpojeviController : Controller
    {

        private readonly DatabaseContext _context;


        public SpojeviController(DatabaseContext context)
        {
            _context = context;
            //con = context;
            
        }

        // GET: /<controller>/
        public IActionResult Index()
        {

            return View(_context.Spoj.Include(s=>s.Type).Include(s=>s.SpojElement).ThenInclude(s=>s.Element).ToList());
        }


        public IActionResult Create()
        {
            ViewDataSet();
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,TypeId")] Spoj spoj,ICollection<int> Elements)
        {


            if (Elements.Count >=Spoj.MinSizeOfElements)
            {
                _context.Add(spoj);
                if (ModelState.IsValid)
                {
                    var list = new List<SpojElement>();
                    foreach (var elem in Elements) {

                        //until the ui supports entering count for each element set it to 1 and later edit it
                        list.Add(new SpojElement() { Count=1,ElementId=elem,SpojId=spoj.Id});

                    }


                    
                    _context.AddRange(list);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewDataSet();
            var spojVM = new SpojVM();
            return View(spojVM);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spoj = await _context.Spoj.SingleOrDefaultAsync(m => m.Id == id);
            if (spoj == null)
            {
                return NotFound();
            }
            ViewDataSet();
            var spojVM = new SpojVM() { Name=spoj.Name,TypeID=spoj.TypeId,Elements= spoj.SpojElement.Select(s => s.ElementId).ToList() };
            return View(spojVM);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TypeId")] Spoj spoj)
        {
            if (id != spoj.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spoj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpojExists(spoj.Id))
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
            ViewDataSet();
            var spojVM = new SpojVM();
            return View(spojVM);
        }

        /// <summary>
        /// Sets the data set for Spoj form
        /// </summary>
        private void ViewDataSet() {

            ViewData["TypeId"] = new SelectList(_context.VrstaSpoja, "Id", "Name");
            ViewData["Elements"] = new SelectList(_context.Element, "Id", "Name");
        }

        private bool SpojExists(int id)
        {
            return _context.Spoj.Any(e => e.Id == id);
        }
    }
}
