using Projeto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto.Web.Areas.AreaRestrita.Models.Limite
{
    public class ConsultaLimiteControleViewModel
    {
        public int idLimite { get; set; }

        [Display(Name = "Data Cálculo")]
        public DateTime DataCalculo { get; set; }

        public decimal LSC { get; set; }
        public decimal LC { get; set; }
        public decimal LIC { get; set; }

        [Display(Name = "Usuário Análise")]
        public string UsuarioAnalise { get; set; }

        public bool Ativo { get; set; }
    }
}