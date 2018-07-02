using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto.Web.Areas.AreaRestrita.Models.Users
{
    public class AlterarSenhaViewModel
    {
        public int IdUsuario { get; set; }

        [RegularExpression("^[a-zA-Z0-9@&]{4,50}$", ErrorMessage = "Senha inválida, apenas letras e números de 4 a 50 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Nova senha")]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "Senhas não conferem")]
        [Required(ErrorMessage = "Por favor, confirme sua senha")]
        [Display(Name = "Confirmar a nova senha")]
        public string SenhaConfirm { get; set; }
    }
}