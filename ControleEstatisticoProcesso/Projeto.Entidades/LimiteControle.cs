using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entidades
{
    public class LimiteControle
    {
        public int IdLimite { get; set; }
        public DateTime DataCalculo { get; set; }
        public decimal LSC { get; set; }
        public decimal LC { get; set; }
        public decimal LIC { get; set; }
        public bool Ativo { get; set; }
        public string TipoCarta { get { return "Atributo - Fração Não-Conforme"; } }

        public List<Lote> Lotes { get; set; }
    }
}
