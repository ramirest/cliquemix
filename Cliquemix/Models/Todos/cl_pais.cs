using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Todos
{
    public class cl_pais
    {
        #region _Atributos 
        private Int32 _pid; //Código do país
        private string _nome_pais; //Nome do país
        #endregion

        #region _Propriedades 
        public int Pid
        {
            get { return _pid; }
            set { _pid = value; }
        }

        public string NomePais
        {
            get { return _nome_pais; }
            set { _nome_pais = value; }
        }
        #endregion
   }
}