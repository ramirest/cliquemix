using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Todos
{
    public class cl_cidade
    {
        #region _Atributos 
        private Int32 _cid; //Código da cidade
        private string _nome_cidade; //Nome da cidade
        private Int32 _eid; //Código do estado vinculado
        #endregion

        #region _Propriedades 
        public int Cid
        {
            get { return _cid; }
            set { _cid = value; }
        }

        public string NomeCidade
        {
            get { return _nome_cidade; }
            set { _nome_cidade = value; }
        }

        public int Eid
        {
            get { return _eid; }
            set { _eid = value; }
        }
        #endregion
    }
}