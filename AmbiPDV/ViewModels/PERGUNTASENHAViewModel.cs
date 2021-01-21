using AmbiStore.Shared.EFCore.Data;
using AmbiStore.Shared.Libraries.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbiPDV.ViewModels
{
    public class PERGUNTASENHAViewModel : ViewModelBase
    {
        public string TITULO { get; set; }
        public string SUBTITULO { get; set; }
        public Cargo[] CARGOS { get; set; }

        public PERGUNTASENHAViewModel()
        {

        }

        public async Task<bool> VerificaSenha(string password)
        {
            AmbiStore.Shared.Libraries.Functions functions = new AmbiStore.Shared.Libraries.Functions();
            return await functions.VerificaSenhaPorCargo(password, CARGOS);
        }
    }
}
