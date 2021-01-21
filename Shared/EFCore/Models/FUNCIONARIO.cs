using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using AmbiStore.Shared.Libraries.Enums;
using Newtonsoft.Json;

namespace AmbiStore.Shared.EFCore.Models
{
    public class FUNCIONARIO : EntityBase
    {
        public int FOLHA { get; set; }
        [MaxLength(16)]
        public decimal SALARIO { get; set; }
        public decimal VALOR_EXTRA { get; set; }
        public DateTime DATA_ADMISSAO { get; set; }
        public DateTime DATA_DEMISSAO { get; set; }
        public string RAMAL { get; set; }
        public string SENHA { get; set; }
        [Required]
        [DefaultValue(Status.Ativo)]
        public Status STATUS { get; set; }
        [MaxLength(14)]
        public string PIS { get; set; }
        [MaxLength(20)]
        public string APELIDO { get; set; }
        public string OBSERVACOES { get; set; }

        //======================= CHAVES ESTRANGEIRAS
        public int ID_CONTATO_FUNCIONARIO { get; set; }
        public CONTATO CONTATO_FUNCIONARIO { get; set; }
        public List<PERMISSAO_FUNCIONARIO> PERMISSOES { get; set; }
        public List<CARGO_FUNCIONARIO> CARGOS { get; set; }
        public List<CONTATO> VENDEDOR_PREF_FUNCIONARIO { get; set; }
        public List<VENDA> VENDA_VENDEDOR { get; set; }
        public List<FUNC_MOD_COLUNA> FUNC_MOD_COLUNAS_FUNC { get; set; }
        public List<COMPRA> COMPRA_COMPRADOR { get; set; }

    }
}