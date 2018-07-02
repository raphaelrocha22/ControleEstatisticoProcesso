using Projeto.DAL.Persistencia;
using Projeto.Entidades;
using Projeto.Web.Models.Home;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Projeto.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var d = new UsuarioDAL();
                    Usuario u = d.ConsultarUsuario(model.Login.ToLower(), model.Senha);

                    if (u != null)
                    {
                        var ticket = new FormsAuthenticationTicket(u.Login, false, 60);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                        Response.Cookies.Add(cookie);

                        Session.Add("usuario", u);

                        return RedirectToAction("Index", "CEP", new { area = "AreaRestrita" });
                    }
                    else
                    {
                        ViewBag.Mensagem = "Acesso negado, usuário ou senha incorretos ou usuário inativo";
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Mensagem = $"Erro não esperado, por favor entre em contato com o administrador do sistema. Erro: {e.Message}";
                }
            }
            return View();
        }

        public ActionResult AlterarSenha()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AlterarSenha(UpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var d = new UsuarioDAL();
                    Usuario u = d.ConsultarUsuario(model.Login.ToLower(), model.SenhaAntiga);

                    if (u != null)
                    {
                        d.AtualizarSenha(model.Senha, u.IdUsuario);

                        ViewBag.Sucesso = true;
                        ViewBag.Mensagem = "Senha alterada com sucesso";
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.Sucesso = false;
                        ViewBag.Mensagem = "Não foi possível completar a operação, usuário ou senha incorretos ou usuário inativo";
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Sucesso = false;
                    ViewBag.Mensagem = $"Erro não esperado, por favor entre em contato com o administrador do sistema. Erro: {e.Message}";
                }
            }
            return View();
        }
    }
}