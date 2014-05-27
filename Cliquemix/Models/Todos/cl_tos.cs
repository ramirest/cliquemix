using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Todos
{
    public abstract class cl_tos
    {
        #region _Atributos 

        private Int32 _tid;
        private string _clausula;
        #endregion

        #region _Propriedades 
        public int Tid
        {
            get { return _tid; }
            set { _tid = value; }
        }

        public string Clausula
        {
            get { return _clausula; }
            set { _clausula = value; }
        }
        #endregion
    }
}