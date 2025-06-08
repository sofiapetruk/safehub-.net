using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gs.Models
{
    /// <summary>
    /// Representa o estoque de itens no abrigo.
    /// </summary>
    [Table("T_SAFEHUB_ESTOQUE_ABRIGO")]
    public class EstoqueAbrigo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdEstoque { get; set; }

        [Required(ErrorMessage = "O nome do item é obrigatório")]
        [Column("nm_item")]
        [StringLength(100, ErrorMessage = "O nome do item deve ter no máximo 100 caracteres")]
        public string NomeItem { get; set; }

        [Required(ErrorMessage = "O tipo do item é obrigatório")]
        [Column("tp_item")]
        public EstoqueAbrigoEnum TipoItem { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória")]
        [Column("quantidade")]
        public float Quantidade { get; set; }

        /// <summary>
        /// Chave estrangeira para CadastroAbrigo.
        /// </summary>
        [Required]
        [Column("fk_id_abrigo")]
        public long FkIdAbrigo { get; set; }

        [ForeignKey(nameof(FkIdAbrigo))]
        [InverseProperty(nameof(CadastroAbrigo.Estoques))]
        public CadastroAbrigo ChaveAbrigo { get; set; }
    }
}
