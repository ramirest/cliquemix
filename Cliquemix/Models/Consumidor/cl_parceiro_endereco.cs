using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Anunciante
{
    public class cl_parceiro_endereco
    {
        #region _Atributos 
        private Int32 _peid; //codigo do parceiro endereço
        private Int32 _pid; //codigo parceiro (anunciante)
        private Int32 _cep;
        private string _logradouro;
        private Int32 _numero;
        private string _complemento;
        private string _bairro;
        private string _cidade;
        private string _estado;
        #endregion

        #region _Propriedades 

        public int Peid
        {
            get { return _peid; }
            set { _peid = value; }
        }

        public int Pid
        {
            get { return _pid; }
            set { _pid = value; }
        }

        public int Cep
        {
            get { return _cep; }
            set { _cep = value; }
        }

        public string Logradouro
        {
            get { return _logradouro; }
            set { _logradouro = value; }
        }

        public int Numero
        {
            get { return _numero; }
            set { _numero = value; }
        }

        public string Complemento
        {
            get { return _complemento; }
            set { _complemento = value; }
        }

        public string Bairro
        {
            get { return _bairro; }
            set { _bairro = value; }
        }

        public string Cidade
        {
            get { return _cidade; }
            set { _cidade = value; }
        }

        public string Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }
        #endregion
    }
}