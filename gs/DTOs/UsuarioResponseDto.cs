namespace gs.DTOs
{
    public class UsuarioResponseDto
    {
        public long IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public long ChaveAbrigo { get; set; }
    }
}
