using Projeto.Entidades.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.Web.Areas.AreaRestrita.Models.Operador
{
    public class ConsultaViewModel
    {
        public int IdOperador { get; set; }
        public string Setor { get; set; }
        public string Nome { get; set; }
        public string Ativo { get; set; }
    }
}