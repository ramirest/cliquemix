using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Administrador
{
    public class cl_banner
    {
        #region _Atributos

        private Int32 _bid;                  //codigo do banner;
        private string _titulo_banner;       // titulo do banner;

        #endregion
        //
        #region _Propriedades

        public int Bid
        {
            get { return _bid; }
            set { _bid = value; }
        }

        public string TituloBanner
        {
            get { return _titulo_banner; }
            set { _titulo_banner = value; }
        }

        #endregion
    }
}