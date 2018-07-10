using Projeto.Entidades;
using Projeto.Web.Areas.AreaRestrita.Models;
using System;
using System.Web.Mvc;

namespace Projeto.Web.Areas.AreaRestrita.Controllers
{
    public class CEPController : Controller
    {
        // GET: AreaRestrita/CEP
        public ActionResult Index()
        {
            Usuario u = (Usuario)Session["usuario"];

            if (u != null)
            {
                return View(new UsuarioViewModel() { Usuario = u });
            }
            else
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }
        }
    }
}