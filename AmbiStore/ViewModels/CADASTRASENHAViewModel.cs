using AmbiStore.Shared.EFCore.Data;
using AmbiStore.Shared.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static AmbiStore.Shared.Libraries.Static;


namespace AmbiStore.ViewModels
{
    public class CADASTRASENHAViewModel
    {

        public AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();

        public bool TemSenha
        {
            get
            {
                if (funcionario is null) return false;
                else
                {
                    if (funcionario.SENHA is null) return false;
                    else return true;
                }
            }
        }
        public FUNCIONARIO funcionario { get; set; }

        public CADASTRASENHAViewModel()
        {

        }
        public async Task GravaNovaSenha(string senha)
        {
            funcionario.SENHA = GetMD5Hash(senha);
            _context.Update(funcionario);
            await _context.SaveChangesAsync();

        }
    }
}