namespace HelpFinance.Models.DTOs.Usuario
{
    public class UsuarioDTO
    {
        public int ID { get; set; }
        public string NOME { get; set; }
        public string LOGIN { get; set; }
        public string EMAIL { get; set; }
        public string TELEFONE { get; set; }
        public string CRIADO_EM { get; set; }
    }

    public class RegisterUsuario
    {
        public string NOME { get; set; }
        public string LOGIN { get; set; }
        public string PASSWORD { get; set; }
        public string EMAIL { get; set; }
        public string TELEFONE { get; set; }
    }

}
