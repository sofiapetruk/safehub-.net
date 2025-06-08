using gs.Data;
using gs.DTOs;
using gs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace gs.Controllers
{
    [ApiController]
    [Route("api/abrigos")]
    public class AbrigoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AbrigoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AbrigoResponseDto>>> GetAll()
        {
            var abrigos = await _context.Abrigos.ToListAsync();
            var response = abrigos.Select(a => new AbrigoResponseDto
            {
                IdCadastroAbrigo = a.IdCadastroAbrigo,
                NomeAbrigo = a.NomeAbrigo,
                CapacidadePessoa = a.CapacidadePessoa,
                NomeResponsavel = a.NomeResponsavel,
                Longitude = a.Longitude,
                Latitude = a.Latitude
            }).ToList();

            return Ok(response);
        }

        [HttpGet("{idAbrigo}")]
        public async Task<ActionResult<AbrigoResponseDto>> GetById(long idAbrigo)
        {
            var abrigo = await _context.Abrigos.FindAsync(idAbrigo);
            if (abrigo == null)
                return NotFound();

            var response = new AbrigoResponseDto
            {
                IdCadastroAbrigo = abrigo.IdCadastroAbrigo,
                NomeAbrigo = abrigo.NomeAbrigo,
                CapacidadePessoa = abrigo.CapacidadePessoa,
                NomeResponsavel = abrigo.NomeResponsavel,
                Longitude = abrigo.Longitude,
                Latitude = abrigo.Latitude
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<AbrigoResponseDto>> Create(AbrigoRequestDto request)
        {
            var abrigo = new CadastroAbrigo
            {
                NomeAbrigo = request.NomeAbrigo,
                CapacidadePessoa = request.CapacidadePessoa,
                NomeResponsavel = request.NomeResponsavel,
                Longitude = request.Longitude,
                Latitude = request.Latitude
            };

            _context.Abrigos.Add(abrigo);
            await _context.SaveChangesAsync();

            var response = new AbrigoResponseDto
            {
                IdCadastroAbrigo = abrigo.IdCadastroAbrigo,
                NomeAbrigo = abrigo.NomeAbrigo,
                CapacidadePessoa = abrigo.CapacidadePessoa,
                NomeResponsavel = abrigo.NomeResponsavel,
                Longitude = abrigo.Longitude,
                Latitude = abrigo.Latitude
            };

            return CreatedAtAction(nameof(GetById), new { idAbrigo = abrigo.IdCadastroAbrigo }, response);
        }

        [HttpPut("{idAbrigo}")]
        public async Task<ActionResult<AbrigoResponseDto>> Update(long idAbrigo, AbrigoRequestDto request)
        {
            var abrigo = await _context.Abrigos.FindAsync(idAbrigo);
            if (abrigo == null)
                return NotFound();

            abrigo.NomeAbrigo = request.NomeAbrigo;
            abrigo.CapacidadePessoa = request.CapacidadePessoa;
            abrigo.NomeResponsavel = request.NomeResponsavel;
            abrigo.Longitude = request.Longitude;
            abrigo.Latitude = request.Latitude;

            await _context.SaveChangesAsync();

            var response = new AbrigoResponseDto
            {
                IdCadastroAbrigo = abrigo.IdCadastroAbrigo,
                NomeAbrigo = abrigo.NomeAbrigo,
                CapacidadePessoa = abrigo.CapacidadePessoa,
                NomeResponsavel = abrigo.NomeResponsavel,
                Longitude = abrigo.Longitude,
                Latitude = abrigo.Latitude
            };

            return Ok(response);
        }

        [HttpDelete("{idAbrigo}")]
        public async Task<IActionResult> Delete(long idAbrigo)
        {
            var abrigo = await _context.Abrigos.FindAsync(idAbrigo);
            if (abrigo == null)
                return NotFound();

            _context.Abrigos.Remove(abrigo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("paginacao")]
        public async Task<ActionResult<object>> GetPaged(int pagina = 0, int tamanho = 2)
        {
            var query = _context.Abrigos.AsQueryable();
            var total = await query.CountAsync();
            var itens = await query
                .Skip(pagina * tamanho)
                .Take(tamanho)
                .ToListAsync();

            var response = new
            {
                TotalItems = total,
                Pagina = pagina,
                Tamanho = tamanho,
                Itens = itens.Select(a => new AbrigoResponseDto
                {
                    IdCadastroAbrigo = a.IdCadastroAbrigo,
                    NomeAbrigo = a.NomeAbrigo,
                    CapacidadePessoa = a.CapacidadePessoa,
                    NomeResponsavel = a.NomeResponsavel,
                    Longitude = a.Longitude,
                    Latitude = a.Latitude
                })
            };

            return Ok(response);
        }
    }
}
