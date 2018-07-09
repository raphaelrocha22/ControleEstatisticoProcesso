using Projeto.Entidades;
using Projeto.Web.Areas.AreaRestrita.Models;
using System.Web.Mvc;

namespace Projeto.Web.Areas.AreaRestrita.Controllers
{
    public class CEPController : Controller
    {
        // GET: AreaRestrita/CEP
        public ActionResult Index()
        {
            return View(new UsuarioViewModel() { Usuario = (Usuario)Session["usuario"] });
        }
    }
}