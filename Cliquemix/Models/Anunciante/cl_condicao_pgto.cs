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
        private Boolean _ativo;         // o status ativo receberá apenas '0' ou '1';
        #endregion

        #region _Propriedades 
        public int Cpid
        {
            get { return _cpid; }
        }

        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }

        public Boolean Ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }
        #endregion

        #region _Métodos 

        #region _Método Novo Condição de Pagamento 
        public void _novo(string @pDescCondPagto, Boolean @pAtivo)
        {
            try
            {
                Descricao = pDescCondPagto;
                Ativo = pAtivo;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Editar Condição de Pagamento 
        public void _editar(Int32 @pCodCondPagto, string @pDescCondPagto, Boolean @pAtivo)
        {
            try
            {
                //where _cpid == CodCondPagto
                Descricao = pDescCondPagto;
                Ativo = pAtivo;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Excluir Compra Crédito 
        public void _excluir(Int32 @pCodCondPagto)
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
        public cl_condicao_pgto() // Contrutor Padrão
        {
            _cpid = 0;
            _descricao = string.Empty;
            _ativo = false;
        }
        #endregion

        #region _Destrutores 
        ~cl_condicao_pgto() // Destrutor Padrão
        {
            _cpid = 0;
            _descricao = string.Empty;
            _ativo = false;
        }
        #endregion
    }
}