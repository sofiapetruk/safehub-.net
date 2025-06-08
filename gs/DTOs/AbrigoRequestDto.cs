using System.ComponentModel.DataAnnotations;

namespace gs.DTOs
{
    public class AbrigoRequestDto
    {
        [Required(ErrorMessage = "O nome do abrigo é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome do abrigo deve ter no máximo 100 caracteres")]
        public string NomeAbrigo { get; set; }

        [Required(ErrorMessage = "A capacidade de pessoas é obrigatória")]
        [Range(1, int.MaxValue, ErrorMessage = "A capacidade deve ser ao menos 1")]
        public int CapacidadePessoa { get; set; }

        [Required(ErrorMessage = "O nome do responsável é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome do responsável deve ter no máximo 100 caracteres")]
        public string NomeResponsavel { get; set; }

        [Required(ErrorMessage = "A longitude é obrigatória")]
        [StringLength(100, ErrorMessage = "A longitude deve ter no máximo 100 caracteres")]
        public string Longitude { get; set; }

        [Required(ErrorMessage = "A latitude é obrigatória")]
        [StringLength(100, ErrorMessage = "A latitude deve ter no máximo 100 caracteres")]
        public string Latitude { get; set; }
    }
}
