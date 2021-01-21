using AmbiStore.Shared.EFCore.Data;
using AmbiStore.Shared.EFCore.Models;
using AmbiStore.Shared.EFCore.Services;
using AmbiStore.Shared.Libraries.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AmbiStore.Shared.Libraries.Static;

namespace AmbiStore.ViewModels
{
    public class LOGINViewModel : ViewModelBase
    {
        public AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();

        public List<FUNCIONARIO> FUNCIONARIOS
        {
            get
            {
                return _context.FUNCIONARIOs.Select(x => x).Include(x => x.CONTATO_FUNCIONARIO).Include(y => y.CARGOS).Include(x=>x.PERMISSOES)
                    .Where(x =>
                      x.CARGOS.Select(y => y).Where(y => y.CARGO == Cargo.Atendente).Any()
                      &&
                      x.STATUS == Status.Ativo
                ).ToList();
            }
        }
        public FUNCIONARIO FuncionarioSelecionado { get; set; }

        public async Task CadastraSenhaAdminAsync(string password)
        {
            FUNCIONARIO admin = await _context.FUNCIONARIOs.Select(x => x).Where(x => x.ID == -1).FirstAsync();
            admin.SENHA = GetMD5Hash(password);
            _context.Update(admin);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> VerificaConexaoAsync()
        {
            return await _context.Database.CanConnectAsync();
        }

        public bool VerificaConexao()
        {
            try
            {
                return _context.Database.CanConnect();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public LOGINViewModel()
        {

        }
    }
}