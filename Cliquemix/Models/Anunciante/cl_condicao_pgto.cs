using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Anunciante
{
    public class cl_condicao_pgto
    {
        #region _Atributos 
            private Int32 _cpid;         //Código da condição do pagamento;
            private string _descricao;   //descrição da condição do pagamento;
            private char _ativo;         // o status ativo receberá apenas 'S' ou 'N';
        #endregion

        #region _Propriedades 

        public int Cpid
        {
            get { return _cpid; }
            set { _cpid = value; }
        }

        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }

        public char Ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }
        #endregion
    }
}