﻿using Projeto.Web.Areas.AreaRestrita.Models.Lote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.Web.Areas.AreaRestrita.Models.Limite
{
    public class MetodosPartial
    {
        public CadastroLoteAmostraViewModel CadastroLoteAmostra { get; set; }
        public List<ConsultaLoteAmostraViewModel> ConsultaLoteAmostra { get; set; }
    }
}