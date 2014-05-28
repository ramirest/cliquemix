using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Anunciante
{
    public class cl_desconta_credito
    {
        #region _Atributos 
        private Int32 _dcid; //Código sequencial de desconto de crédito
        private Int32 _adid; //Código do destaque do anunciante que desconta o crédito
        private float _saldo_credito_ini; //Saldo de crédito inicial
        private float _qt_credito_final; //Qtde de crédito final
        private DateTime _dt_ocorrido; //Data do ocorrido
        #endregion

        #region _Propriedades 
        public int Dcid
        {
            get { return _dcid; }
            set { _dcid = value; }
        }

        public int Adid
        {
            get { return _adid; }
            set { _adid = value; }
        }

        public float SaldoCreditoIni
        {
            get { return _saldo_credito_ini; }
            set { _saldo_credito_ini = value; }
        }

        public float QtCreditoFinal
        {
            get { return _qt_credito_final; }
            set { _qt_credito_final = value; }
        }

        public DateTime DtOcorrido
        {
            get { return _dt_ocorrido; }
            set { _dt_ocorrido = value; }
        }
        #endregion

        #region _Métodos

        #region _Método Descontar Crédito
        public void _descontarCredito(Int32 @PcodDestaqueAnunciante, Int32 @PQtdeCreditosDescontar, DateTime @PdataOcorrido)
        {
            try
            {
                QtCreditoFinal = SaldoCreditoIni - PQtdeCreditosDescontar;
                DtOcorrido = PdataOcorrido;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Retornar Crédito 
        public void _retornarCredito(Int32 @PcodDescontoCredito)
        {
            try
            {
                //    
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #endregion

        #region _Construtores
        public cl_desconta_credito() // Contrutor Padrão
        {
            _dcid = 0;
            _adid = 0;
            _saldo_credito_ini = 0;
            _qt_credito_final = 0;
            _dt_ocorrido = DateTime.Now;
        }
        #endregion

        #region _Destrutores 
        public ~cl_desconta_credito() // Contrutor Padrão
        {
            _dcid = 0;
            _adid = 0;
            _saldo_credito_ini = 0;
            _qt_credito_final = 0;
            _dt_ocorrido = DateTime.Now;
        }
        #endregion

    }
}