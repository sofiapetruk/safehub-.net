
using System.ComponentModel.DataAnnotations;

namespace gs.DTOs
{
    public class EstoqueAbrigoRequestDto
    {
        [Required(ErrorMessage = "O nome do item é obrigatório")]
        [StringLength(100)]
        public string NomeItem { get; set; } = null!;

        [Required(ErrorMessage = "O tipo do item é obrigatório")]
        public string TipoItem { get; set; } = null!;

        [Required(ErrorMessage = "A quantidade é obrigatória")]
        [Range(0, float.MaxValue)]
        public float Quantidade { get; set; }
    }
}
