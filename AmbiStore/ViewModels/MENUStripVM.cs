using AmbiStore.Shared.EFCore.Models;
using AmbiStore.Shared.Libraries.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AmbiStore.Shared.Libraries.Static;

namespace AmbiStore.ViewModels
{
    public class MENUStripVM : ViewModelBase
    {
        public bool PERMITE_CONTATOS
        {
            get
            {
                return FUN_LOGADO.PERMISSOES.Select(x => x).Where(x => x.MODULO == Modulo.Contatos && x.PERMITIDO).Any();
            }
        }
        public bool PERMITE_ESTOQUE
        {
            get
            {
                return FUN_LOGADO.PERMISSOES.Select(x => x).Where(x => x.MODULO == Modulo.Estoque && x.PERMITIDO).Any();
            }
        }
        public bool PERMITE_VENDAS
        {
            get
            {
                return FUN_LOGADO.PERMISSOES.Select(x => x).Where(x => x.MODULO == Modulo.Vendas && x.PERMITIDO).Any();
            }
        }
        public bool PERMITE_COMPRAS
        {
            get
            {
                return FUN_LOGADO.PERMISSOES.Select(x => x).Where(x => x.MODULO == Modulo.Compras && x.PERMITIDO).Any();
            }
        }
        public bool PERMITE_DAVS
        {
            get
            {
                return FUN_LOGADO.PERMISSOES.Select(x => x).Where(x => x.MODULO == Modulo.DAVs && x.PERMITIDO).Any();
            }
        }
        public bool PERMITE_DASHBOARD { get { return false; } }
        public void ChecaPermissoes()
        {
            OnPropertyChanged("PERMITE_CONTATOS");
            OnPropertyChanged("PERMITE_ESTOQUE");
            OnPropertyChanged("PERMITE_VENDAS");
            OnPropertyChanged("PERMITE_COMPRAS");
            OnPropertyChanged("PERMITE_DAVS");
            OnPropertyChanged("PERMITE_DASHBOARD");
        }
    }
}
