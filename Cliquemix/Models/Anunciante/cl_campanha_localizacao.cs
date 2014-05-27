using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Anunciante
{
    public class cl_campanha_localizacao
    {
        #region _Atributos 
        private Int32 _clid;
        private Int32 _caid;
        private Int32 _pid;
        private Int32 _eid;
        private Int32 _cid;
        #endregion

        #region _Propriedades 
        public int Clid
        {
            get { return _clid; }
            set { _clid = value; }
        }

        public int Caid
        {
            get { return _caid; }
            set { _caid = value; }
        }

        public int Pid
        {
            get { return _pid; }
            set { _pid = value; }
        }

        public int Eid
        {
            get { return _eid; }
            set { _eid = value; }
        }

        public int Cid
        {
            get { return _cid; }
            set { _cid = value; }
        }
        #endregion
    }
}