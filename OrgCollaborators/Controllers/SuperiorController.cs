using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrgCollaborators.Entities;
using OrgCollaborators.Models;

namespace OrgCollaborators.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperiorController : ControllerBase
    {
        private readonly MyDbContext _context;

        public SuperiorController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Superior
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Superior>>> GetSuperior()
        {
            return await _context.Superior.ToListAsync();
        }

        // GET: api/Superior/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Superior>> GetSuperior(int id)
        {
            var superior = await _context.Superior.FindAsync(id);

            if (superior == null)
            {
                return NotFound();
            }

            return superior;
        }

        // PUT: api/Superior/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSuperior(int id, Superior superior)
        {
            if (id != superior.IdPessoa)
            {
                return BadRequest();
            }

            _context.Entry(superior).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuperiorExists(id))
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

        // POST: api/Superior
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Superior>> PostSuperior(Superior superior)
        {
            _context.Superior.Add(superior);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSuperior", new { id = superior.IdPessoa }, superior);
        }

        // DELETE: api/Superior/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuperior(int id)
        {
            var superior = await _context.Superior.FindAsync(id);
            if (superior == null)
            {
                return NotFound();
            }

            _context.Superior.Remove(superior);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SuperiorExists(int id)
        {
            return _context.Superior.Any(e => e.IdPessoa == id);
        }
    }
}
