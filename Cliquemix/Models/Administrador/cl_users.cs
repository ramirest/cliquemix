using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Todos
{
    public class cl_users
    {
        #region _Atributos 
        private Int32 _uid;
        private string _username;
        private string _pwd;
        private Int32 _utid;
        #endregion

        #region _Propriedades 

        public int Uid
        {
            get { return _uid; }
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Pwd
        {
            get { return _pwd; }
            set { _pwd = value; }
        }

        public int Utid
        {
            get { return _utid; }
            set { _utid = value; }
        }
        #endregion

        #region _Métodos 

        #region _Método Novo Usuário 
        public void _novo(string @pUsername, string @pPassword, Int32 @pUserTipo)
        {
            try
            {
                Username = pUsername;
                Pwd = pPassword;
                Utid = @pUserTipo;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Editar Usuário 
        public void _editar(Int32 @pCodUser, string @pUsername, string @pPassword, Int32 @pUserTipo)
        {
            try
            {
                //_uid == pCodUser
                Username = pUsername;
                Pwd = pPassword;
                Utid = @pUserTipo;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Excluir Usuário 
        public void _excluir(Int32 @pCodUser)
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