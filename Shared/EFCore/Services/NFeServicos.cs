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
    public class NFeServicos
    {
        AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();

        ParametrosService parametrosDS = new ParametrosService(new AmbiStoreDbContextFactory());


        public async Task<ServiceResponse> GravarVenda(VENDA venda)
        {
            //Verificações de estrutura
            string erroMsg = "Falha ao gravar a nota fiscal";
            if (venda.VENDA_ITEMs.Count < 1)
                return new ServiceResponse(ServiceResponse.ServiceResponseStatus.Interrompido, erroMsg, "A nota não possui itens");
            if (venda.NATUREZA_OPERACAO is null)
                return new ServiceResponse(ServiceResponse.ServiceResponseStatus.Interrompido, erroMsg, "Não foi especificada a natureza de operação");
            if (venda.NATUREZA_OPERACAO.CFOP.CFOP < 5101 || venda.NATUREZA_OPERACAO.CFOP.CFOP > 7949)
                return new ServiceResponse(ServiceResponse.ServiceResponseStatus.Interrompido, erroMsg, "O CFOP utilizado está fora dos limites");
            if (venda.PLANO_CONTA is null)
                return new ServiceResponse(ServiceResponse.ServiceResponseStatus.Interrompido, erroMsg, "PLANO_CONTA não pode ser NULL");
            if (venda.CLIENTE is null)
                return new ServiceResponse(ServiceResponse.ServiceResponseStatus.Interrompido, erroMsg, "CLIENTE não pode ser NULL");
            if (venda.VENDEDOR is null)
                //venda.VENDEDOR = await funcionarioDS.Get(-1);
                //if (venda.VALORVENDA == 0)
                //    return new ServiceResponse(ServiceResponse.ServiceResponseStatus.Interrompido, erroMsg, "VALORVENDA não pode ser 0");
            try
            {
                _context.Update(venda);

            }
            catch (Exception ex)
            {
                return new ServiceResponse(ServiceResponse.ServiceResponseStatus.Interrompido, erroMsg, "Erro não esperado.", ex);
            }
            try
            {
                _context.Update(venda.NFE);
            }
            catch (Exception ex)
            {

                return new ServiceResponse(ServiceResponse.ServiceResponseStatus.Interrompido, erroMsg, "Erro não esperado.", ex);
            }
            try
            {
                foreach (VENDA_ITEM vendaItem in venda.VENDA_ITEMs)
                {
                    ESTOQUE estoque = await _context.ESTOQUEs.Select(x => x).Where(x => x.ID == vendaItem.ESTOQUE.ID).FirstOrDefaultAsync();
                    estoque.QUANTIDADE -= vendaItem.QTD_ITEM;
                    estoque.ULTIMA_VENDA = venda.DT_SAIDA;
                    _context.ESTOQUEs.Update(estoque);
                }
            }
            catch (Exception ex)
            {
                return new ServiceResponse(ServiceResponse.ServiceResponseStatus.Interrompido, erroMsg, "Erro não esperado.", ex);
            }
            var funcoes = new Libraries.Functions();

            try
            {
                var (credito, debito) = funcoes.CriaPartidaDobrada(
                    venda.DT_SAIDA,
                    venda.VENDA_PAGAMENTOs.Sum(x => x.VLR_PAGTO),
                    $"NF {venda.NF_NUMERO}/{venda.NF_SERIE}/{venda.NF_MODELO}",
                    venda.PLANO_CONTA,
                    await _context.PLANO_CONTAs.Select(x => x).Where(x => x.COD_CONTA == "abc").FirstOrDefaultAsync() //PLC_CONTAS_A_RECEBER
                    );
                await _context.MOVIMENTOs.AddRangeAsync(credito, debito);// Sai da conta especificada na NFe e entra nas contas a receber.
            }
            catch (Exception ex)
            {
                return new ServiceResponse(ServiceResponse.ServiceResponseStatus.Interrompido, erroMsg, "Erro não esperado.", ex);
            }
            try
            {
                foreach (var pagto in venda.VENDA_PAGAMENTOs)
                {
                    if (pagto.FORMAPAGAMENTO.PARCELAMENTO.NUMERO_PARCELA == 0)//Caso o pagamento seja à vista, o dinheiro já sai direto de contas a receber e vai para o caixa geral
                    {
                        var (credito, debito) = funcoes.CriaPartidaDobrada(
                            venda.DT_SAIDA,
                            venda.VENDA_PAGAMENTOs.Sum(x => x.VLR_PAGTO),
                            $"NF {venda.NF_NUMERO}/{venda.NF_SERIE}/{venda.NF_MODELO}",
                    await _context.PLANO_CONTAs.Select(x => x).Where(x => x.COD_CONTA == "abc").FirstOrDefaultAsync(), //PLC_CONTAS_A_RECEBER
                    await _context.PLANO_CONTAs.Select(x => x).Where(x => x.COD_CONTA == "abc").FirstOrDefaultAsync() //PLC_CAIXA_GERAL
                            );
                        await _context.MOVIMENTOs.AddRangeAsync(credito, debito);

                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                return new ServiceResponse(ServiceResponse.ServiceResponseStatus.Interrompido, erroMsg, "Erro não esperado.", ex);
            }

            await _context.SaveChangesAsync();

            return new ServiceResponse(ServiceResponse.ServiceResponseStatus.Concluído, "Venda gravada com sucesso!");
        }

        public async Task<List<VENDA>> BuscaTodasAsVendas()
        {

            return await _context.VENDAs.Select(x => x)
                .Include(x => x.CLIENTE)
                .Include(x => x.NATUREZA_OPERACAO)
                .Include(x => x.TRANSPORTADORA)
                .Include(x => x.VENDEDOR)
                .Include(x => x.NFE).ToListAsync();
        }

        public async Task<VENDA> BuscaVendaPorID(int id)
        {
            return await _context.VENDAs.Select(x => x)
                .Include(x => x.CLIENTE)
                .Include(x => x.NATUREZA_OPERACAO)
                .Include(x => x.TRANSPORTADORA)
                .Include(x => x.VENDEDOR)
                .Include(x => x.NFE)
                .Where(x => x.ID == id).FirstOrDefaultAsync();
        }
    }
}
