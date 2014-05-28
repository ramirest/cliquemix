using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Anunciante
{
    public class cl_premio_destaque
    {
        #region _Atributos 
        private Int32 _pid; //Código do prêmio destaque
        private Int32 _did; //Código do destaque
        private DateTime _dtOcorrido; //Data do ocorrido
        #endregion

        #region _Propriedades 
        public int Pid
        {
            get { return _pid; }
        }

        public int Did
        {
            get { return _did; }
            set { _did = value; }
        }

        public DateTime DtOcorrido
        {
            get { return _dtOcorrido; }
            set { _dtOcorrido = value; }
        }
        #endregion

        #region _Métodos 

        #region _Método Novo Prêmio Destaque 
        public void _novo(Int32 @pCodDestaque, DateTime @pDataOcorrido)
        {
            try
            {
                Did = pCodDestaque;
                DtOcorrido = pDataOcorrido;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Editar Prêmio Destaque 
        public void _novo(Int32 @pCodPremioDestaque, Int32 @pCodDestaque, DateTime @pDataOcorrido)
        {
            try
            {
                //where _pid == pCodPremioDestaque
                Did = pCodDestaque;
                DtOcorrido = pDataOcorrido;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Excluir Prêmio Destaque 
        public void _excluir(Int32 @pCodPremioDestaque)
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
        public cl_premio_destaque() // Contrutor Padrão
        {
            _pid = 0;
            _did = 0;
            _dtOcorrido = DateTime.Now;
        }
        #endregion

        #region _Destrutores
        public ~cl_premio_destaque() // Destrutor Padrão
        {
            _pid = 0;
            _did = 0;
            _dtOcorrido = DateTime.Now;
        }
        #endregion
    }
}