using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using AmbiStore.Shared.EFCore.Data;
using AmbiStore.Shared.EFCore.Models;
using AmbiStore.Shared.Libraries.Enums;
using AmbiStore.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AmbiPad.ViewModels
{
    public class ComandaVM : ViewModelBase
    {
        
        public ComandaVM()
        {
           estoque =  _context.ESTOQUEs.Select(c => c).ToList();

        }
        #region  Variáveis e Propriedades
        AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();
        /*public int ID_COMANDA
        {
            get 
            {
                return 1;
            }
            set 
            {
                value = OBJETO_COMANDA[0].COMANDA_ID;
                
                

            }
        }*/
        private string id_comanda;

        public string ID_COMANDA
        {
            get { return id_comanda; }
            set { id_comanda = value; }
        }
        private int total_valor;
        public decimal TOTAL_VALOR
        {
            get { return total_valor; }
            set { }
        }



        public string DESCRICAO { get ; set; }
        public List<COMANDA_HISTORICO> OBJETO_COMANDA { get; set; }

        public List<COMANDA_HISTORICO> FECHAMENTO { get; set; }
        public List<ESTOQUE> estoque { get; set; }

        private string produ;

        public string PRODU
        {
            get { return produ; }
            set { produ = value; }
        }

        
        public ObservableCollection<COMANDA> COMANDA_LISTA {
            get { return ListaComanda(); } set { } }
        public List<COMANDA> COMANDA_LIST { get; set; }
        #endregion

        #region Metodos 
        public void PesquisaComanda() 
        
        {
         
           OBJETO_COMANDA =  _context.COMANDA_HISTORICOs.Select(c => c).Include(c => c.COMANDA).Include(c => c.ESTOQUE).Where(w => w.COMANDA_ID == int.Parse(ID_COMANDA) && w.ISDELETED == false && w.STATUS == Status.Ativo).ToList();
           
           OnPropertyChanged("OBJETO_COMANDA");
            
            if (OBJETO_COMANDA is null)
            {
                OBJETO_COMANDA.Clear();
                //OBJETO_COMANDA = _context.COMANDAs.Select(c => c).Include(c => c.COMANDA_HISTORICOS).ThenInclude(c => c.ESTOQUE).Where(w => w.ID == ID_COMANDA).ToList();
            }
            
        }
        public decimal SomaQuantidade()
        {
            int count = 0;
            decimal vlr = 0; 
            do
            {
                if (OBJETO_COMANDA.Count() > 0)
                {
                    vlr = vlr + (OBJETO_COMANDA[count].ESTOQUE.PRECO_VENDA * OBJETO_COMANDA[count].QTD);
                }
                count++;

            } while (count < OBJETO_COMANDA.Count());
            return vlr;
        }
        public void AdicionarComanda()
        {
            try
            {
                AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();
                COMANDA NHANHA = new COMANDA();
                NHANHA.DATA_CRIACAO = DateTime.Now;
                NHANHA.STATUS_COMANDA = Status.Ativo;
                _context.Update(NHANHA);
                _context.SaveChanges();
                OnPropertyChanged("COMANDA_LISTA");


            }
            catch (Exception ex) 
            {
            
            }
        }
        public void AdicionaItemComanda(string Qtd)
        {
            try   
            {
                if (int.Parse(Qtd) == 0) Qtd = "1";
                var a = DateTime.Now;
                //AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();
                COMANDA_HISTORICO CH = new COMANDA_HISTORICO();
                // estoque = _context.ESTOQUEs.Select(c => c).Where(c => c.ID == int.Parse(PRODU)).ToList();
                var produto = estoque.FirstOrDefault(c => c.ID == int.Parse(PRODU));
                CH.ESTOQUE = produto;
                CH.COMANDA_ID = int.Parse(ID_COMANDA);
                CH.DATA_COMANDA = DateTime.Now;
                CH.QTD = decimal.Parse(Qtd);
                CH.STATUS = Status.Ativo;
                CH.VLR_TOTAL = produto.PRECO_VENDA * decimal.Parse(Qtd);

                //if (COMANDA_LIST[ID_COMANDA - 1].STATUS_COMANDA == Status.Ativo)
                //if (COMANDA_LIST.Select(c=>c).Where(c=>c.ID == ID_COMANDA).FirstOrDefault()?.STATUS_COMANDA == Status.Ativo)
                if (COMANDA_LIST.FirstOrDefault(c=>c.ID == int.Parse(ID_COMANDA))?.STATUS_COMANDA == Status.Ativo)
                {
                    _context.Update(CH);
                    _context.SaveChanges();
                    OnPropertyChanged("COMANDA_LISTA");
                    PesquisaComanda();
                }
                else 
                {

                    MessageBox.Show("Não é possível usar uma comanda inativa", "Atenção", MessageBoxButton.OK, MessageBoxImage.Error);
                
                }
                
                
               

            }
            catch (Exception ex)
            {

            }
        }

        
        public ObservableCollection<COMANDA> ListaComanda()
        {
            try
            {
                AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();
                //COMANDA Comanda = new COMANDA();

                COMANDA_LIST = _context.COMANDAs.Select(c => c).ToList();
                return  Converta(COMANDA_LIST);


            }
            catch (Exception)
            {

                throw;
            }
            



        }
        public ObservableCollection<COMANDA> Converta(IEnumerable<COMANDA> original)
        {
            try
            {
                var a = new ObservableCollection<COMANDA>(original);
                return a;
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        public void DeletaItemComanda(int ID)
        {
            try
            {
                AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();
                //COMANDA Comanda = new COMANDA();
                COMANDA_HISTORICO EXCLUSAO = new COMANDA_HISTORICO();
                EXCLUSAO = _context.COMANDA_HISTORICOs.Select(c => c).Where(w => w.ID == ID).FirstOrDefault();
                EXCLUSAO.ISDELETED = true;
                _context.Update(EXCLUSAO);
                _context.SaveChanges();
                PesquisaComanda();


            }
            catch (Exception)
            {

                throw;
            }
            

        }
        public void FechaComanda(string ID) 
        {

            OBJETO_COMANDA = new List<COMANDA_HISTORICO>();
            
            OnPropertyChanged("OBJETO_COMANDA");
            /*
            try
            {
                int id = int.Parse(ID);
                AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();
                //COMANDA Comanda = new COMANDA();
               FECHAMENTO = _context.COMANDA_HISTORICOs.Select(c => c).Where(w => w.COMANDA_ID == id && w.STATUS == Status.Ativo).ToList();
                //   FECHAMENTO = _context.COMANDA_HISTORICOs.Select(c => c).Where(w => w.COMANDA_ID == id && w.STATUS == Status.Ativo).ToList();
                int count = 0;

                do
                {
                    FECHAMENTO[count].DATA_COMANDA_FECHAMENTO = DateTime.Now;
                    FECHAMENTO[count].STATUS = Status.Inativo;
                    count++;

                } while (count < FECHAMENTO.Count());

                _context.UpdateRange(FECHAMENTO);
                //_context.Update(FECHAMENTO);
                _context.SaveChanges();
                PesquisaComanda();

            }
            catch (Exception ex)
            {

                throw;
            }
           */

        }

        public void InativaComanda(int id) 
        {
            AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();
            //COMANDA Comanda = new COMANDA();
            COMANDA INATIVA = new COMANDA();
            INATIVA = _context.COMANDAs.Select(c => c).Where(w => w.ID == id).FirstOrDefault();
            if (INATIVA.STATUS_COMANDA == Status.Ativo)
            {
                INATIVA.STATUS_COMANDA = Status.Inativo;
            }
            else 
            {
                INATIVA.STATUS_COMANDA = Status.Ativo;
            
            }
            _context.Update(INATIVA);
            _context.SaveChanges();
            OnPropertyChanged("COMANDA_LISTA");
        }
        #endregion
    }
}
