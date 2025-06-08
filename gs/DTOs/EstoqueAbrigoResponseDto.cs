using gs.Models;

namespace gs.DTOs
{
    public class EstoqueAbrigoResponseDto
    {
        public long IdEstoque { get; set; }
        public string NomeItem { get; set; }
        public string TipoItem { get; set; }
        public float Quantidade { get; set; }
        public long ChaveAbrigo { get; set; }
    }
}
