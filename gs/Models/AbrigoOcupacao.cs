using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace gs.Models
{
    [Table("T_SAFEHUB_ABRIGO_OCUPACAO")]
    public class AbrigoOcupacao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdOcupacao { get; set; }

        [Required(ErrorMessage = "O número de pessoas é obrigatório")]
        [Column("nr_pessoa")]
        public int NumeroPessoa { get; set; }

        [Required(ErrorMessage = "A data de registro é obrigatória")]
        [Column("dt_registro")]
        public DateTime DataRegistro { get; set; }

        [Required]
        [Column("fk_id_abrigo")]
        public long FkIdAbrigo { get; set; }

        [ForeignKey(nameof(FkIdAbrigo))]
        [InverseProperty(nameof(CadastroAbrigo.Ocupacoes))]
        public CadastroAbrigo ChaveAbrigo { get; set; }
    }
}
