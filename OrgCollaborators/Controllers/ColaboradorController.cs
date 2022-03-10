using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OrgCollaborators.Entities;
using OrgCollaborators.Models;

namespace OrgCollaborators.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColaboradorController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ColaboradorController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Colaborador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Superior_Colaborador>>> GetSuperior_Colaborador()
        {
            return await _context.Superior_Colaborador.ToListAsync();
        }

        // GET: api/Colaborador/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Superior_Colaborador>> GetSuperior_Colaborador(int id)
        {
            var superior_Colaborador = await _context.Superior_Colaborador.FindAsync(id);

            if (superior_Colaborador == null)
            {
                return NotFound();
            }

            return superior_Colaborador;
        }

        // PUT: api/Colaborador/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSuperior_Colaborador(int id, Superior_Colaborador superior_Colaborador)
        {
            if (id != superior_Colaborador.IdSuperior_Colaborador)
            {
                return BadRequest();
            }

            _context.Entry(superior_Colaborador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Superior_ColaboradorExists(id))
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

        // POST: api/Colaborador
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Superior_Colaborador>> PostSuperior_Colaborador(Superior_Colaborador superior_Colaborador)
        {
            try
            {
                ColabController controllerColab = new ColabController(_context);
                SuperiorController controllerSup = new SuperiorController(_context);
                if (superior_Colaborador.IdColaborador > 0
                    && superior_Colaborador.IdSuperior > 0
                    && superior_Colaborador.IdSuperior != superior_Colaborador.IdColaborador)
                {
                    //// Process Colaborador
                    //Pessoa personColab = _context.Pessoa.Find(superior_Colaborador.IdColaborador);
                    //var serializedColab = JsonConvert.SerializeObject(personColab);
                    //Colaborador colab = JsonConvert.DeserializeObject<Colaborador>(serializedColab);

                    //// Process Superior
                    //Pessoa personSuperior = _context.Pessoa.Find(superior_Colaborador.IdSuperior);
                    //var serializedSuperior = JsonConvert.SerializeObject(personSuperior);
                    //Superior superior = JsonConvert.DeserializeObject<Superior>(serializedSuperior);

                    _context.Superior_Colaborador.Add(superior_Colaborador);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {

            }

            return CreatedAtAction("GetSuperior_Colaborador", new { id = superior_Colaborador.IdSuperior_Colaborador }, superior_Colaborador);
        }

        // DELETE: api/Colaborador/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuperior_Colaborador(int id)
        {
            var superior_Colaborador = await _context.Superior_Colaborador.FindAsync(id);
            if (superior_Colaborador == null)
            {
                return NotFound();
            }

            _context.Superior_Colaborador.Remove(superior_Colaborador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Superior_ColaboradorExists(int id)
        {
            return _context.Superior_Colaborador.Any(e => e.IdSuperior_Colaborador == id);
        }
    }
}
