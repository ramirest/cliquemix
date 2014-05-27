using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Anunciante
{
    public class cl_funcionalidade
    {
        #region _Atributos 

        private Int32 _fid; //codigo da funcionalidade
        private string _dsfuncionalidade;
        #endregion

        #region _Proriedades 

        public int Fid
        {
            get { return _fid; }
            set { _fid = value; }
        }

        public string Dsfuncionalidade
        {
            get { return _dsfuncionalidade; }
            set { _dsfuncionalidade = value; }
        }
        #endregion
    }
}