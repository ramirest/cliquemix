using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Cliquemix.Models.Consumidor
{
    public class cl_premio
    {
        #region _Atributos 
        private Int32 _pid; //Código do prêmio
        private string _tituloPremio; //Título do prêmio
        private string _dsPremio; //Descrição do prêmio
        private Int32 _npid; //Código do nível de prêmio
        private DateTime _dtCriacao; //Data de criação do prêmio
        private Boolean _ativo; //Prêmio está ativo?
        #endregion

        #region _Propriedades 
        public int Pid
        {
            get { return _pid; }
        }

        public string TituloPremio
        {
            get { return _tituloPremio; }
            set { _tituloPremio = value; }
        }

        public string DsPremio
        {
            get { return _dsPremio; }
            set { _dsPremio = value; }
        }

        public int Npid
        {
            get { return _npid; }
            set { _npid = value; }
        }

        public DateTime DtCriacao
        {
            get { return _dtCriacao; }
            set { _dtCriacao = value; }
        }

        public Boolean Ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }
        #endregion

        #region _Métodos 

        #region _Método Novo Prêmio 
        public void _novo(string @pTituloPremio, string @pDsPremio, Int32 @pCodNivelPremio, DateTime @pDtCriacao, Boolean @pAtivo)
        {
            try
            {
                TituloPremio = pTituloPremio;
                DsPremio = pDsPremio;
                Npid = pCodNivelPremio;
                DtCriacao = pDtCriacao;
                Ativo = pAtivo;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Editar Prêmio 
        public void _editar(Int32 @pCodPremio, string @pTituloPremio, string @pDsPremio, Int32 @pCodNivelPremio, DateTime @pDtCriacao, Boolean @pAtivo)
        {
            try
            {
                //where pid == pCodPremio
                TituloPremio = pTituloPremio;
                DsPremio = pDsPremio;
                Npid = pCodNivelPremio;
                DtCriacao = pDtCriacao;
                Ativo = pAtivo;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Excluir Prêmio
        public void _excluir(Int32 @codPremio)
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
        public cl_premio() // Contrutor Padrão
        {
            _pid = 0;
            _tituloPremio = string.Empty;
            _dsPremio = string.Empty;
            _npid = 0;
            _dtCriacao = DateTime.Now;
            _ativo = false;
        }
        #endregion

        #region _Destrutores 
        public ~cl_premio() // Destrutor Padrão
        {
            _pid = 0;
            _tituloPremio = string.Empty;
            _dsPremio = string.Empty;
            _npid = 0;
            _dtCriacao = DateTime.Now;
            _ativo = false;
        }        
        #endregion
    }
}