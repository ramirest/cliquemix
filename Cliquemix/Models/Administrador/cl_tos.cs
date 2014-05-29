using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cliquemix.Models.Todos;

namespace Cliquemix.Models.Todos
{
    public class cl_tos
    {
        #region _Atributos 
        private Int32 _tid; //Código do termo de serviço (Term of Service) ID
        private string _titulo; //Título do termo de serviço
        private string _clausula; //Cláusula do termo de serviço
        private char _tipo; //Tipo do termo (A = Anunciante | C = Consumidor)
        #endregion

        #region _Propriedades 
        public int Tid
        {
            get { return _tid; }
        }

        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value; }
        }

        public string Clausula
        {
            get { return _clausula; }
            set { _clausula = value; }
        }

        public char Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }
        #endregion

        #region _Métodos 

        #region _Método Novo Term of Service 
        public void _novo(string @pTitulo, string @pClausula, char @pTipo)
        {
            try
            {
                Titulo = pTitulo;
                Clausula = pClausula;
                Tipo = pTipo;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Editar Term of Service 
        public void _editar(Int32 @pCodTermo, string @pTitulo, string @pClausula, char @pTipo)
        {
            try
            {
                //where _tid == pCodTermo
                Titulo = pTitulo;
                Clausula = pClausula;
                Tipo = pTipo;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Excluir Term of Service 
        public void _excluir(Int32 @pCodTermo)
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
        public cl_tos() // Contrutor Padrão
        {
            _tid = 0;
            _titulo = string.Empty;
            _clausula = string.Empty;
            _tipo = ' ';
        }
        #endregion

        #region _Destrutores 
       ~cl_tos() // Destrutor Padrão
        {
            _tid = 0;
            _titulo = string.Empty;
            _clausula = string.Empty;
            _tipo = ' ';            
        }        
        #endregion
    }


}