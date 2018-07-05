using Projeto.Entidades.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entidades
{
    public class Maquina
    {
        public int IdMaquina { get; set; }
        public string CodInterno { get; set; }
        public string Modelo { get; set; }
        public string Fabricante { get; set; }
        public Setor Setor { get; set; }
    }
}
