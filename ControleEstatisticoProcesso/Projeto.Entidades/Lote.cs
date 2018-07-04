using Projeto.Entidades.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entidades
{
    public class Lote
    {
        public int IdLote { get; set; }
        public DateTime DataHora { get; set; }
        public int QtdTotal { get; set; }
        public int QtdReprovada { get; set; }
        public decimal PercentualReprovado { get; set; }
        public Status Status { get; set; }
        public string Comentario { get; set; }
        public Usuario UsuarioAnalise { get; set; }
        public Usuario UsuarioAprovacao { get; set; }
        public TipoLote TipoLote { get; set; }

        public LimiteControle LimiteControle { get; set; }
    }
}
