using System.ComponentModel.DataAnnotations;

namespace TodoJWT.Dto
{
    public class UsuarioDto
    {
        [Required(ErrorMessage = "O username é obrigatório")]
        public string Username { get; set; }
        [Required(ErrorMessage ="A senha é obrigatória")]
        public string Password { get; set; }
    }
}
