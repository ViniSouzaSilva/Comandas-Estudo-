using System;
using System.Collections.Generic;
using System.Text;
using AmbiStore.Shared.Serializador.NFe;
using AmbiStore.Shared.EFCore.Models;
using AmbiStore.Shared.EFCore.Data;
using System.Linq;
namespace AmbiStore.Shared.Servicos
{
   public  class ImportaNotaCompra
    {
        public void ImportaNota() 
        {
            AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();
            nfeproc nfeprocc = new nfeproc();
            var Nota = nfeprocc.NfeProc.NFe.infNFe;

            EMITENTE emit = new EMITENTE();

          
            int CodMun = int.Parse(Nota.ide.cMunFG);
            MUNICIPIO municipio = _context.MUNICIPIOs
                .Select(x => x)
                .Where(x => x.ID_MUNICIPIO == CodMun)
                .FirstOrDefault();
            
            //municipio.UF;

        }

    }
}
