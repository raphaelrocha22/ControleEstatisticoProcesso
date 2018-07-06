using Projeto.DAL.Persistencia;
using Projeto.Entidades;
using Projeto.Entidades.Enuns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        [Display(Name = "Analisado por*")]
        public Usuario UsuarioAnalise { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Tipo Lote*")]
        public TipoLote TipoLote { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Máquina*")]
        public int IdMaquina { get; set; }

        [Display(Name = "Tipo Carta")]
        public string TipoCarta { get; set; }

        public List<SelectListItem> ListarMaquinas
        {
            get
            {
                var lista = new List<SelectListItem>();
                foreach (var item in new MaquinaDAL().ConsultarMaquina())
                {
                    lista.Add(new SelectListItem() { Text = item.CodInterno + " - " + item.Modelo, Value = item.IdMaquina.ToString() });
                }
                return lista;
            }
        }
    }
}