using Projeto.Entidades;
using Projeto.Web.Areas.AreaRestrita.Models.Lote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.Web.Areas.AreaRestrita.Models.Limite
{
    public class LimiteControleViewModel
    {
        public CadastroLoteAmostraViewModel CadastroLoteAmostra { get; set; }
        public List<ConsultaLoteAmostraViewModel> ConsultaLoteAmostra { get; set; }


        public LimiteControle LimiteControle { get; set; }
    }
}