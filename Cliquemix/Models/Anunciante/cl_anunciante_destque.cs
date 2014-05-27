using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Anunciante
{
    public class cl_anunciante_destque
    {
        #region _Atributos 
            private Int32 _adid;                     //codigo anunciante destaque;
            private Int32 _aid;                      //codigo anunciante;
            private Int32 _did;                      //codigo destaque;
            private Int32 _adsid;                    //codigo anunciante destaque status;
            private float _qt_credito_na_compra;     // Quantidade de creditos feitos na compra;
            private DateTime _dt_associacao;         //data e hora da associação do anunciante ao destaque;
        #endregion

        #region _Propriedades 

        public int Adid
        {
            get { return _adid; }
            set { _adid = value; }
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
            get { return _qt_credito_na_compra; }
            set { _qt_credito_na_compra = value; }
        }

        public DateTime DtAssociacao
        {
            get { return _dt_associacao; }
            set { _dt_associacao = value; }
        }
        #endregion

    }
}