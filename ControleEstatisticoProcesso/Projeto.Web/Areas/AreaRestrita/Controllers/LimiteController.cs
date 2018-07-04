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
        public ActionResult AmostrasLimiteControle(int? qtdTotal)
        {
            var model = new MetodosPartial();
            model.CadastroLoteAmostra = new CadastroLoteAmostraViewModel();
            model.CadastroLoteAmostra.UsuarioAnalise = new Usuario();

            try
            {
                model.CadastroLoteAmostra.UsuarioAnalise = (Usuario)Session["usuario"];
                model.CadastroLoteAmostra.TipoLote = Entidades.Enuns.TipoLote.Amostra;
                if (qtdTotal != null)
                    model.CadastroLoteAmostra.QtdTotal = (int)qtdTotal;
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

                    l.IdLote = model.IdLote;
                    l.DataHora = DateTime.Now;
                    l.QtdTotal = model.QtdTotal;
                    l.QtdReprovada = model.QtdReprovada;
                    l.Comentario = model.Comentario;
                    l.UsuarioAnalise.IdUsuario = model.UsuarioAnalise.IdUsuario;
                    l.TipoLote = model.TipoLote;

                    new LoteDAL().CadastrarLoteAmostra(l);

                    return AmostrasLimiteControle(model.QtdTotal);
                }

            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }
    }
}