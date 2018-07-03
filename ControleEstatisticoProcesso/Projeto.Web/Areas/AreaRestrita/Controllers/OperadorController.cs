using Projeto.DAL.Persistencia;
using Projeto.Entidades;
using Projeto.Web.Areas.AreaRestrita.Models.Operador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.Web.Areas.AreaRestrita.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class OperadorController : Controller
    {
        // GET: AreaRestrita/Operador
        public ActionResult Cadastro()
        {
            return View(new CadastroViewModel());
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Cadastro(CadastroViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var o = new Operador();
                    o.Nome = model.Nome;
                    o.Setor = model.Setor;
                    o.Ativo = true;

                    new OperadorDAL().CadastrarOperador(o);

                    TempData["Sucesso"] = true;
                    TempData["Mensagem"] = $"Operador {o.Nome} cadastrado com sucesso.";

                    return RedirectToAction("Cadastro", "Operador");
                }
            }
            catch (Exception e)
            {
                TempData["Sucesso"] = false;
                TempData["Mensagem"] = $"Ops..Algo deu errado. Erro:{e.Message}";
            }
            return View(model);
        }

        public ActionResult Consulta()
        {
            var lista = new List<ConsultaViewModel>();
            
            foreach (var item in new OperadorDAL().ListarOperadores())
            {
                var m = new ConsultaViewModel();
                m.IdOperador = item.IdOperador;
                m.Setor = item.Setor.ToString();
                m.Nome = item.Nome;
                m.Ativo = item.Ativo is true ? "SIM" : "NÃO";

                lista.Add(m);
            }
            return View(lista);
        }

        public ActionResult Edicao(int id)
        {
            var model = new EdicaoViewModel();

            try
            {
                Operador o = new OperadorDAL().ConsultarOperador(id);

                model.IdOperador = o.IdOperador;
                model.Setor = o.Setor;
                model.Nome = o.Nome;
                model.Ativo = o.Ativo;
            }
            catch (Exception e)
            {
                TempData["Sucesso"] = false;
                TempData["Resultado"] = $"Ops..Algo deu errado. Erro:{e.Message}";
            }
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edicao(EdicaoViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var o = new Operador();
                    o.IdOperador = model.IdOperador;
                    o.Nome = model.Nome;
                    o.Setor = model.Setor;
                    o.Ativo = model.Ativo;

                    new OperadorDAL().AtualizarOperador(o);

                    TempData["Sucesso"] = true;
                    TempData["Mensagem"] = $"Operador {o.Nome} atualizado com sucesso.";

                    return RedirectToAction("Consulta", "Operador");
                }
            }
            catch (Exception e)
            {
                TempData["Sucesso"] = false;
                TempData["Resultado"] = $"Ops..Algo deu errado. Erro:{e.Message}";
            }

            return View(model);
        }
    }
}