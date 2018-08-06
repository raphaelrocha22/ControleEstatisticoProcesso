using Projeto.DAL.Persistencia;
using Projeto.Entidades;
using Projeto.Entidades.Enuns;
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
            return View(CarregarModel(new CadastroLoteProducaoViewModel()));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CadastroLotesProducao(LoteProducaoViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (new LoteDAL().VerificarLoteExiste(model.CadastroLoteProducao.IdLote))
                        throw new Exception($"Um lote com a numeração {model.CadastroLoteProducao.IdLote} já foi cadastrado no sistema");

                    if (model.CadastroLoteProducao.QtdReprovada > model.CadastroLoteProducao.QtdTotal)
                        throw new Exception("A quantidade de itens reprovados não pode ser maior que a quantidade total de itens");

                    var l = new Lote();
                    l.UsuarioAnalise = new Usuario();
                    l.UsuarioAprovacao = null;
                    l.Maquina = new Maquina();
                    l.LimiteControle = new LimiteControle();

                    l.DataHora = DateTime.Now;
                    l.TipoLote = model.CadastroLoteProducao.TipoLote;
                    l.Maquina.IdMaquina = model.CadastroLoteProducao.IdMaquina;
                    l.UsuarioAnalise = model.CadastroLoteProducao.UsuarioAnalise;
                    l.IdLote = model.CadastroLoteProducao.IdLote;
                    l.QtdTotal = model.CadastroLoteProducao.QtdTotal;
                    l.QtdReprovada = model.CadastroLoteProducao.QtdReprovada;
                    l.PercentualReprovado = Convert.ToDecimal(l.QtdReprovada) / Convert.ToDecimal(l.QtdTotal);
                    l.Comentario = model.CadastroLoteProducao.Comentario;
                    l.LimiteControle = model.LimiteControle;
                    if (l.PercentualReprovado >= model.LimiteControle.LIC && l.PercentualReprovado <= model.LimiteControle.LSC)
                    {
                        l.Status = Status.Aprovado;
                    }
                    else if (model.CadastroLoteProducao.UsuarioAprovacao.Login != null && model.CadastroLoteProducao.UsuarioAprovacao.Senha != null)
                    {
                        l.Status = Status.AprovadoSupervisao;

                        Usuario u = new UsuarioController().Consulta(model.CadastroLoteProducao.UsuarioAprovacao.Login, model.CadastroLoteProducao.UsuarioAprovacao.Senha);
                        if (u != null && (u.Perfil.Equals(PerfilUsuario.Supervisor) || u.Perfil.Equals(PerfilUsuario.Gerente) || u.Perfil.Equals(PerfilUsuario.Administrador)))
                            l.UsuarioAprovacao = u;
                        else
                            throw new Exception("As credênciais do usuário aprovação estão incorretas ou a conta não possui autorização para esta ação");
                    }
                    else
                    {
                        l.Status = Status.Reprovado;
                    }

                    new LoteDAL().CadastrarLoteProducao(l);

                    TempData["Sucesso"] = true;
                    TempData["Mensagem"] = $"Lote {model.CadastroLoteProducao.IdLote} cadastrado com sucesso";

                    ModelState.Clear();
                }
            }
            catch (Exception e)
            {
                TempData["Sucesso"] = false;
                TempData["Mensagem"] = $"Erro: {e.Message}";
            }

            return View(CarregarModel(model.CadastroLoteProducao));
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

        private LoteProducaoViewModel CarregarModel(CadastroLoteProducaoViewModel dados)
        {
            var model = new LoteProducaoViewModel();
            model.CadastroLoteProducao = new CadastroLoteProducaoViewModel();
            model.CadastroLoteProducao.UsuarioAnalise = new Usuario();

            try
            {
                if (dados != null)
                {
                    model.CadastroLoteProducao.QtdTotal = dados.QtdTotal;
                    model.CadastroLoteProducao.IdMaquina = dados.IdMaquina;
                }

                model.CadastroLoteProducao.TipoCarta = new LimiteControle().TipoCarta;
                model.CadastroLoteProducao.UsuarioAnalise = (Usuario)Session["usuario"];
                model.CadastroLoteProducao.TipoLote = TipoLote.Producao;
                model.LimiteControle = new LimiteDAL().ConsultarLimiteControle(true).FirstOrDefault();
            }
            catch (Exception e)
            {
                TempData["Sucesso"] = false;
                TempData["Mensagem"] = "Erro: " + e.Message;
            }
            return model;
        }

        private List<ConsultaLoteProducaoViewModel> ConsultarLotesProducao(int? idLimite = null, int? qtd = int.MaxValue)
        {
            try
            {
                var lista = new List<ConsultaLoteProducaoViewModel>();
                foreach (var item in new LoteDAL().ConsultarLotesProducao(idLimite, qtd))
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
                    m.UsuarioAnalise = item.UsuarioAnalise.Nome.ToString();
                    m.UsuarioAprovacao = item.UsuarioAprovacao.Nome.ToString();
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