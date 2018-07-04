using Projeto.Entidades;
using Projeto.Entidades.Enuns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto.Web.Areas.AreaRestrita.Models.Lote
{
    public class CadastroLoteAmostraViewModel
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Lote*")]
        public int IdLote { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Qtd Total*")]
        public int QtdTotal { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Qtd Reprov.*")]
        public int QtdReprovada { get; set; }

        [MaxLength(250, ErrorMessage = "Máximo {1} Caracteres")]
        [Display(Name = "Comentario")]
        public string Comentario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public Usuario UsuarioAnalise { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Campo obrigatório")]
        public TipoLote TipoLote { get; set; }
    }
}