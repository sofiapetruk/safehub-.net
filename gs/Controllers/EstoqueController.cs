using gs.Data;
using gs.DTOs;
using gs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gs.Controllers
{
    [ApiController]
    [Route("api/usuarios/{idUsuario}/estoque")]
    public class EstoqueController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EstoqueController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstoqueAbrigoResponseDto>>> ListAll(long idUsuario)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.ChaveAbrigo)
                .FirstOrDefaultAsync(u => u.IdUsuario == idUsuario);
            if (usuario == null)
                return NotFound(new { message = "Usuário não encontrado." });

            var idAbrigo = usuario.FkIdAbrigo;
            var estoques = await _context.Estoques
                .Where(e => e.FkIdAbrigo == idAbrigo)
                .ToListAsync();

            var response = estoques.Select(e => new EstoqueAbrigoResponseDto
            {
                IdEstoque = e.IdEstoque,
                NomeItem = e.NomeItem,
                TipoItem = e.TipoItem.ToString(),
                Quantidade = e.Quantidade,
                ChaveAbrigo = e.FkIdAbrigo
            });

            return Ok(response);
        }

        // GET: api/usuarios/{idUsuario}/estoque/{idEstoque}
        [HttpGet("{idEstoque}")]
        public async Task<ActionResult<EstoqueAbrigoResponseDto>> GetById(long idUsuario, long idEstoque)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.ChaveAbrigo)
                .FirstOrDefaultAsync(u => u.IdUsuario == idUsuario);
            if (usuario == null)
                return NotFound(new { message = "Usuário não encontrado." });

            var estoque = await _context.Estoques
                .FirstOrDefaultAsync(e => e.IdEstoque == idEstoque && e.FkIdAbrigo == usuario.FkIdAbrigo);
            if (estoque == null)
                return NotFound();

            var response = new EstoqueAbrigoResponseDto
            {
                IdEstoque = estoque.IdEstoque,
                NomeItem = estoque.NomeItem,
                TipoItem = estoque.TipoItem.ToString(),
                Quantidade = estoque.Quantidade,
                ChaveAbrigo = estoque.FkIdAbrigo
            };

            return Ok(response);
        }

        // POST: api/usuarios/{idUsuario}/estoque
        [HttpPost]
        public async Task<ActionResult<EstoqueAbrigoResponseDto>> Create(
            long idUsuario,
            EstoqueAbrigoRequestDto request)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.ChaveAbrigo)
                .FirstOrDefaultAsync(u => u.IdUsuario == idUsuario);
            if (usuario == null)
                return NotFound(new { message = "Usuário não encontrado." });

            if (!Enum.TryParse<EstoqueAbrigoEnum>(request.TipoItem, true, out var tipo))
                return BadRequest(new { message = $"Tipo inválido. Valores válidos: {string.Join(", ", Enum.GetNames(typeof(EstoqueAbrigoEnum)))}" });

            var novo = new EstoqueAbrigo
            {
                NomeItem = request.NomeItem,
                TipoItem = tipo,
                Quantidade = request.Quantidade,
                FkIdAbrigo = usuario.FkIdAbrigo
            };

            _context.Estoques.Add(novo);
            await _context.SaveChangesAsync();

            var response = new EstoqueAbrigoResponseDto
            {
                IdEstoque = novo.IdEstoque,
                NomeItem = novo.NomeItem,
                TipoItem = novo.TipoItem.ToString(),
                Quantidade = novo.Quantidade,
                ChaveAbrigo = novo.FkIdAbrigo
            };

            return CreatedAtAction(nameof(GetById), new { idUsuario, idEstoque = novo.IdEstoque }, response);
        }

        // PUT: api/usuarios/{idUsuario}/estoque/{idEstoque}
        [HttpPut("{idEstoque}")]
        public async Task<ActionResult<EstoqueAbrigoResponseDto>> Update(long idUsuario, long idEstoque, EstoqueAbrigoRequestDto request)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.ChaveAbrigo)
                .FirstOrDefaultAsync(u => u.IdUsuario == idUsuario);
            if (usuario == null)
                return NotFound(new { message = "Usuário não encontrado." });

            var estoque = await _context.Estoques
                .FirstOrDefaultAsync(e => e.IdEstoque == idEstoque && e.FkIdAbrigo == usuario.FkIdAbrigo);
            if (estoque == null)
                return NotFound();

            if (!Enum.TryParse<EstoqueAbrigoEnum>(request.TipoItem, true, out var tipoParsed))
                return BadRequest(new { message = $"Tipo inválido. Valores válidos: {string.Join(", ", Enum.GetNames(typeof(EstoqueAbrigoEnum)))}" });

            estoque.NomeItem = request.NomeItem;
            estoque.TipoItem = tipoParsed;
            estoque.Quantidade = request.Quantidade;
            await _context.SaveChangesAsync();

            var updated = new EstoqueAbrigoResponseDto
            {
                IdEstoque = estoque.IdEstoque,
                NomeItem = estoque.NomeItem,
                TipoItem = estoque.TipoItem.ToString(),
                Quantidade = estoque.Quantidade,
                ChaveAbrigo = estoque.FkIdAbrigo
            };
            return Ok(updated);
        }

        // DELETE: api/usuarios/{idUsuario}/estoque/{idEstoque}
        [HttpDelete("{idEstoque}")]
        public async Task<IActionResult> Delete(long idUsuario, long idEstoque)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.ChaveAbrigo)
                .FirstOrDefaultAsync(u => u.IdUsuario == idUsuario);
            if (usuario == null)
                return NotFound(new { message = "Usuário não encontrado." });

            var estoque = await _context.Estoques
                .FirstOrDefaultAsync(e => e.IdEstoque == idEstoque && e.FkIdAbrigo == usuario.FkIdAbrigo);
            if (estoque == null)
                return NotFound();

            _context.Estoques.Remove(estoque);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
