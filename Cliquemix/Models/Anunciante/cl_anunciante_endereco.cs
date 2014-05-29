using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.SqlServer.Server;

namespace Cliquemix.Models.Anunciante
{
    public class cl_anunciante_endereco
    {
        #region _Atributos 
        private Int32 _peid; //codigo do endereço do anunciante
        private Int32 _pid; //codigo anunciante
        private string _cep; //CEP do anunciante
        private string _logradouro; //Logradouro do anunciante
        private Int32 _numero; //Número do anunciante
        private string _complemento; //Complemento do anunciante
        private string _bairro; //Bairro do anunciante
        private string _cidade; //Cidade do anunciante
        private string _estado; //Estado do anunciante
        #endregion

        #region _Propriedades 
        public int Peid
        {
            get { return _peid; }
        }

        public Int32 Pid
        {
            get { return _pid; }
            set { _pid = value; }
        }

        public string Cep
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

        #region _Métodos 

        #region _Método Novo Endereço do Anunciante 
        public void _novo(Int32 @pCodAnunciante, string @pCep, string @pLogradouro, Int32 @pNumero, string @pComplemento, 
            string @pBairro, string @pCidade, string @pEstado)
        {
            try
            {
                Pid = pCodAnunciante;
                Cep = pCep;
                Logradouro = pLogradouro;
                Numero = pNumero;
                Complemento = pComplemento;
                Bairro = pBairro;
                Cidade = pCidade;
                Estado = pEstado;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Editar Endereço do Anunciante 
        public void _editar(Int32 @pCodEndAnunciante, Int32 @pCodAnunciante, string @pCep, string @pLogradouro, Int32 @pNumero, string @pComplemento, 
            string @pBairro, string @pCidade, string @pEstado)
        {
            try
            {
                //where _peid == CodAnunciante
                Pid = pCodAnunciante;
                Cep = pCep;
                Logradouro = pLogradouro;
                Numero = pNumero;
                Complemento = pComplemento;
                Bairro = pBairro;
                Cidade = pCidade;
                Estado = pEstado;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Excluir Endereço do Anunciante 
        public void _excluir(Int32 @CodEndAnunciante)
        {
            try
            {
                //Delete SQL
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #endregion

        #region _Construtores 
        public cl_anunciante_endereco() // Contrutor Padrão
        {
            _peid = 0;
            _pid = 0;
            _cep = string.Empty;
            _logradouro = string.Empty;
            _numero = 0;
            _complemento = string.Empty;
            _bairro = string.Empty;
            _cidade = string.Empty;
            _estado = string.Empty;
        }
        #endregion

        #region _Destrutores 
        ~cl_anunciante_endereco() // Destrutor Padrão
        {
            _peid = 0;
            _pid = 0;
            _cep = string.Empty;
            _logradouro = string.Empty;
            _numero = 0;
            _complemento = string.Empty;
            _bairro = string.Empty;
            _cidade = string.Empty;
            _estado = string.Empty;
        }        
        #endregion
    }
}