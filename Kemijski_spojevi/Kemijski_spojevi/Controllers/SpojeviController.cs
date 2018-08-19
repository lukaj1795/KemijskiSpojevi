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
            ViewData["TypeId"] = new SelectList(_context.VrstaSpoja, "Id", "Name");
            return View();
        }


        public IActionResult Create()
        {
            ViewDataSet();
            return View();
        }


       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id,[Bind("Id,Name,TypeId")] Spoj spoj,ICollection<int> Elements)
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

            var spoj = await _context.Spoj.Include(s=>s.SpojElement).ThenInclude(s=>s.Element).SingleOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TypeId")] Spoj spoj, ICollection<int> Elements)
        {
            if (id != spoj.Id)
            {
                return NotFound();
            }
            if (Elements.Count >= Spoj.MinSizeOfElements)
            {
                if (ModelState.IsValid)
                {
                    try
                    {

                        var list = new List<SpojElement>();
                        //Add elements to compound
                        foreach (var elem in Elements)
                        {
                            if (!SpojElementExists(spoj.Id, elem))
                            {
                                //until the ui supports entering count for each element set it to 1 and later edit it
                                list.Add(new SpojElement() { Count = 1, ElementId = elem, SpojId = spoj.Id });
                            }

                        }
                        //Remove elements from compound
                        foreach (var elem2 in _context.SpojElement.Where(s=>s.SpojId==spoj.Id).Select(s => s.Element).ToList())
                        {
                            if (!Elements.Contains(elem2.Id))
                            {
                                var spojElementToDelete = _context.SpojElement.FirstOrDefault(s => s.SpojId == spoj.Id && s.ElementId == elem2.Id);
                                _context.Remove(spojElementToDelete);
                            }
                        }

                        _context.AddRange(list);
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

        private bool SpojElementExists(int spojId,int elementId)
        {
            return _context.SpojElement.Include(s => s.Spoj).Include(s => s.Element).Any(m => m.ElementId == elementId & m.SpojId == spojId);
        }

        public IActionResult EditCount(int? spojId, int? elementId)
        {
            ViewData["Elements"] = new SelectList(_context.Element, "Id", "Name");
            var spojElement = _context.SpojElement.Include(s => s.Spoj).Include(s => s.Element).SingleOrDefault(m => m.ElementId == elementId & m.SpojId == spojId);
            return View(spojElement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCount(int spojId, int elementId, [Bind("SpojId,ElementId,Count")] SpojElement spojElement)
        {
            ViewData["Elements"] = new SelectList(_context.Element, "Id", "Name");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spojElement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpojElementExists(spojId,elementId))
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


            return View(spojElement);
        }



    }
}
