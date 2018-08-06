using Projeto.DAL.Persistencia;
using Projeto.Entidades;
using Projeto.Web.Areas.AreaRestrita.Models.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto.Web.Areas.AreaRestrita.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        // GET: AreaRestrita/Usuario
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Remove("usuario");

            return RedirectToAction("Login", "Home", new { area = "" });
        }

        [Authorize(Roles = "Administrador")]
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
                ModelState.Remove("Setor");
                if (ModelState.IsValid)
                {
                    var u = new Usuario();

                    u.Nome = model.Nome;
                    u.Login = model.Login.ToLower();
                    u.Perfil = model.Perfil;
                    u.Setor = model.Setor;
                    u.Senha = model.SenhaConfirm;
                    u.Ativo = true;
                    new UsuarioDAL().CadastrarUsuario(u);

                    TempData["Sucesso"] = true;
                    TempData["Mensagem"] = $"Usuário {u.Login} cadastrado com sucesso";

                    return RedirectToAction("Cadastro", "Usuario");
                }
            }
            catch (Exception e)
            {
                TempData["Sucesso"] = false;
                TempData["Mensagem"] = "Erro: " + e.Message;
            }

            return View(new CadastroViewModel());
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public ActionResult Consulta()
        {
            var lista = new List<ConsultaViewModel>();

            foreach (var item in new UsuarioDAL().ListarUsuarios())
            {
                var model = new ConsultaViewModel();
                model.IdUsuario = item.IdUsuario;
                model.Nome = item.Nome;
                model.Login = item.Login;
                model.Perfil = item.Perfil;
                model.Setor = item.Setor != 0 ? item.Setor.ToString() : string.Empty;
                model.Ativo = item.Ativo;

                lista.Add(model);
            }
            return View(lista);
        }

        public Usuario Consulta(string login, string senha)
        {
            try
            {
                return new UsuarioDAL().ConsultarUsuario(login.ToLower(), senha);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Edicao(int id)
        {
            try
            {
                Usuario u = new UsuarioDAL().ConsultarUsuario(id);

                var model = new EdicaoViewModel();
                model.IdUsuario = u.IdUsuario;
                model.Nome = u.Nome;
                model.Login = u.Login;
                model.Ativo = u.Ativo;
                model.Perfil = u.Perfil;
                model.Setor = u.Setor;

                return View(model);
            }
            catch (Exception e)
            {
                TempData["Sucesso"] = false;
                TempData["Mensagem"] = "Erro: " + e.Message;
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edicao(EdicaoViewModel model)
        {
            try
            {
                ModelState.Remove("Setor");
                if (ModelState.IsValid)
                {
                    var u = new Usuario();
                    u.IdUsuario = model.IdUsuario;
                    u.Nome = model.Nome;
                    u.Login = model.Login.ToLower();
                    u.Perfil = model.Perfil;
                    u.Setor = model.Setor;
                    u.Ativo = model.Ativo;

                    new UsuarioDAL().AtualizarUsuario(u);

                    TempData["Sucesso"] = true;
                    TempData["Mensagem"] = $"Usuário {u.Login} atualizado com sucesso";

                    return RedirectToAction("Consulta", "Usuario");
                }
            }
            catch (Exception e)
            {
                ViewBag.Sucesso = false;
                ViewBag.Mensagem = "Erro: " + e.Message;
            }

            return View(new EdicaoViewModel());
        }
        
        [Authorize(Roles = "Administrador")]
        public ActionResult AlterarSenha(int id)
        {
            return View(new AlterarSenhaViewModel() { IdUsuario = id });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AlterarSenha(AlterarSenhaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var d = new UsuarioDAL();
                    d.AtualizarSenha(model.SenhaConfirm, model.IdUsuario);

                    TempData["Sucesso"] = true;
                    TempData["Mensagem"] = "Senha atualizada com sucesso";

                    return RedirectToAction("Consulta", "Usuario");
                }
            }
            catch (Exception e)
            {
                TempData["Sucesso"] = false;
                TempData["Mensagem"] = "Erro: " + e.Message;
            }
            return View();
        }
    }
}