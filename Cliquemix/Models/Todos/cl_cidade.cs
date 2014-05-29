using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Todos
{
    public class cl_cidade
    {
        #region _Atributos 
        private Int32 _cid; //Código da cidade
        private string _nomeCidade; //Nome da cidade
        private Int32 _eid; //Código do estado vinculado
        #endregion

        #region _Propriedades 
        public Int32 Cid
        {
            get { return _cid; }
        }

        public string NomeCidade
        {
            get { return _nomeCidade; }
            set { _nomeCidade = value; }
        }

        public Int32 Eid
        {
            get { return _eid; }
            set { _eid = value; }
        }
        #endregion

        #region _Métodos 

        #region _Método Nova Cidade 
        public void _novo(string @pNomeCidade, Int32 @pCodEstado)
        {
            try
            {
                NomeCidade = pNomeCidade;
                Eid = pCodEstado;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Editar Cidade 
        public void _editar(Int32 @pCodCidade, string @pNomeCidade, Int32 @pCodEstado)
        {
            try
            {
                //where _cid == pCodCidade
                NomeCidade = pNomeCidade;
                Eid = pCodEstado;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Excluir Cidade 
        public void _excluir(Int32 @pCodCidade)
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
        public cl_cidade() // Contrutor Padrão
        {
            _cid = 0;
            _nomeCidade = string.Empty;
            _eid = 0;
        }
        #endregion

        #region _Destrutores 
        ~cl_cidade() // Destrutor Padrão
        {
            _cid = 0;
            _nomeCidade = string.Empty;
            _eid = 0;
        }        
        #endregion
    }
}