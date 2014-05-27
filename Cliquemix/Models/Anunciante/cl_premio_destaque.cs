using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Anunciante
{
    public class cl_premio_destaque
    {
        #region _Atributos 
        private Int32 _pid; //Código do prêmio destaque
        private Int32 _did; //Código do destaque
        private DateTime _dt_ocorrido; //Data do ocorrido
        #endregion

        #region _Propriedades 
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