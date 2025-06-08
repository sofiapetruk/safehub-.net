using gs.Data;
using gs.DTOs;
using gs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gs.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioResponseDto>>> GetAll()
        {
            var usuarios = await _context.Usuarios
                .Include(u => u.ChaveAbrigo)
                .ToListAsync();

            var response = usuarios.Select(u => new UsuarioResponseDto
            {
                IdUsuario = u.IdUsuario,
                Nome = u.Nome,
                Email = u.Email,
                Senha = u.Senha,
                ChaveAbrigo = u.FkIdAbrigo
            }).ToList();

            return Ok(response);
        }

        [HttpGet("{idUsuario}")]
        public async Task<ActionResult<UsuarioResponseDto>> GetById(long idUsuario)
        {
            var u = await _context.Usuarios
                .Include(x => x.ChaveAbrigo)
                .FirstOrDefaultAsync(x => x.IdUsuario == idUsuario);

            if (u == null)
                return NotFound();

            var response = new UsuarioResponseDto
            {
                IdUsuario = u.IdUsuario,
                Nome = u.Nome,
                Email = u.Email,
                Senha = u.Senha,
                ChaveAbrigo = u.FkIdAbrigo
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioResponseDto>> Create(UsuarioRequestDto request)
        {

            var abrigo = await _context.Abrigos.FindAsync(request.ChaveAbrigo);
            if (abrigo == null)
                return NotFound(new { message = "Abrigo não encontrado." });

            var usuario = new CadastroUsuario
            {
                Nome = request.Nome,
                Email = request.Email,
                Senha = request.Senha,
                FkIdAbrigo = request.ChaveAbrigo
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var response = new UsuarioResponseDto
            {
                IdUsuario = usuario.IdUsuario,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Senha = usuario.Senha,
                ChaveAbrigo = usuario.FkIdAbrigo
            };

            return CreatedAtAction(nameof(GetById), new { idUsuario = usuario.IdUsuario }, response);
        }

        [HttpPut("{idUsuario}")]
        public async Task<ActionResult<UsuarioResponseDto>> Update(long idUsuario, UsuarioRequestDto request)
        {
            var existing = await _context.Usuarios.FindAsync(idUsuario);
            if (existing == null)
                return NotFound();

            var abrigo = await _context.Abrigos.FindAsync(request.ChaveAbrigo);
            if (abrigo == null)
                return NotFound(new { message = "Abrigo não encontrado." });

            existing.Nome = request.Nome;
            existing.Email = request.Email;
            existing.Senha = request.Senha;
            existing.FkIdAbrigo = request.ChaveAbrigo;

            await _context.SaveChangesAsync();

            var response = new UsuarioResponseDto
            {
                IdUsuario = existing.IdUsuario,
                Nome = existing.Nome,
                Email = existing.Email,
                Senha = existing.Senha,
                ChaveAbrigo = existing.FkIdAbrigo
            };

            return Ok(response);
        }

        [HttpDelete("{idUsuario}")]
        public async Task<IActionResult> Delete(long idUsuario)
        {
            var existing = await _context.Usuarios.FindAsync(idUsuario);
            if (existing == null)
                return NotFound();

            _context.Usuarios.Remove(existing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("paginacao")]
        public async Task<ActionResult<object>> GetPaged(int pagina = 0, int tamanho = 5)
        {
            var query = _context.Usuarios.AsQueryable();
            var total = await query.CountAsync();
            var items = await query
                .Skip(pagina * tamanho)
                .Take(tamanho)
                .ToListAsync();

            var list = items.Select(u => new UsuarioResponseDto
            {
                IdUsuario = u.IdUsuario,
                Nome = u.Nome,
                Email = u.Email,
                Senha = u.Senha,
                ChaveAbrigo = u.FkIdAbrigo
            });

            var result = new
            {
                TotalItems = total,
                Pagina = pagina,
                Tamanho = tamanho,
                Itens = list
            };

            return Ok(result);
        }
    }
}
