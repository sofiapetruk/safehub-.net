using gs.Data;
using gs.DTOs;
using gs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace gs.Controllers
{
    [ApiController]
    [Route("api/ocupacoes")]
    public class OcupacaoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OcupacaoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("usuario/{idUsuario}")]
        public async Task<ActionResult<IEnumerable<OcupacaoResponseDto>>> ListByUsuario(long idUsuario)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.ChaveAbrigo)
                .FirstOrDefaultAsync(u => u.IdUsuario == idUsuario);
            if (usuario == null)
                return NotFound(new { message = "Usuário não encontrado." });

            var idAbrigo = usuario.FkIdAbrigo;
            var ocupacoes = await _context.Ocupacoes
                .Where(o => o.FkIdAbrigo == idAbrigo)
                .ToListAsync();

            var response = ocupacoes.Select(o => new OcupacaoResponseDto
            {
                IdOcupacao = o.IdOcupacao,
                NumeroPessoa = o.NumeroPessoa,
                DataRegistro = o.DataRegistro,
                ChaveAbrigo = o.FkIdAbrigo
            });

            return Ok(response);
        }

        [HttpGet("{idOcupacao}")]
        public async Task<ActionResult<OcupacaoResponseDto>> GetById(long idOcupacao)
        {
            var o = await _context.Ocupacoes.FindAsync(idOcupacao);
            if (o == null)
                return NotFound();

            var response = new OcupacaoResponseDto
            {
                IdOcupacao = o.IdOcupacao,
                NumeroPessoa = o.NumeroPessoa,
                DataRegistro = o.DataRegistro,
                ChaveAbrigo = o.FkIdAbrigo
            };

            return Ok(response);
        }

        [HttpPost("usuario/{idUsuario}")]
        public async Task<ActionResult<OcupacaoResponseDto>> Create(long idUsuario, OcupacaoRequestDto request)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.ChaveAbrigo)
                .FirstOrDefaultAsync(u => u.IdUsuario == idUsuario);
            if (usuario == null)
                return NotFound(new { message = "Usuário não encontrado." });

            var abrigo = usuario.ChaveAbrigo;
            var hoje = DateTime.UtcNow.Date;

            var totalHoje = await _context.Ocupacoes
                .Where(o => o.FkIdAbrigo == abrigo.IdCadastroAbrigo && o.DataRegistro == hoje)
                .SumAsync(o => (int?)o.NumeroPessoa) ?? 0;

            if (totalHoje + request.NumeroPessoa > abrigo.CapacidadePessoa)
                return BadRequest(new { message = "Capacidade do abrigo excedida." });

            var existente = await _context.Ocupacoes
                .FirstOrDefaultAsync(o => o.FkIdAbrigo == abrigo.IdCadastroAbrigo && o.DataRegistro == hoje);

            if (existente != null)
            {
                existente.NumeroPessoa += request.NumeroPessoa;
                await _context.SaveChangesAsync();
                var resp = new OcupacaoResponseDto
                {
                    IdOcupacao = existente.IdOcupacao,
                    NumeroPessoa = existente.NumeroPessoa,
                    DataRegistro = existente.DataRegistro,
                    ChaveAbrigo = existente.FkIdAbrigo
                };
                return Ok(resp);
            }

            var nova = new AbrigoOcupacao
            {
                FkIdAbrigo = abrigo.IdCadastroAbrigo,
                NumeroPessoa = request.NumeroPessoa,
                DataRegistro = hoje
            };
            _context.Ocupacoes.Add(nova);
            await _context.SaveChangesAsync();

            var response = new OcupacaoResponseDto
            {
                IdOcupacao = nova.IdOcupacao,
                NumeroPessoa = nova.NumeroPessoa,
                DataRegistro = nova.DataRegistro,
                ChaveAbrigo = nova.FkIdAbrigo
            };
            return CreatedAtAction(nameof(GetById), new { idOcupacao = nova.IdOcupacao }, response);
        }

        [HttpPut("{idOcupacao}")]
        public async Task<ActionResult<OcupacaoResponseDto>> Update(long idOcupacao, OcupacaoRequestDto request)
        {
            var ocupacao = await _context.Ocupacoes
                .Include(o => o.ChaveAbrigo)
                .FirstOrDefaultAsync(o => o.IdOcupacao == idOcupacao);
            if (ocupacao == null)
                return NotFound();

            var abrigo = ocupacao.ChaveAbrigo;
            var hoje = ocupacao.DataRegistro;

            var totalOutros = await _context.Ocupacoes
                .Where(o => o.FkIdAbrigo == abrigo.IdCadastroAbrigo && o.DataRegistro == hoje && o.IdOcupacao != idOcupacao)
                .SumAsync(o => (int?)o.NumeroPessoa) ?? 0;

            if (totalOutros + request.NumeroPessoa > abrigo.CapacidadePessoa)
                return BadRequest(new { message = "Capacidade do abrigo excedida ao atualizar." });

            ocupacao.NumeroPessoa = request.NumeroPessoa;
            await _context.SaveChangesAsync();

            var response = new OcupacaoResponseDto
            {
                IdOcupacao = ocupacao.IdOcupacao,
                NumeroPessoa = ocupacao.NumeroPessoa,
                DataRegistro = ocupacao.DataRegistro,
                ChaveAbrigo = ocupacao.FkIdAbrigo
            };
            return Ok(response);
        }

        [HttpDelete("{idOcupacao}")]
        public async Task<IActionResult> Delete(long idOcupacao)
        {
            var ocupacao = await _context.Ocupacoes.FindAsync(idOcupacao);
            if (ocupacao == null)
                return NotFound();

            _context.Ocupacoes.Remove(ocupacao);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
