using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Anunciante
{
    public class cl_campanha_anuncio
    {
        #region _Atributos 

        private Int32 cid;      //codigo da campanha;
        private Int32 aid;      //codigo do anuncio;
        private Int32 casid;    //codigo do status do anuncio;

        #endregion

        #region _Propriedades

        public int Cid
        {
            get { return cid; }
            set { cid = value; }
        }

        public int Aid
        {
            get { return aid; }
            set { aid = value; }
        }

        public int Casid
        {
            get { return casid; }
            set { casid = value; }
        }
        #endregion
    }
}