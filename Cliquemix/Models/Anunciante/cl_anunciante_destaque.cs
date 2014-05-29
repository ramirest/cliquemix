using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Anunciante
{
    public class cl_anunciante_destaque
    {
        #region _Atributos 
            private Int32 _adid;                     //codigo anunciante destaque;
            private Int32 _aid;                      //codigo anunciante;
            private Int32 _did;                      //codigo destaque;
            private Int32 _adsid;                    //codigo anunciante destaque status;
            private float _qtCreditoCompra;          //Quantidade de creditos feitos na compra;
            private DateTime _dtAssociacao;         //data e hora da associação do anunciante ao destaque;
        #endregion

        #region _Propriedades 
        public int Adid
        {
            get { return _adid; }
        }

        public int Aid
        {
            get { return _aid; }
            set { _aid = value; }
        }

        public int Did
        {
            get { return _did; }
            set { _did = value; }
        }

        public int Adsid
        {
            get { return _adsid; }
            set { _adsid = value; }
        }

        public float QtCreditoNaCompra
        {
            get { return _qtCreditoCompra; }
            set { _qtCreditoCompra = value; }
        }

        public DateTime DtAssociacao
        {
            get { return _dtAssociacao; }
            set { _dtAssociacao = value; }
        }
        #endregion

        #region _Métodos

        #region _Método Novo Anunciante Destaque 
        public void _novo(Int32 @pCodAnunciante, Int32 @pCodDestaque, Int32 @pCodAnunDestStatus, float @pQtdeCreditoCompra, DateTime @pDataAssociacao)
        {
            try
            {
                Aid = pCodAnunciante;
                Did = pCodDestaque;
                Adsid = pCodAnunDestStatus;
                QtCreditoNaCompra = pQtdeCreditoCompra;
                DtAssociacao = pDataAssociacao;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Editar Anunciante Destaque 
        public void _editar(Int32 @pCodAnunDestaque, Int32 @pCodAnunciante, Int32 @pCodDestaque, Int32 @pCodAnunDestStatus, float @pQtdeCreditoCompra, DateTime @pDataAssociacao)
        {
            try
            {
                //where adid == pCodAnunDestaque
                Aid = pCodAnunciante;
                Did = pCodDestaque;
                Adsid = pCodAnunDestStatus;
                QtCreditoNaCompra = pQtdeCreditoCompra;
                DtAssociacao = pDataAssociacao;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Excluir Anunciante Destaque
        public void _excluir(Int32 @codAnunDestaque)
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
        public cl_anunciante_destaque() // Contrutor Padrão
        {
            _adid = 0;
            _aid = 0;
            _did = 0;
            _adsid = 0;
            _qtCreditoCompra = 0;
            _dtAssociacao = DateTime.Now;
        }
        #endregion

        #region _Destrutores 
        public ~cl_anunciante_destaque() // Destrutor Padrão
        {
            _adid = 0;
            _aid = 0;
            _did = 0;
            _adsid = 0;
            _qtCreditoCompra = 0;
            _dtAssociacao = DateTime.Now;  
        }
        #endregion
    }
}