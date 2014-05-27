using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Anunciante
{
    public class cl_campanha
    {
        #region _Atributos 
        private Int32 _cid; //codigo da campanha;
        private string _titulo_campanha;
        private float _dt_inicio;
        private float _dt_termino;

        private Int32 did; // codigo   destaque.
        private Int32 scid;
        #endregion

        #region _Propriedades 

        public int Cid
        {
            get { return _cid; }
            set { _cid = value; }
        }

        public string TituloCampanha
        {
            get { return _titulo_campanha; }
            set { _titulo_campanha = value; }
        }

        public float DtInicio
        {
            get { return _dt_inicio; }
            set { _dt_inicio = value; }
        }

        public float DtTermino
        {
            get { return _dt_termino; }
            set { _dt_termino = value; }
        }
        #endregion
    }
}