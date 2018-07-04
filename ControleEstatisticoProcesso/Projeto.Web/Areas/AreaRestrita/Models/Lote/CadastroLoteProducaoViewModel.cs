using Projeto.Entidades;
using Projeto.Entidades.Enuns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto.Web.Areas.AreaRestrita.Models.Lote
{
    public class CadastroLoteProducaoViewModel:CadastroLoteAmostraViewModel
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public Usuario UsuarioAprovacao { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Campo obrigatório")]
        public Status Status { get; set; }

        public LimiteControle LimiteControle { get; set; }
    }
}