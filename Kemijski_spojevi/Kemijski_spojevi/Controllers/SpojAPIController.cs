using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kemijski_spojevi.Database;
using Kemijski_spojevi.Models;
using Microsoft.Extensions.Logging;

namespace Kemijski_spojevi.Controllers
{
    [Produces("application/json")]
    [Route("api/Spoj")]
    public class SpojAPIController : Controller
    {
        private ILogger logger;
        private readonly DatabaseContext _context;

        public SpojAPIController(DatabaseContext context, ILogger<SpojAPIController> logger)
        {
            _context = context;
            this.logger = logger;
        }
        /// <summary>
        /// Returns every entity from database table Spoj
        /// including data for type name and every element and their count from the compound.
        /// </summary>
        /// <returns>Every entity from database table Spoj and status Ok</returns>
        // GET: api/Spoj
        [HttpGet]
        public async Task<IActionResult> GetSpoj()
        {
            var elements = _context.SpojElement.Select(s => new SpojElementAPIModel() {
                SpojId=s.SpojId,Count=s.Count,Element=s.Element.Name,ElementId=s.ElementId
            }).ToList();
            var l = await _context.Spoj.Select(s => new SpojAPIModel() {
                ElementCounts=elements.Where(a=>a.SpojId==s.Id), Name = s.Name, Id = s.Id, TypeName = s.Type.Name
            }).ToListAsync();
            return Ok(l);
        }
        /*/// <summary>
        /// Gets a compound from table Spoj.
        /// </summary>
        /// <param name="id">Id of the compound.</param>
        /// <returns>Ok if the entity is found, not found or bad request otherwise.</returns>
        // GET: api/Spoj/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpoj([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                logger.LogError("spoj " + id + " nije valjan");
                return BadRequest(ModelState);
            }

            var spoj = await _context.Spoj.SingleOrDefaultAsync(m => m.Id == id);

            if (spoj == null)
            {
                logger.LogError("spoj "+ id +" nije nađen");
                return NotFound();
            }

            return Ok(spoj);
        }*/

        /// <summary>
        /// Returns every compound of type with given type id.
        /// </summary>
        /// <param name="typeId">Type id</param>
        /// <returns>Every compound of type with type id and status Ok.</returns>
        [HttpGet("{typeId}")]
        public async Task<IActionResult> GetSpojByType([FromRoute] int typeId)
        {
            if (!ModelState.IsValid)
            {
                logger.LogError("spoj nije valjan");
                return BadRequest(ModelState);
            }
            var elements = _context.SpojElement.Select(s => new SpojElementAPIModel()
            {
                SpojId = s.SpojId,
                Count = s.Count,
                Element = s.Element.Name,
                ElementId = s.ElementId
            }).ToList();
            var spoj = await _context.Spoj.Where(s=>s.TypeId==typeId).Select(s=>new SpojAPIModel {
                ElementCounts = elements.Where(a => a.SpojId == s.Id), Name = s.Name, Id = s.Id, TypeName = s.Type.Name }).ToListAsync();

            if (spoj == null)
            {
                logger.LogError("nema spoja tipa "+_context.VrstaSpoja.First(s=>s.Id==typeId).Name);
                return NotFound();
            }

            return Ok(spoj);
        }



        /// <summary>
        /// Modifies an existing compound.
        /// </summary>
        /// <param name="id">Id of the modified compound.</param>
        /// <param name="spoj">Modified compound.</param>
        /// <returns>204 if successful, badrequest if not valid or not found if it doesn't exist.</returns>
        // PUT: api/Spoj/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpoj([FromRoute] int id, [FromBody] Spoj spoj)
        {
            if (!ModelState.IsValid)
            {
                logger.LogError("spoj nije valjan");
                return BadRequest(ModelState);
            }

            if (id != spoj.Id)
            {
                logger.LogError("spoj nema dobar id"+" tražen: "+spoj.Id+" dobiven "+id);
                return BadRequest();
            }

            _context.Entry(spoj).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpojExists(id))
                {
                    logger.LogError("spoj " + id + " nije nađen");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        /// <summary>
        /// Creates a new entity for table Spoj.
        /// </summary>
        /// <param name="spoj">Chemical compund</param>
        /// <returns>Bad request if it is not valid, or status 201 if created.</returns>
        // POST: api/Spoj
        [HttpPost]
        public async Task<IActionResult> PostSpoj([FromBody] Spoj spoj)
        {
            if (!ModelState.IsValid)
            {
                logger.LogError("spoj nije valjan");
                return BadRequest(ModelState);
            }

            _context.Spoj.Add(spoj);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpoj", new { id = spoj.Id }, spoj);
        }


        /// <summary>
        /// Deletes an entity with Id equal to the parameter Id.
        /// </summary>
        /// <param name="id">Id of the entity to delete.</param>
        /// <returns>Ok if deleted, NotFound if there is no entity with that id.</returns>
        // DELETE: api/Spoj/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpoj([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var spoj = await _context.Spoj.Include(s=>s.SpojElement).SingleOrDefaultAsync(m => m.Id == id);
            if (spoj == null)
            {
                logger.LogError("spoj " + id + " nije nađen");
                return NotFound();
            }
            var deleteList=spoj.SpojElement.ToList();
            _context.SpojElement.RemoveRange(deleteList);
            await _context.SaveChangesAsync();
            _context.Spoj.Remove(spoj);
            await _context.SaveChangesAsync();

            return Ok(spoj);
        }

        private bool SpojExists(int id)
        {
            return _context.Spoj.Any(e => e.Id == id);
        }
    }
}