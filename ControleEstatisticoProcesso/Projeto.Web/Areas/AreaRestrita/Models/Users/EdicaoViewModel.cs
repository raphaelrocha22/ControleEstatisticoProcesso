using Projeto.Entidades.Enuns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto.Web.Areas.AreaRestrita.Models.Users
{
    public class EdicaoViewModel
    {
        public int IdUsuario { get; set; }

        [RegularExpression("^[a-zA-Zà-üÀ-Ü\\s]{4,50}$", ErrorMessage = "Nome inválido, apenas letras de 4 a 50 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }

        [RegularExpression("^[a-zA-Z0-9]{4,50}$", ErrorMessage = "Login inválido, apenas letras (sem acentos) e números de 4 a 50 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Login { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Selecione o Perfil")]
        public PerfilUsuario Perfil { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Selecione o Setor")]
        public Setor Setor { get; set; }

        [Required(ErrorMessage = "Selecione como ativo ou inativo")]
        public bool Ativo { get; set; }
    }
}