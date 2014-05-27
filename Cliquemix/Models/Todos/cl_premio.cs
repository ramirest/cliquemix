using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Consumidor
{
    public class cl_premio
    {
        #region _Atributos 
        private Int32 _pid; //Código do prêmio
        private string _titulo_premio; //Título do prêmio
        private string _ds_premio; //Descrição do prêmio
        private Int32 _npid; //Código do nível de prêmio
        private DateTime _dt_criacao; //Data de criação do prêmio
        private Boolean _ativo; //Prêmio está ativo?
        #endregion

        #region _Propriedades 
        public int Pid
        {
            get { return _pid; }
            set { _pid = value; }
        }

        public string TituloPremio
        {
            get { return _titulo_premio; }
            set { _titulo_premio = value; }
        }

        public string DsPremio
        {
            get { return _ds_premio; }
            set { _ds_premio = value; }
        }

        public int Npid
        {
            get { return _npid; }
            set { _npid = value; }
        }

        public DateTime DtCriacao
        {
            get { return _dt_criacao; }
            set { _dt_criacao = value; }
        }

        public char Ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }
        #endregion
    }
}