using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Todos
{
    public class cl_ban
    {
        #region _Atributos 
        private Int32 _baid; //Código do ban
        private Int32 _btid; //Código do tipo do ban
        private Int32 _uid; //Código do usuário banido
        private DateTime _dt_ocorrido; //Data do ban ocorrido
        #endregion

        #region _Propriedades 
        public int Baid
        {
            get { return _baid; }
            set { _baid = value; }
        }

        public int Btid
        {
            get { return _btid; }
            set { _btid = value; }
        }

        public int Uid
        {
            get { return _uid; }
            set { _uid = value; }
        }

        public DateTime DtOcorrido
        {
            get { return _dt_ocorrido; }
            set { _dt_ocorrido = value; }
        }
        #endregion
    }
}