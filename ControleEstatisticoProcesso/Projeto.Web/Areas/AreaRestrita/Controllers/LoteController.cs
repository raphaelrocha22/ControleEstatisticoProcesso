using Projeto.DAL.Persistencia;
using Projeto.Entidades;
using Projeto.Web.Areas.AreaRestrita.Models.Lote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.Web.Areas.AreaRestrita.Controllers
{
    public class LoteController : Controller
    {
        // GET: AreaRestrita/Lote
        public ActionResult CadastroLotesProducao()
        {
            var model = new LoteProducaoViewModel();
            model.CadastroLoteProducao = new CadastroLoteProducaoViewModel();
            model.CadastroLoteProducao.UsuarioAnalise = new Usuario();

            try
            {
                model.ConsultaLoteProducao = ConsultarLotesProducao();

                model.CadastroLoteProducao.TipoCarta = new LimiteControle().TipoCarta;
                model.CadastroLoteProducao.UsuarioAnalise = (Usuario)Session["usuario"];
                model.CadastroLoteProducao.TipoLote = Entidades.Enuns.TipoLote.Producao;
                model.CadastroLoteProducao.QtdTotal = model.ConsultaLoteProducao.Count > 0 ? model.ConsultaLoteProducao.FirstOrDefault().QtdTotal : 0;
                model.CadastroLoteProducao.IdMaquina = model.ConsultaLoteProducao.Count > 0 ? model.ConsultaLoteProducao.FirstOrDefault().Maquina.IdMaquina : 0;
                model.LimiteControle = model.LimiteControle != null ? model.LimiteControle : new LimiteDAL().ConsultarLimiteControle(true).FirstOrDefault();
            }
            catch (Exception e)
            {
                TempData["Sucesso"] = false;
                TempData["Mensagem"] = "Erro: " + e.Message;
            }

            return View(model);
        }

        public JsonResult ExcluirLote(int id)
        {
            try
            {
                new LoteDAL().ExcluirLote(id);
                return Json(true);
            }
            catch (Exception e)
            {
                return Json($"Erro: {e.Message}");
            }
        }




        private List<ConsultaLoteProducaoViewModel> ConsultarLotesProducao(int? idLimite = null)
        {
            try
            {
                var lista = new List<ConsultaLoteProducaoViewModel>();
                foreach (var item in new LoteDAL().ConsultarLotesProducao(idLimite))
                {
                    var m = new ConsultaLoteProducaoViewModel();
                    m.Maquina = new Maquina();
                    m.LimiteControle = new LimiteControle();

                    m.IdLote = item.IdLote;
                    m.DataHora = item.DataHora.ToString("dd/MM/yyyy HH:mm");
                    m.QtdTotal = item.QtdTotal;
                    m.QtdReprovada = item.QtdReprovada;
                    m.PercentualReprovado = Math.Round(item.PercentualReprovado * 100, 2);
                    m.Status = item.Status;
                    m.Comentario = item.Comentario;
                    m.UsuarioAnalise = item.UsuarioAnalise.Nome;
                    m.UsuarioAprovacao = item.UsuarioAprovacao.Nome;
                    m.LimiteControle.LSC = Math.Round(item.LimiteControle.LSC * 100, 2);
                    m.LimiteControle.LC = Math.Round(item.LimiteControle.LC * 100, 2);
                    m.LimiteControle.LIC = Math.Round(item.LimiteControle.LIC * 100, 2);
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