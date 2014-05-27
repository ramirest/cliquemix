using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Anunciante
{
    public class cl_anuncio_funcionalidade
    {
        #region _Atributos 
            private Int32 _aid;
            private Int32 _fid;
        #endregion

        #region _Propriedades

        public int Aid
        {
            get { return _aid; }
            set { _aid = value; }
        }

        public int Fid
        {
            get { return _fid; }
            set { _fid = value; }
        }
        #endregion
    }
}