using System.ComponentModel;

namespace Tarefas.Domain.Enuns
{
    public enum TipoDeAcesso
    {
        Usuario = 1,
        Manager = 2,
        Admin = 3

    }

    public enum Status
    {
        A_fazer = 1,
        Em_andamentos = 2,
        Impendimento = 3,
        Cancelada = 4,
        Concluida = 5
    }

    public enum Prioridades
    {
        Baixa = 1,
        Média = 2,
        Alta = 3
    }

    public enum SistemaRequisitante : short
    {
        [Description("adm")]
        administracao = 0,
        [Description("art")]
        arearestrita = 1,
        [Description("svc")]
        sivic = 2,
        Principal = 4
    }
}
