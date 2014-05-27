using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Anunciante
{
    public class cl_ramo_atividade
    {
        #region _Atributos 
            private Int32 _raid;         //Codigo do ramo de atividade
            private string _descricao;   // descrição do ramo de atividade
        #endregion

        #region _Propriedades 

        public int Raid
        {
            get { return _raid; }
            set { _raid = value; }
        }

        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }
	    #endregion
            
        



    }
}