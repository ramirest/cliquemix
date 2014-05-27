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
        private Int32 _aid; //Código do anúncio vinculado à compra de créditos
        private Int32 _cid; //Código do pacote de crédito vinculado à compra de crédito
        #endregion

        #region _Propriedades 
        public int Ccid
        {
            get { return _ccid; }
            set { _ccid = value; }
        }

        public int Aid
        {
            get { return _aid; }
            set { _aid = value; }
        }

        public int Cid
        {
            get { return _cid; }
            set { _cid = value; }
        }
        #endregion
    }
}