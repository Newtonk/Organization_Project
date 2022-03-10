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
    public class SetoresController : ControllerBase
    {
        private readonly MyDbContext _context;

        public SetoresController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Setores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Setor>>> GetSetor()
        {
            return await _context.Setor.ToListAsync();
        }

        // GET: api/Setores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Setor>> GetSetor(int id)
        {
            var setor = await _context.Setor.FindAsync(id);

            if (setor == null)
            {
                return NotFound();
            }

            return setor;
        }

        // PUT: api/Setores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSetor(int id, Setor setor)
        {
            if (id != setor.IdSetor)
            {
                return BadRequest();
            }

            _context.Entry(setor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetorExists(id))
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

        // POST: api/Setores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Setor>> PostSetor(Setor setor)
        {
            _context.Setor.Add(setor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSetor", new { id = setor.IdSetor }, setor);
        }

        // DELETE: api/Setores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSetor(int id)
        {
            var setor = await _context.Setor.FindAsync(id);
            if (setor == null)
            {
                return NotFound();
            }

            _context.Setor.Remove(setor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SetorExists(int id)
        {
            return _context.Setor.Any(e => e.IdSetor == id);
        }
    }
}
