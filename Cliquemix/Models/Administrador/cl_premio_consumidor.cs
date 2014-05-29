using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Todos
{
    public class cl_premio_consumidor
    {
        #region _Atributos 
        private Int32 _pcid; //Código do prêmio consumidor (PK)
        private Int32 _pid; //Código do prêmio (FK)
        private Int32 _cacid; //Código da campanha anúncio consumidor (FK)
        private DateTime _dtOcorrido ; //Data do ocorrido
        #endregion

        #region _Propriedades 
        public int Pcid
        {
            get { return _pcid; }
        }

        public int Pid
        {
            get { return _pid; }
            set { _pid = value; }
        }

        public int Cacid
        {
            get { return _cacid; }
            set { _cacid = value; }
        }

        public DateTime DtOcorrido
        {
            get { return _dtOcorrido; }
            set { _dtOcorrido = value; }
        }
        #endregion

        #region _Métodos 

        #region _Método Novo Prêmio Consumidor 
        public void _novo(Int32 @pCodPremio, Int32 @pCodCampAnunConsumidor, DateTime @pDataOcorrido)
        {
            try
            {
                Pid = pCodPremio;
                Cacid = pCodCampAnunConsumidor;
                DtOcorrido = pDataOcorrido;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Editar Prêmio Consumidor 
        public void _editar(Int32 @pCodPremioConsumidor, Int32 @pCodPremio, Int32 @pCodCampAnunConsumidor, DateTime @pDataOcorrido)
        {
            try
            {
                //where pcid == pCodPremioConsumidor
                Pid = pCodPremio;
                Cacid = pCodCampAnunConsumidor;
                DtOcorrido = pDataOcorrido;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Excluir Prêmio Consumidor 
        public void _excluir(Int32 @codPremioConsumidor)
        {
            try
            {
                //Delete SQL
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #endregion

        #region _Construtores 
        public cl_premio_consumidor() // Contrutor Padrão
        {
            _pcid = 0;
            _pid = 0;
            _cacid = 0;
            _dtOcorrido = DateTime.Now;
        }
        #endregion

        #region _Destrutores 
        ~cl_premio_consumidor() // Destrutor Padrão
        {
            _pcid = 0;
            _pid = 0;
            _cacid = 0;
            _dtOcorrido = DateTime.Now;
        }        
        #endregion
    }
}