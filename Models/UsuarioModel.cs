using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpFinance.Models
{
    [Table("Usuario")]
    public class UsuarioModel
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }
        [Column("LOGIN")]
        public string LOGIN { get; set; }
        [Column("NOME")]
        public string NOME { get; set; }
        [Column("EMAIL")]
        public string EMAIL { get; set; }
        [Column("TELEFONE")]
        public string TELEFONE { get; set; }
        [Column("SENHA")]
        public string? SENHA { get; set; }
        [Column("CRIADO_EM")]
        public DateTime CRIADO_EM { get; set; } = DateTime.Now;
    }


    [Table("UsuarioDadosComplementares")]
    public class UsuarioDadosComplementares()
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }
        [Column("ID_USUARIO")]
        public int ID_USUARIO { get; set; }
        [Column("SALARIO")]
        public double SALARIO { get; set; }
        [ForeignKey("ID_USUARIO")]
        public UsuarioModel Usuario { get; set; }
    }
}
