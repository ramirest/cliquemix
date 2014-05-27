using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cliquemix.Models.Todos;

namespace Cliquemix.Models.Todos
{
    public class cl_tos_anunciante : cl_tos
    {
        #region _Atributos 
        private Int32 _taid; // codigo termo de serviço do anunciante;
        #endregion

        #region _Propriedades 

        public int Taid
        {
            get { return _taid; }
            set { _taid = value; }
        }
        #endregion
    }


}