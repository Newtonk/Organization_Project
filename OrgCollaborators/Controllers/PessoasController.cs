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
    public class PessoasController : ControllerBase
    {
        private readonly MyDbContext _context;

        public PessoasController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Pessoas
        [HttpGet]
        public ActionResult<IEnumerable<Pessoa>> GetPessoas()
        {
            List<Pessoa> colabs = _context.Pessoa.Include(x => x.Superior).Include(x => x.Colaborador).ToList();

            return colabs;
        }

        // GET: api/Pessoas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa>> GetPessoa(int id)
        {
            var pessoa = await _context.Pessoa.FindAsync(id);

            if (pessoa == null)
            {
                return NotFound();
            }

            return pessoa;
        }

        // PUT: api/Pessoas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPessoa(int id, RequestUser requestPessoa)
        {
            if (id != requestPessoa.IdPessoa)
            {
                return BadRequest();
            }

            Pessoa pessoa = _context.Pessoa.Include(x => x.Superior).Include(x => x.Colaborador).FirstOrDefault(x => x.IdPessoa == requestPessoa.IdPessoa);

            // If User is Colab for first time, add in Colab Table
            if (requestPessoa.isColab && pessoa.Colaborador == null)
            {
                Colaborador colab = new Colaborador();
                colab.Pessoa = pessoa;
                _context.Colaborador.Add(colab);
            }
            // If User is not Superior, but is in Superior Table, remove from it.
            else if (!requestPessoa.isColab && pessoa.Colaborador != null)
            {
                _context.Colaborador.Remove(pessoa.Colaborador);
            }

            if (requestPessoa.isSuperior && pessoa.Superior == null)
            {
                Superior superior = new Superior();
                superior.Pessoa = pessoa;
                _context.Superior.Add(superior);
            }
            // If User is not Superior, but is in Superior Table, remove from it.
            else if (!requestPessoa.isSuperior && pessoa.Superior != null)
            {
                _context.Superior.Remove(pessoa.Superior);
            }

            _context.Entry(pessoa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(pessoa);
        }

        private ResponseUser ValidateInsertPessoa(RequestUser requestPessoa, ResponseUser response)
        {
            if (_context.Pessoa.Any(x => x.Cpf == requestPessoa.Cpf))
            {
                response.Message = "Um usuário ja possui este CPF!";
                return response;
            }
            if (!requestPessoa.isColab && !requestPessoa.isSuperior)
            {
                response.Message = "O usuário deve ser um colaborador ou um superior imediato!";
                return response;
            }

            if (String.IsNullOrEmpty(response.Message))
                response.Success = true;
            return response;
        }

        // POST: api/Pessoas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pessoa>> PostPessoa(RequestUser requestPessoa)
        {
            ResponseUser response = new ResponseUser();
            try
            {
                Pessoa pessoa = new Pessoa(requestPessoa.Name, requestPessoa.Email, requestPessoa.Cpf);
                response.Pessoa = pessoa;
                response = ValidateInsertPessoa(requestPessoa, response);
                if (response.Success)
                {
                    _context.Pessoa.Add(pessoa);
                    if (requestPessoa.isColab)
                    {
                        Colaborador colab = new Colaborador();
                        colab.Pessoa = pessoa;
                        _context.Colaborador.Add(colab);
                    }
                    if (requestPessoa.isSuperior)
                    {
                        Superior superior = new Superior();
                        superior.Pessoa = pessoa;
                        _context.Superior.Add(superior);
                    }
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Não foi possível inserir este usuário!";
            }
            return Ok(response);
        }

        // DELETE: api/Pessoas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePessoa(int id)
        {
            var pessoa = await _context.Pessoa.Include(x => x.Superior).Include(x => x.Colaborador).FirstOrDefaultAsync(x => x.IdPessoa == id);

            if (pessoa == null)
            {
                return NotFound();
            }

            if (pessoa.Colaborador != null)
            {
                Colaborador colab = _context.Colaborador.FirstOrDefault(x => x.IdColaborador == pessoa.Colaborador.IdColaborador);
                _context.Colaborador.Remove(colab);
            }
            if (pessoa.Superior != null)
            {
                Superior superior = _context.Superior.FirstOrDefault(x => x.IdSuperior == pessoa.Superior.IdSuperior);
                _context.Superior.Remove(superior);
            }

            _context.Pessoa.Remove(pessoa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PessoaExists(int id)
        {
            return _context.Pessoa.Any(e => e.IdPessoa == id);
        }
    }
}
