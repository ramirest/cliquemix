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
        private float _qt_credito; //Qtde de crédito
        private float _vl_credito; //Valor do crédito
        #endregion

        #region _Propriedades 
        public int Cid
        {
            get { return _cid; }
            set { _cid = value; }
        }

        public float QtCredito
        {
            get { return _qt_credito; }
            set { _qt_credito = value; }
        }

        public float VlCredito
        {
            get { return _vl_credito; }
            set { _vl_credito = value; }
        }
        #endregion
    }
}