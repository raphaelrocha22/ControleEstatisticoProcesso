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
            var model = new LimiteControleViewModel();
            model.CadastroLoteAmostra = new CadastroLoteAmostraViewModel();
            model.CadastroLoteAmostra.UsuarioAnalise = new Usuario();

            try
            {
                model.ConsultaLoteAmostra = ConsultarAmostras();

                model.CadastroLoteAmostra.TipoCarta = new LimiteControle().TipoCarta;
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
        public ActionResult CadastroAmostras(LimiteControleViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var l = new Lote();
                    l.UsuarioAnalise = new Usuario();
                    l.Maquina = new Maquina();

                    l.IdLote = model.CadastroLoteAmostra.IdLote;
                    l.DataHora = DateTime.Now;
                    l.QtdTotal = model.CadastroLoteAmostra.QtdTotal;
                    l.QtdReprovada = model.CadastroLoteAmostra.QtdReprovada;
                    l.PercentualReprovado = Convert.ToDecimal(l.QtdReprovada) / Convert.ToDecimal(l.QtdTotal);
                    l.Comentario = model.CadastroLoteAmostra.Comentario;
                    l.UsuarioAnalise.IdUsuario = model.CadastroLoteAmostra.UsuarioAnalise.IdUsuario;
                    l.TipoLote = model.CadastroLoteAmostra.TipoLote;
                    l.Maquina.IdMaquina = model.CadastroLoteAmostra.IdMaquina;

                    new LoteDAL().CadastrarLoteAmostra(l);

                    TempData["Sucesso"] = true;
                    TempData["Mensagem"] = $"Lote {model.CadastroLoteAmostra.IdLote} cadastrado com sucesso";

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

        [HttpPost]
        public JsonResult CalculoLimiteControle()
        {
            try
            {
                return Json(CalcularLimiteControle());
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        [HttpPost]
        public JsonResult CadastroLimiteControle()
        {
            try
            {
                var model = CalcularLimiteControle();

                var l = new LimiteControle();
                l.DataCalculo = DateTime.Now;
                l.LSC = model.LimiteControle.LSC;
                l.LC = model.LimiteControle.LC;
                l.LIC = model.LimiteControle.LIC;
                l.Usuario = (Usuario)Session["usuario"];
                l.Ativo = true;

                l.Lotes = new List<Lote>();
                foreach (var item in model.ConsultaLoteAmostra)
                {
                    l.Lotes.Add(new Lote() { IdLote = item.IdLote });
                }

                new LimiteDAL().CadastrarLimiteControle(l);

                return Json(true);
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        public ActionResult ConsultaLimiteControle()
        {
            var lista = new List<ConsultaLimiteControleViewModel>();

            foreach (var item in new LimiteDAL().ConsultarLimiteControle())
            {
                var l = new ConsultaLimiteControleViewModel();
                l.idLimite = item.IdLimite;
                l.DataCalculo = item.DataCalculo;
                l.LSC = item.LSC;
                l.LC = item.LC;
                l.LIC = item.LIC;
                l.UsuarioAnalise = item.Usuario.Nome;
                l.Ativo = item.Ativo;

                lista.Add(l);
            }
            return View(lista);
        }

        public ActionResult ConsultaAmostras(int id)
        {
            var model = new LimiteControleViewModel();
            model.ConsultaLoteAmostra = ConsultarAmostras(id);
            model.LimiteControle = new LimiteDAL().ConsultarLimiteControle(null,id).FirstOrDefault();

            return View(model);
        }

        public JsonResult DefinirLimiteAtivo(int id)
        {
            try
            {
                new LimiteDAL().DefinirLimiteAtivo(id);
                return Json(true);

            }
            catch (Exception e)
            {
                return Json($"Erro {e.Message}");
            }
        }

        

        private List<ConsultaLoteAmostraViewModel> ConsultarAmostras(int? idLimite = null)
        {
            try
            {
                var lista = new List<ConsultaLoteAmostraViewModel>();
                foreach (var item in new LoteDAL().ConsultarAmostras(idLimite))
                {
                    var m = new ConsultaLoteAmostraViewModel();
                    m.Maquina = new Maquina();

                    m.IdLote = item.IdLote;
                    m.DataHora = item.DataHora.ToString("dd/MM/yyyy HH:mm");
                    m.QtdTotal = item.QtdTotal;
                    m.QtdReprovada = item.QtdReprovada;
                    m.PercentualReprovado = Math.Round(item.PercentualReprovado * 100, 2);
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

        private LimiteControleViewModel CalcularLimiteControle()
        {
            try
            {
                var model = new LimiteControleViewModel();
                model.LimiteControle = new LimiteControle();

                model.ConsultaLoteAmostra = ConsultarAmostras();

                double totalDefeitos = model.ConsultaLoteAmostra.Sum(m => m.QtdReprovada);
                double qtdAmostras = model.ConsultaLoteAmostra.Count();
                double qtdInspecao = model.ConsultaLoteAmostra.FirstOrDefault().QtdTotal;
                double p = Math.Round(totalDefeitos / (qtdAmostras * qtdInspecao), 4);

                model.LimiteControle.LC = Convert.ToDecimal(p);
                model.LimiteControle.LSC = Math.Round(Convert.ToDecimal(p + 3 * Math.Sqrt((p * (1 - p)) / qtdAmostras)), 4);
                model.LimiteControle.LIC = Math.Round(Convert.ToDecimal(p - 3 * Math.Sqrt((p * (1 - p)) / qtdAmostras)), 4);

                return model;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}