using AmbiStore.Shared.EFCore.Data;
using AmbiStore.Shared.EFCore.Models;
using AmbiStore.Shared.Libraries.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AmbiStore.Shared.Libraries.Static;

namespace AmbiStore.ViewModels
{
    public class ESTOQUEListVM : ViewModelBase
    {
        public AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();

        public List<FUNC_MOD_COLUNA> COLUNAS
        {
            get
            {
                return _context.FUNC_MOD_COLUNAs.Select(x => x).Where(x => x.FUNCIONARIO == FUN_LOGADO && x.MODULO == Modulo.Estoque).ToList();
            }
            set
            {

            }
        }

        public List<ESTOQUE> ListaEstoque
        {
            get
            {
                return _context.ESTOQUEs.Select(e => e)
                    .Include(e => e.PRODUTO_ESTOQUE)
                    .Include(e => e.SERVICO_ESTOQUE)
                    .Include(e=>e.FORNECEDOR_PREFERENCIAL)
                    .Include(e=>e.ULTIMO_FORNECEDOR)
                    .Include(e=>e.GRUPO)
                    .ToList();
            }
        }
    }
}
