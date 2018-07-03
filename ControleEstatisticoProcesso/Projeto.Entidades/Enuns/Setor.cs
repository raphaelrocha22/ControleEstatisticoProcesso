using System.ComponentModel;

namespace Projeto.Entidades.Enuns
{
    public enum Setor
    {
        [Description("SC")]
        Convencional = 1,

        [Description("SF")]
        FreeForm = 2,

        [Description("HC")]
        HC = 3,

        [Description("AR")]
        AR = 4,

        [Description("TR")]
        Triagem = 5,

        [Description("CO")]
        Coloracao = 6,

        [Description("MO")]
        Montagem = 7,

        [Description("CQ")]
        CQ = 8,

        [Description("ES")]
        Estoque = 9,

        [Description("AT")]
        Atendimento = 10
    }
}
