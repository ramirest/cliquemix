using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cliquemix.Models.Todos;

namespace Cliquemix.Models.Todos
{
    public class cl_tos_consumidor : cl_tos
    {
        #region _Atributos 
        private Int32 _tcid; //codigo do termo de serviço do consumidor;
        #endregion

        #region _Propriedades 
        public int Tcid
        {
            get { return _tcid; }
            set { _tcid = value; }
        }
        #endregion
    }
}