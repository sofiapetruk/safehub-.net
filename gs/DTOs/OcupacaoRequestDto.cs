using System.ComponentModel.DataAnnotations;

namespace gs.DTOs
{
    public class OcupacaoRequestDto : IValidatableObject
    {
        [Required(ErrorMessage = "O número de pessoas é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O número de pessoas deve ser pelo menos 1")]
        public int NumeroPessoa { get; set; }

        /// <summary>
        /// Implementação de validação adicional para conferir capacidade máxima do abrigo.
        /// </summary>
        /// <param name="validationContext"></param>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Exemplo: recuperar serviço de abrigo via DI para verificar capacidade
            // var service = (IAbrigoService)validationContext.GetService(typeof(IAbrigoService));
            // if (!service.PodeOcupar(abrigoId, NumeroPessoa))
            //     yield return new ValidationResult("Capacidade máxima excedida", new[] { nameof(NumeroPessoa) });
            yield break;
        }
    }
}
