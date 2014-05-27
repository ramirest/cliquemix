using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Todos
{
    public class cl_users
    {
        #region _Atributos 
        private Int32 _uid;
        private string _username;
        private string _pwd;
        private Int32 _tuid;
        #endregion

        #region _Propriedades 

        public int Uid
        {
            get { return _uid; }
            set { _uid = value; }
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Pwd
        {
            get { return _pwd; }
            set { _pwd = value; }
        }

        public int Tuid
        {
            get { return _tuid; }
            set { _tuid = value; }
        }
        #endregion
    }
}