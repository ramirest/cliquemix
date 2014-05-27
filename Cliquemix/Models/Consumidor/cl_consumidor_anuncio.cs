using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Consumidor
{
    //Tabela relacionada ao vínculo Anúncio x Consumidor
    public class cl_consumidor_anuncio
    {
        #region _Atributos 
        private Int32 _cid; //Código do consumidor
        private Int32 _aid; //Código do anúncio
        #endregion

        #region _Propriedades 
        public int Cid
        {
            get { return _cid; }
            set { _cid = value; }
        }

        public int Aid
        {
            get { return _aid; }
            set { _aid = value; }
        }
        #endregion

    }
}