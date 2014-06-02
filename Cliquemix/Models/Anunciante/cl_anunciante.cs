using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using Cliquemix.Models;

namespace Cliquemix.Models.Anunciante
{
    public class cl_anunciante
    {

        #region _Atributos 
        private Int32 _pid; //Código do anunciante;
        private string _cnpj; //CNPJ do anunciante;
        private string _razao_social; //Razão Social do anunciante
        private string _fantasia; //Nome fantasia do anunciante
        private string _contato; //Contato do anunciante
        private string _ie; //Inscrição estadual anunciante
        private string _im; //Inscrição municipal anunciante
        private string _email; //E-mail do anunciante
        private string _site; //Site do anunciante
        private string _obs; //Observações do anunciante
        private Int32 _cpid; //Código da condição de pagamento do anunciante
        private Int32 _raid; //Código do ramo de atividade do anunciante
        private Int32 _taid; //Código do termo do anunciante
        private float _saldo_creditos; // Saldo atual de créditos do anunciante
        private Boolean _leu_tos; //Identifica se o anunciante leu os termos
        //private IList<cl_anunciante> Anunciantes;
        #endregion

        #region _Propriedades 

        public int Pid
        {
            get { return _pid; }
        }       

        public string Cnpj
        {
            get { return _cnpj; }
            set { _cnpj = value; }
        }

        public string RazaoSocial
        {
            get { return _razao_social; }
            set { _razao_social = value; }
        }

        public string NomeFantasia
        {
            get { return _fantasia; }
            set { _fantasia = value; }
        }

        public string Contato
        {
            get { return _contato; }
            set { _contato = value; }
        }

        public string InscEstadual
        {
            get { return _ie; }
            set { _ie = value; }
        }

        public string InscMunicipal
        {
            get { return _im; }
            set { _im = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Site
        {
            get { return _site; }
            set { _site = value; }
        }

        public string Obs
        {
            get { return _obs; }
            set { _obs = value; }
        }

        public int Cpid
        {
            get { return _cpid; }
            set { _cpid = value; }
        }

        public int Raid
        {
            get { return _raid; }
            set { _raid = value; }
        }

        public int Taid
        {
            get { return _taid; }
            set { _taid = value; }
        }

        public float SaldoCreditos
        {
            get { return _saldo_creditos; }
            set { _saldo_creditos = value; }
        }

        public bool LeuTos
        {
            get { return _leu_tos; }
            set { _leu_tos = value; }
        }
        #endregion

        #region _Construtores 
        public cl_anunciante() // Contrutor Padrão
        {
            _cnpj = "00.000.000/0000-00";
            _razao_social = "Razão Social";
            _fantasia = "Nome Fantasia";
            _contato = "nome de contato";
        }
        #endregion


        #region _Destrutores 
        ~cl_anunciante() // Destrutor Padrão
        {

        }
        #endregion
    }
}