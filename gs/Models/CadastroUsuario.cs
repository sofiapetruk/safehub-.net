using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace gs.Models
{
    [Table("T_SAFEHUB_CADASTRO_USUARIO")]
    public class CadastroUsuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdUsuario { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [Column("nome")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        [Column("email")]
        [StringLength(100, ErrorMessage = "O email deve ter no máximo 100 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [Column("senha")]
        [StringLength(15, ErrorMessage = "A senha deve ter no máximo 15 caracteres")]
        public string Senha { get; set; }

        [Required]
        [Column("fk_id_abrigo")]
        public long FkIdAbrigo { get; set; }

        [ForeignKey(nameof(FkIdAbrigo))]
        [InverseProperty(nameof(CadastroAbrigo.Usuarios))]
        public CadastroAbrigo ChaveAbrigo { get; set; }

        public CadastroUsuario() { }

        public CadastroUsuario(long idUsuario, string nome, string email, string senha, long fkIdAbrigo)
        {
            IdUsuario = idUsuario;
            Nome = nome;
            Email = email;
            Senha = senha;
            FkIdAbrigo = fkIdAbrigo;
        }
    }
}
