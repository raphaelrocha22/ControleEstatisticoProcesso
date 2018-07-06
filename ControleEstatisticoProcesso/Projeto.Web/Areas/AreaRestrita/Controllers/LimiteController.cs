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
        public ActionResult CadastroAmostras()
        {
            var model = new MetodosPartial();
            model.CadastroLoteAmostra = new CadastroLoteAmostraViewModel();
            model.CadastroLoteAmostra.UsuarioAnalise = new Usuario();

            try
            {
                model.ConsultaLoteAmostra = ConsultarAmostras();

                model.CadastroLoteAmostra.UsuarioAnalise = (Usuario)Session["usuario"];
                model.CadastroLoteAmostra.TipoLote = Entidades.Enuns.TipoLote.Amostra;
                model.CadastroLoteAmostra.QtdTotal = model.ConsultaLoteAmostra.Count > 0 ? model.ConsultaLoteAmostra.FirstOrDefault().QtdTotal : 0;
                model.CadastroLoteAmostra.IdMaquina = model.ConsultaLoteAmostra.Count > 0 ? model.ConsultaLoteAmostra.FirstOrDefault().Maquina.IdMaquina : 0;
            }
            catch (Exception e)
            {
                TempData["Sucesso"] = false;
                TempData["Mensagem"] = "Erro: " + e.Message;
            }

            return View(model);
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
                    TempData["Mensagem"] = $"Lote {model.IdLote} cadastrado com sucesso";

                    ModelState.Clear();
                }
            }
            catch (Exception e)
            {
                TempData["Sucesso"] = false;
                TempData["Mensagem"] = $"Erro: {e.Message}";
            }
            return CadastroAmostras();
        }

        public JsonResult CalculoLimiteControle()
        {
            var model = new LimiteControleViewModel();
            model.LimiteControle = new LimiteControle();

            model.ConsultaLoteAmostra = ConsultarAmostras();

            double totalDefeitos = model.ConsultaLoteAmostra.Sum(m => m.QtdReprovada);
            double qtdAmostras = model.ConsultaLoteAmostra.Count();
            double qtdInspecao = model.ConsultaLoteAmostra.FirstOrDefault().QtdTotal;
            double p = Math.Round(totalDefeitos / (qtdAmostras * qtdInspecao), 4);

            model.LimiteControle.LC = Convert.ToDecimal(p);
            model.LimiteControle.LSC = Math.Round(Convert.ToDecimal(p + 3 * Math.Sqrt((p * (1 - p)) / qtdAmostras)),4);
            model.LimiteControle.LIC = Math.Round(Convert.ToDecimal(p - 3 * Math.Sqrt((p * (1 - p)) / qtdAmostras)),4);

            return Json(model);
        }

        private List<ConsultaLoteAmostraViewModel> ConsultarAmostras()
        {
            try
            {
                var lista = new List<ConsultaLoteAmostraViewModel>();
                foreach (var item in new LoteDAL().ConsultarAmostras())
                {
                    var m = new ConsultaLoteAmostraViewModel();
                    m.Maquina = new Maquina();

                    m.IdLote = item.IdLote;
                    m.DataHora = item.DataHora.ToString("dd/MM/yyyy HH:mm");
                    m.QtdTotal = item.QtdTotal;
                    m.QtdReprovada = item.QtdReprovada;
                    m.PercentualReprovado = Math.Round(item.PercentualReprovado*100, 2);
                    m.Comentario = item.Comentario;
                    m.UsuarioAnalise = item.UsuarioAnalise.Nome;
                    m.TipoLote = item.TipoLote;
                    m.Maquina = item.Maquina;

                    lista.Add(m);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}