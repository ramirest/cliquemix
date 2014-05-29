using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Todos
{
    public class cl_users_tipo
    {
        #region _Atributos 
        private Int32 _utid;
        private string dsUsersTipo;
        #endregion

        #region _Propriedades 
        public int Utid
        {
            get { return _utid; }
        }

        public string DsUsersTipo
        {
            get { return dsUsersTipo; }
            set { dsUsersTipo = value; }
        }
        #endregion

        #region _Métodos 

        #region _Método Novo Tipo Usuário 
        public void _novo(string @pDsUserTipo)
        {
            try
            {
                DsUsersTipo = @pDsUserTipo;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Editar Tipo Usuário 
        public void _editar(string @pDsUserTipo)
        {
            try
            {
                //_uid == pCodUser
                DsUsersTipo = pDsUserTipo;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Excluir Tipo Usuário 
        public void _excluir(Int32 @pCodUserTipo)
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
        public cl_users_tipo() // Contrutor Padrão
        {
            _uid = 0;
            _username = string.Empty;
            _pwd = string.Empty;
            _utid = 0;
        }
        #endregion

        #region _Destrutores 
        public ~cl_users_tipo() // Destrutor Padrão
        {
            _uid = 0;
            _username = string.Empty;
            _pwd = string.Empty;
            _utid = 0;         
        }        
        #endregion
    }
}