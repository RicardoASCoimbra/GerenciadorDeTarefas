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

    public enum TipoDeFuncao
    {
        SCRUM_MASTER = 1,
        PRODUCT_OWNER = 2,
        DESENVOLVEDOR_FRONT_END = 3,
        ANALISTA_DE_SISTEMAS = 4,
        ARQUITETO_DE_SISTEMAS = 5,
        GERENTE_DE_PROJETO = 6,
        ANALISTA_DE_QUALIDADE = 7,
        TESTER = 8,
        ANALISTA_DE_AUTOMACAO_DE_TESTES = 9,
        ANALISTA_DE_TESTE_DE_PERFORMANCE = 10,
        ANALISTA_DE_TESTE_DE_SEGURANCA = 11,
        ADMINISTRADOR_DE_REDE = 12,
        ADMINISTRADOR_DE_SISTEMAS = 13,
        ENGENHEIRO_DE_SEGURANCA = 14,
        ADMINISTRADOR_DE_BANCO_DE_DADOS = 15,
        GERENTE_DE_INFRAESTRUTURA = 16,
        ENGENHEIRO_DEVOPS = 17,
        ESPECIALISTA_EM_AUTOMACAO_DEVOPS = 18,
        ADMINISTRADOR_DE_AMBIENTE_DEVOPS = 19,
        ARQUITETO_DE_INFRAESTRUTURA_DEVOPS = 20,
        GERENTE_DEVOPS = 21,
        ANALISTA_DE_SUPORTE = 22,
        ESPECIALISTA_DE_ATENDIMENTO = 23,
        TECNICO_DE_TI = 24,
        GERENTE_DE_SUPORTE = 25,
        DESIGNER_DE_INTERFACE = 26,
        DESIGNER_DE_EXPERIENCIA = 27,
        DESIGNER_GRAFICO = 28,
        ARQUITETO_DE_INFORMACAO = 29,
        ESPECIALISTA_EM_USABILIDADE = 30,
        DESENVOLVEDOR_BACK_END = 31,
        DESENVOLVEDOR_FULSTACK = 32
    }
}
