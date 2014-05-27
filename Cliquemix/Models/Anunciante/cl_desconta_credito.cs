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
    }
}