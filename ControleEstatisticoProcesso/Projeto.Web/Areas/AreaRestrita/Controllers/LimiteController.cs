using Projeto.DAL.Persistencia;
using Projeto.Entidades;
using Projeto.Web.Areas.AreaRestrita.Models.Limite;
using Projeto.Web.Areas.AreaRestrita.Models.Lote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.Web.Areas.AreaRestrita.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class LimiteController : Controller
    {
        // GET: AreaRestrita/Lote
        public ActionResult CadastroAmostras(int? qtdTotal, int? idMaquina)
        {
            var model = new MetodosPartial();
            model.CadastroLoteAmostra = new CadastroLoteAmostraViewModel();
            model.CadastroLoteAmostra.UsuarioAnalise = new Usuario();
            
            try
            {
                model.CadastroLoteAmostra.UsuarioAnalise = (Usuario)Session["usuario"];
                model.CadastroLoteAmostra.TipoLote = Entidades.Enuns.TipoLote.Amostra;
                model.CadastroLoteAmostra.QtdTotal = qtdTotal != null ? (int)qtdTotal : 0;
                model.CadastroLoteAmostra.IdMaquina = idMaquina != null ? (int)idMaquina : 0;

                model.ConsultaLoteAmostra = ConsultarAmostras();
            }
            catch (Exception e)
            {
                TempData["Sucesso"] = false;
                TempData["Mensagem"] = "Erro: " + e.Message;
            }

            return View(model);
        }

        private List<ConsultaLoteAmostraViewModel> ConsultarAmostras()
        {
            try
            {
                var lista = new List<ConsultaLoteAmostraViewModel>();
                foreach (var item in new LoteDAL().ConsultarAmostras())
                {
                    var m = new ConsultaLoteAmostraViewModel();
                    m.IdLote = item.IdLote;
                    m.DataHora = item.DataHora.ToString("dd/MM/yyyy HH:mm");
                    m.QtdTotal = item.QtdTotal;
                    m.QtdReprovada = item.QtdReprovada;
                    m.PercentualReprovado = Math.Round(item.PercentualReprovado * 100,2);
                    m.Comentario = item.Comentario;
                    m.UsuarioAnalise = item.UsuarioAnalise.Nome;
                    m.TipoLote = item.TipoLote;

                    lista.Add(m);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CadastroAmostras(CadastroLoteAmostraViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var l = new Lote();
                    l.UsuarioAnalise = new Usuario();
                    l.Maquina = new Maquina();

                    l.IdLote = model.IdLote;
                    l.DataHora = DateTime.Now;
                    l.QtdTotal = model.QtdTotal;
                    l.QtdReprovada = model.QtdReprovada;
                    l.PercentualReprovado = Convert.ToDecimal(l.QtdReprovada) / Convert.ToDecimal(l.QtdTotal);
                    l.Comentario = model.Comentario;
                    l.UsuarioAnalise.IdUsuario = model.UsuarioAnalise.IdUsuario;
                    l.TipoLote = model.TipoLote;
                    l.Maquina.IdMaquina = model.IdMaquina;

                    new LoteDAL().CadastrarLoteAmostra(l);

                    TempData["Sucesso"] = true;
                    TempData["Mensagem"] = $"Lote{model.IdLote} cadastrado com sucesso";

                    ModelState.Clear();
                    return CadastroAmostras(model.QtdTotal, model.IdMaquina);
                }
            }
            catch (Exception e)
            {
                TempData["Sucesso"] = false;
                TempData["Mensagem"] = $"Erro: {e.Message}";
            }
            return View(model);
        }


    }
}