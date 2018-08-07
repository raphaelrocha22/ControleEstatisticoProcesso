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
        public LimiteControle LimiteControle { get; set; }

        public Status Status { get; set; }

        [Display(Name = "Usuário Aprovação")]
        public string UsuarioAprovacao { get; set; }
                
        public List<ConsultaLoteProducaoViewModel> Lotes { get; set; }
        
        [Display(Name = "Data Inicio*")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public DateTime DataInicio { get; set; }

        [Display(Name = "Data Fim*")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public DateTime DataFim { get; set; }
    }
}