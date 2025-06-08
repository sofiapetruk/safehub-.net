using System.ComponentModel.DataAnnotations;

namespace gs.DTOs
{
    public class UsuarioRequestDto : IValidatableObject
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        [StringLength(100, ErrorMessage = "O email deve ter no máximo 100 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "A senha deve ter entre 8 e 15 caracteres")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O abrigo é obrigatório")]
        public long ChaveAbrigo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Exemplo: injetar IAbrigoService para verificar se ChaveAbrigo existe
            // var service = (IAbrigoService)validationContext.GetService(typeof(IAbrigoService));
            // if (!service.ExisteAbrigo(ChaveAbrigo))
            //     yield return new ValidationResult("Abrigo informado não existe", new[] { nameof(ChaveAbrigo) });
            yield break;
        }
    }
}
