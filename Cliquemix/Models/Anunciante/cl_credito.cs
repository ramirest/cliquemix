using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Anunciante
{
    public class cl_credito
    {
        #region _Atributos 
        private Int32 _cid; //Código do crédito
        private float _qtCredito; //Qtde de crédito
        private float _vlCredito; //Valor do crédito
        #endregion

        #region _Propriedades 
        public int Cid
        {
            get { return _cid; }
        }

        public float QtCredito
        {
            get { return _qtCredito; }
            set { _qtCredito = value; }
        }

        public float VlCredito
        {
            get { return _vlCredito; }
            set { _vlCredito = value; }
        }
        #endregion

        #region _Métodos 

        #region _Método Novo Crédito 
        public void _novo(float @pQtdeCredito, float @pVlCredito)
        {
            try
            {
                QtCredito = pQtdeCredito;
                VlCredito = pVlCredito;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Editar Crédito 
        public void _editar(Int32 @pCodCredito, float @pQtdeCredito, float @pVlCredito)
        {
            try
            {
                //where cid == CodCredito
                QtCredito = pQtdeCredito;
                VlCredito = pVlCredito;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Excluir Crédito 
        public void _excluir(Int32 @pCodCredito)
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
        public cl_credito() // Contrutor Padrão
        {
            _cid = 0;
            _qtCredito = 0;
            _vlCredito = 0;
        }
        #endregion

        #region _Destrutores 
        public ~cl_credito() // Destrutor Padrão
        {
            _cid = 0;
            _qtCredito = 0;
            _vlCredito = 0;
        }
        #endregion
    }
}