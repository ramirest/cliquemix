using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Todos
{
    public class cl_estado
    {
        #region _Atributos 
        private Int32 _eid; //Código do estado
        private string _nomeEstado; //Nome do estado
        private string _sgEstado; //Sigla do estado
        #endregion

        #region _Propriedades 
        public Int32 Eid
        {
            get { return _eid; }
            set { _eid = value; }
        }

        public string NomeEstado
        {
            get { return _nomeEstado; }
            set { _nomeEstado = value; }
        }

        public string SgEstado
        {
            get { return _sgEstado; }
            set { _sgEstado = value; }
        }
        #endregion

        #region _Métodos 

        #region _Método Novo Estado 
        public void _novo(string @pNomeEstado, string @pSgEstado)
        {
            try
            {
                NomeEstado = pNomeEstado;
                SgEstado = pSgEstado;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Editar Estado 
        public void _novo(Int32 @pCodEstado, string @pNomeEstado, string @pSgEstado)
        {
            try
            {
                //where _codEstado == pCodEstado
                NomeEstado = pNomeEstado;
                SgEstado = pSgEstado;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Excluir Estado 
        public void _excluir(Int32 @pCodEstado)
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
        public cl_estado() // Contrutor Padrão
        {
            _eid = 0;
            _nomeEstado = string.Empty;
            _sgEstado = string.Empty;
        }
        #endregion

        #region _Destrutores 
        public ~cl_estado() // Destrutor Padrão
        {
            _eid = 0;
            _nomeEstado = string.Empty;
            _sgEstado = string.Empty;
        }        
        #endregion
    }
}