using Projeto.Entidades.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.Web.Areas.AreaRestrita.Models.Users
{
    public class ConsultaViewModel
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public PerfilUsuario Perfil { get; set; }
        public bool Ativo { get; set; }
    }
}