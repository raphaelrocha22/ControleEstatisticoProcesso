using Projeto.Entidades.Enuns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto.Web.Areas.AreaRestrita.Models.Operador
{
    public class EdicaoViewModel
    {
        public int IdOperador { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Selecione o Setor")]
        public Setor Setor { get; set; }

        [RegularExpression("^[a-zA-Zà-üÀ-Ü\\s]{4,50}$", ErrorMessage = "Nome inválido, apenas letras de 4 a 50 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }

        public bool Ativo { get; set; }
    }
}