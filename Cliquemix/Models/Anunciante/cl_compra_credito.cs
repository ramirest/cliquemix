using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Anunciante
{
    public class cl_compra_credito
    {
        #region _Atributos 
        private Int32 _ccid; //Código da compra de crédito
        private Int32 _pid; //Código do anunciante vinculado à compra de créditos
        private Int32 _crid; //Código do pacote de crédito vinculado à compra
        private float _qtCompra; //Quantidade de créditos comprada
        #endregion

        #region _Propriedades 
        public int Ccid
        {
            get { return _ccid; }
        }

        public int Pid
        {
            get { return _pid; }
            set { _pid = value; }
        }

        public int Crid
        {
            get { return _crid; }
            set { _crid = value; }
        }

        public float QtCompra
        {
            get { return _qtCompra; }
            set { _qtCompra = value; }
        }
        #endregion

        #region _Métodos 

        #region _Método Novo Compra Crédito 
        public void _novo(Int32 @pCodAnunciante, Int32 @pCodPacoteCredito, float @pQtdeCompra)
        {
            try
            {
                Pid = pCodAnunciante;
                Crid = pCodPacoteCredito;
                QtCompra = pQtdeCompra;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Editar Compra Crédito 
        public void _editar(Int32 @pCodCompraCredito, Int32 @pCodAnunciante, Int32 @pCodPacoteCredito, float @pQtdeCompra)
        {
            try
            {
                //where ccid == pCodCompraCredito
                Pid = pCodAnunciante;
                Crid = pCodPacoteCredito;
                QtCompra = pQtdeCompra;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Excluir Compra Crédito 
        public void _excluir(Int32 @pCodCompraCredito)
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
        public cl_compra_credito() // Contrutor Padrão
        {
            _ccid = 0;
            _pid = 0;
            _crid = 0;
            _qtCompra = 0;
        }
        #endregion

        #region _Destrutores 
        public ~cl_compra_credito() // Destrutor Padrão
        {
            _ccid = 0;
            _pid = 0;
            _crid = 0;
            _qtCompra = 0;
        }
        #endregion
    }
}