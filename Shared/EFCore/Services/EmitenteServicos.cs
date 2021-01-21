using AmbiStore.Shared.EFCore.Data;
using AmbiStore.Shared.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbiStore.Shared.EFCore.Services
{
    public class EmitenteServicos
    {
        AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();

        public async Task<EMITENTE> BuscaEmitente()
        {
            return await _context.EMITENTEs.Select(x => x).FirstAsync();
        }

        public async Task<ServiceResponse> GravaEmitente(EMITENTE emit)
        {
            emit.ID = 1;
            _context.Update(emit);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ServiceResponse(ServiceResponse.ServiceResponseStatus.Interrompido, "Erro ao gravar os dados.", "Erro não esperado.", ex);
            }
            return new ServiceResponse(ServiceResponse.ServiceResponseStatus.Concluído, "Gravado com sucesso");
        }
    }
}
