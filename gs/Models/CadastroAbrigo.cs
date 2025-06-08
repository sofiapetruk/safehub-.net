using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace gs.Models
{
    [Table("T_SAFEHUB_CADASTRO_ABRIGO")]
    public class CadastroAbrigo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdCadastroAbrigo { get; set; }

        [Required(ErrorMessage = "O nome do abrigo é obrigatório")]
        [Column("nome_abrigo")]
        [StringLength(100, ErrorMessage = "O nome do abrigo deve ter no máximo 100 caracteres")]
        public string NomeAbrigo { get; set; }

        [Required(ErrorMessage = "A capacidade de pessoas é obrigatória")]
        [Column("capacidade_pessoa")]
        public int CapacidadePessoa { get; set; }

        [Required(ErrorMessage = "O nome do responsável é obrigatório")]
        [Column("nm_responsavel")]
        [StringLength(100, ErrorMessage = "O nome do responsável deve ter no máximo 100 caracteres")]
        public string NomeResponsavel { get; set; }

        [Required(ErrorMessage = "A longitude é obrigatória")]
        [Column("longitude")]
        [StringLength(100, ErrorMessage = "A longitude deve ter no máximo 100 caracteres")]
        public string Longitude { get; set; }

        [Required(ErrorMessage = "A latitude é obrigatória")]
        [Column("latitude")]
        [StringLength(100, ErrorMessage = "A latitude deve ter no máximo 100 caracteres")]
        public string Latitude { get; set; }

        [InverseProperty(nameof(CadastroUsuario.ChaveAbrigo))]
        public ICollection<CadastroUsuario> Usuarios { get; set; } = new List<CadastroUsuario>();

        [InverseProperty(nameof(AbrigoOcupacao.ChaveAbrigo))]
        public ICollection<AbrigoOcupacao> Ocupacoes { get; set; } = new List<AbrigoOcupacao>();

        [InverseProperty(nameof(EstoqueAbrigo.ChaveAbrigo))]
        public ICollection<EstoqueAbrigo> Estoques { get; set; } = new List<EstoqueAbrigo>();


        public CadastroAbrigo() { }

        public CadastroAbrigo(long idCadastroAbrigo, string nomeAbrigo, int capacidadePessoa, string nomeResponsavel, string longitude, string latitude)
        {
            IdCadastroAbrigo = idCadastroAbrigo;
            NomeAbrigo = nomeAbrigo;
            CapacidadePessoa = capacidadePessoa;
            NomeResponsavel = nomeResponsavel;
            Longitude = longitude;
            Latitude = latitude;
            Usuarios = new List<CadastroUsuario>();
        }
    }
}
