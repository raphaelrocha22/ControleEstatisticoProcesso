using Projeto.Entidades;
using Projeto.Entidades.Enuns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto.Web.Areas.AreaRestrita.Models.Lote
{
    public class ConsultaLoteProducaoViewModel:ConsultaLoteAmostraViewModel
    {
        public Status Status { get; set; }

        [Display(Name = "Usuário Aprovação")]
        public string UsuarioAprovacao { get; set; }

        public LimiteControle LimiteControle { get; set; }
    }
}