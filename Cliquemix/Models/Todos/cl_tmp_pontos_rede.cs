using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Todos
{
    public class cl_tmp_pontos_rede
    {
        #region _Atributos 
        private Int32 _prid; //Código da tabela temporária pontos da rede
        private Int32 _prpid; //Código do anunciante
        private Int32 _praid; //Código do anúncio
        private Int32 _qt_pontos; //Quantidade de pontos
        #endregion

        #region _Propriedades 
        public int Prid
        {
            get { return _prid; }
            set { _prid = value; }
        }

        public int Prpid
        {
            get { return _prpid; }
            set { _prpid = value; }
        }

        public int Praid
        {
            get { return _praid; }
            set { _praid = value; }
        }

        public int QtPontos
        {
            get { return _qt_pontos; }
            set { _qt_pontos = value; }
        }
        #endregion

    }
}
