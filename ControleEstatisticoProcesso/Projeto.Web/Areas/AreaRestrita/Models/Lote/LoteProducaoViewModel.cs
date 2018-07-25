using Projeto.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.Web.Areas.AreaRestrita.Models.Lote
{
    public class LoteProducaoViewModel
    {
        public CadastroLoteProducaoViewModel CadastroLoteProducao { get; set; }
        public List<ConsultaLoteProducaoViewModel> ConsultaLoteProducao { get; set; }
        
        public LimiteControle LimiteControle { get; set; }
    }
}