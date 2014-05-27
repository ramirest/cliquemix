using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Todos
{
    public class cl_premio_consumidor
    {
        #region _Atributos

        private Int32 _cid;
        private Int32 _pid;
        private Int32 _did;
        private DateTime _dt_ocorrido ;

        #endregion



        #region _Propriedades

        public int Cid
        {
            get { return _cid; }
            set { _cid = value; }
        }

        public int Pid
        {
            get { return _pid; }
            set { _pid = value; }
        }

        public int Did
        {
            get { return _did; }
            set { _did = value; }
        }

        public DateTime DtOcorrido
        {
            get { return _dt_ocorrido; }
            set { _dt_ocorrido = value; }
        }
        #endregion
    }
}