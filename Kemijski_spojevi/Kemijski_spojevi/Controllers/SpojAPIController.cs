﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kemijski_spojevi.Database;

namespace Kemijski_spojevi.Controllers
{
    [Produces("application/json")]
    [Route("api/Spoj")]
    public class SpojAPIController : Controller
    {
        private readonly DatabaseContext _context;

        public SpojAPIController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Spojs
        [HttpGet]
        public IEnumerable<Spoj> GetSpoj()
        {
            return _context.Spoj;
        }

        // GET: api/Spojs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpoj([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var spoj = await _context.Spoj.SingleOrDefaultAsync(m => m.Id == id);

            if (spoj == null)
            {
                return NotFound();
            }

            return Ok(spoj);
        }

        // PUT: api/Spojs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpoj([FromRoute] int id, [FromBody] Spoj spoj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != spoj.Id)
            {
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
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Spojs
        [HttpPost]
        public async Task<IActionResult> PostSpoj([FromBody] Spoj spoj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Spoj.Add(spoj);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpoj", new { id = spoj.Id }, spoj);
        }

        // DELETE: api/Spojs/5
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