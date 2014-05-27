using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Todos
{
    public class cl_estado
    {
        #region _Atributos 
        private Int32 _eid; //Código do estado
        private string _nome_estado; //Nome do estado
        private string _sg_estado; //Sigla do estado
        #endregion

        #region _Propriedades 

        public int Eid
        {
            get { return _eid; }
            set { _eid = value; }
        }

        public string NomeEstado
        {
            get { return _nome_estado; }
            set { _nome_estado = value; }
        }

        public string SgEstado
        {
            get { return _sg_estado; }
            set { _sg_estado = value; }
        }
        #endregion
    }
}