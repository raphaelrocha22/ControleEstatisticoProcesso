using Projeto.Entidades.Enuns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto.Web.Areas.AreaRestrita.Models.Users
{
    public class CadastroViewModel
    {
        [RegularExpression("^[a-zA-Zà-üÀ-Ü\\s]{4,50}$", ErrorMessage = "Nome inválido, apenas letras de 4 a 50 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }

        [RegularExpression("^[a-zA-Z0-9]{4,50}$", ErrorMessage = "Login inválido, apenas letras (sem acentos) e números de 4 a 50 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Login { get; set; }

        [RegularExpression("^[a-zA-Z0-9@&]{4,50}$", ErrorMessage = "Senha inválida, apenas letras e números de 4 a 50 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "Senhas não conferem")]
        [Required(ErrorMessage = "Por favor, confirme sua senha")]
        [Display(Name = "Confirmar a Senha")]
        public string SenhaConfirm { get; set; }
    
        [Range(1, int.MaxValue, ErrorMessage = "Selecione o Perfil")]
        public PerfilUsuario Perfil { get; set; }

        public Setor Setor { get; set; }
    }
}