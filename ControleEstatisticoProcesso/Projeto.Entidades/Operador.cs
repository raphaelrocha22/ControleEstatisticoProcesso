using Projeto.Entidades.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entidades
{
    public class Operador
    {
        public int IdOperador { get; set; }
        public Setor Setor { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}
