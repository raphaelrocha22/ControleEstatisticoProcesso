using System.ComponentModel.DataAnnotations;

namespace Projeto.Web.Models.Home
{
    public class LoginViewModel
    {
        [RegularExpression("^[a-zA-Z0-9]{4,50}$", ErrorMessage = "Login inválido")]
        [Required(ErrorMessage = "Por favor, informe o Login do usuário.")]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [RegularExpression("^[a-zA-Z0-9@&]{4,50}$", ErrorMessage = "Senha inválida")]
        [Required(ErrorMessage = "Por favor, informe a Senha.")]
        [Display(Name = "Senha")]
        public string Senha { get; set; }
    }
}