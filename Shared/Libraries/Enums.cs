using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AmbiStore.Shared.Libraries.Enums
{
    public enum StatusContribuicao
    {
        Contribuinte,
        //[Display(Name = "Não Contribuinte")]
        //[Description("Não Contribuinte")]
        NaoContribuinte,
        Isento
    }

    public enum Status { Inativo = 0, Ativo = 1 }

    public enum Status_Nota { Emitida, Impressa, Cancelada }

    public enum ModalidadeFrete
    {
        [Display(Name = "Frete Por Conta")]
        ContratacaoFretePorcontaDoRemetenteCIF = 0,
        ContratacaoFretePorContaDestinatárioFOB = 1,
        Terceiros = 2,
        Remetente = 3,
        Destinatario = 4,
        SemFrete = 9
    }
    public enum IndicacaoDePresença
    {
        [Display(Name = "Indicação de presença")]
        NãoSeAplica = 0, 
        OperacaoPresencial = 1,
        OperacaoNaoPresencialPelaInternet = 2,  
        OperacaooNaoPresencialTeleatendimento = 3, 
        NFCeOperacaoComEntregaDomicilio = 4, 
        OperacaoPresencialForaDoEstabelecimento = 5,
        OperacaoNaoPresencialOutros = 9 
    }

    public enum TipoItem
    {
        [Description("Matéria Prima")]
        MateriaPrima = 1,
        [Description("Embalagem")]
        Embalagem = 2,
        [Description("Produto Em Processo")]
        ProdutoEmProcesso = 3,
        [Description("Produto Acabado")]
        ProdutoAcabado = 4,
        [Description("Subproduto")]
        Subproduto = 5,
        [Description("Produto Intermediário")]
        ProdutoIntermediario = 6,
        [Description("Material de Uso e Consumo")]
        MaterialUsoEConsumo = 7,
        [Description("Ativo Imobilizado")]
        AtivoImobilizado = 8,
        [Description("Serviço")]
        Servico = 9,
        [Description("Mercadoria Para Revenda")]
        MercadoriaParaRevenda = 10,
        [Description("Outros Insumos")]
        OutrosInsumos = 11,
        [Description("Outros")]
        Outros = 99
    }
    public enum IndicadorIE
    {
        [Display(Name = "Indicador da IE do Destinatário")]       
        ContribuinteICMS = 1,
        ContribuinteIsentoIEICMS = 2,
        NaoContribuinte = 3       
    }

    public enum CST_PIS_COFINS
    {
     
        [Description("01 - Operação Tributável com Alíquota Básica")]
        AliquotaBasica = 1,
        [Description("02 - Operação Tributável com Alíquota Diferenciada")]
        AliquotaDiferenciada = 2,
        [Description("03 - Operação Tributável com Alíquota por Unidade de Medida de Produto")]
        AliquotaPorUnidade = 3,
        [Description("04 - Operação Tributável Monofásica - Revenda a Alíquota Zero")]
        Monofasica = 4,
        [Description("05 - Operação Tributável por Substituição Tributária")]
        SubstTributaria = 5,
        [Description("06 - Operação Tributável a Alíquota Zero")]
        AliquotaZero = 6,
        [Description("07 - Operação Isenta da Contribuição")]
        Isenta = 7,
        [Description("08 - Operação sem Incidência da Contribuição")]
        SemIncidencia = 8,
        [Description("09 - Operação com Suspensão da Contribuição")]
        Suspensao = 9,
        [Description("49 - Outras Operações de Saída")]
        OutrasOpsDeSaida = 49,
        [Description("99 - Outras Operações")]
        OutrasOps = 99
    }

    public enum BaixaEstoque { Nao, Sim, Ambos }

    public enum Entrada_Saida { Saida = 0, Entrada = 1 }

    public enum Pede_Info { Não, Antes, Depois }

    public enum Sat_Modelo { Nenhum, Dimep, Daruma, Tanca, Bematech, Elgin, ControlID }

    public enum Parity { Nenhuma, Ímpar, Par, Espaço }

    public enum Balanca_Modelo { Nenhuma, Toledo, Genérica }

    public enum AutoCompleteFilterMode
    {
        None = 0,
        StartsWith = 1,
        StartsWithCaseSensitive = 2,
        StartsWithOrdinal = 3,
        StartsWithOrdinalCaseSensitive = 4,
        Contains = 5,
        ContainsCaseSensitive = 6,
        ContainsOrdinal = 7,
        ContainsOrdinalCaseSensitive = 8,
        Equals = 9,
        EqualsCaseSensitive = 10,
        EqualsOrdinal = 11,
        EqualsOrdinalCaseSensitive = 12,
        Custom = 13
    }

    public enum UsoFMAPAGTO { CupomFiscal, NotaFiscal, Ambos }

    public enum TipoMovCaixa { Sangria, Suprimento }

    public enum TipoDAV
    {
        PedidoDeVenda,
        Orcamento,
        OrdemServico
    }

    public enum TipoComando
    {
        StandardCommand,
        SQLCommand,
        FileCommand
    }
    public enum TipoJuros
    {
        Diario,
        Mensal

    }
    public enum MotivoDesoneracao
    {
        Taxi = 1,
        Uso_Na_Agropecuaria = 3,
        Frotista_Locadora = 4,
        Diplomatico_Consular = 5,
        UMAOALC = 6,    // Util.Motoc.Amaz.Ocid.Areas Liv.Com.(R.714/88-790/94 CONTRAN
        Suframa = 7,
        Venda_a_Orgao_Publico = 8,
        Outros = 9,
        Deficiente_Condutor = 10,
        OrgaoDeFomentoDesenvolvimentoAgropecuario = 12,
        Olimpiadas_Rio_2016 = 16,
        Solicitado_Pelo_Fisco = 90

    }


    public enum TipoProtesto
    {
        DiasCorridos,
        DiasUteis,
        NaoProtestar
    }

    public enum TipoLograd
    {
        Alameda,
        Área,
        Avenida,
        Estrada,
        Praça,
        Rodovia,
        Rua,
        Travessa,
        Outros
    }

    public enum PrioridadeCHAMADO
    {
        Baixa = 100,
        [Display(Name = "Média")]
        Media = 200,
        Alta = 300,
        Urgente = 400,

    }

    public enum MotivoRemessa
    {
        Outros,
        [Display(Name = "Reposição de Estoque")]
        Reposicao,
        [Display(Name = "Novo Produto")]
        NovoProduto,
        [Display(Name = "Remessa para Retirada")]
        Retirada
    }

    public enum Cargo
    {
        Atendente,
        Vendedor,
        Técnico,
        Usuário,
        Gerente,
        Comprador,
        Entregador,
        Logístico
    }

    public enum Moeda
    {
        USD,
        USDT,
        CAD,
        EUR,
        GBP,
        ARS,
        BTC,
        LTC,
        JPY,
        CHF,
        AUD,
        CNY,
        ILS,
        ETH,
        XRP
    }
    public enum TipoItemCadastro
    {
        Nenhum,
        Serial,
        Grade
    }

    public enum TipoMovimento
    {
        Credito,
        Debito
    }
    public enum Modulo
    {
        Contatos,
        Estoque,
        Emitente,
        Vendas,
        Compras,
        DAVs
    }

    public enum EstoqueParam { Conferido, Achado, Inexistente}

    public enum TipoPesquisaCBB { Containing, StartsWith }

    public enum StatusCaixaEnum { Fechado, Livre, EmVenda, Totalizacao, EmDevolucao }

    public enum TipoDesconto { Absoluto, Porcentual }
}
