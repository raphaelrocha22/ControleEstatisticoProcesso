using Projeto.Entidades.Enuns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto.Web.Areas.AreaRestrita.Models.Lote
{
    public class ConsultaLoteAmostraViewModel
    {
        [Display(Name = "Lote")]
        public int IdLote { get; set; }

        [Display(Name = "Data Análise")]
        public string DataHora { get; set; }

        [Display(Name = "Qtd Total")]
        public int QtdTotal { get; set; }

        [Display(Name = "Qtd Reprov.")]
        public int QtdReprovada { get; set; }

        [Display(Name = "% Reprov.")]
        public decimal PercentualReprovado { get; set; }
        public string Comentario { get; set; }

        [Display(Name = "Usuário Análise")]
        public string UsuarioAnalise { get; set; }

        [Display(Name = "Tipo Lote")]
        public TipoLote TipoLote { get; set; }
    }
}