using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Cliquemix.Models;

namespace Cliquemix.Repositories
{
    public class UsuarioRepositorio
    {
        #region _AutenticarUsuario 
        /// <summary>
        /// Método estático chamado AutenticarUsuario que receberá como parâmetro duas strings, 
        /// uma é o Login e a outra é a Senha
        /// </summary>
        /// <param name="login"></param>
        /// <param name="senha"></param>
        public static bool AutenticarUsuario(string login, string senha)
        {
            ApplicationDbContext entities = new ApplicationDbContext();

            var Query = (from u in entities.tbUsers
                         where u.username == login &&
                               u.pwd == senha
                         select u).SingleOrDefault();

            //Usuário não existe ou a senha está incorreta
            if (Query == null)
                return false;

            //Irá setar um cookie encriptado com o Login do usuário autenticado
            FormsAuthentication.SetAuthCookie(Query.username, false);

            return true;
        }
        #endregion

        #region _GetUsuarioLogado 
        /// <summary>
        /// Método estático que irá retornar o Usuario que está logado, 
        /// caso existir um usuário logado é claro.
        /// </summary>
        /// <returns></returns>
        public static tbUsers GetUsuarioLogado()
        {
            string _login = HttpContext.Current.User.Identity.Name;

            //Não existe usuário logado, retornamos null
            if (_login == "")
            {
                return null;
            }
            else
            {
                cliquemixEntities1 entities = new cliquemixEntities1();
                
                //Buscamos no banco de dados o usuário que está logado
                tbUsers user = (from u in entities.tbUsers
                                where u.username == _login
                                select u).SingleOrDefault();

                return user;
            }

        }
        #endregion

        #region _Deslogar 
        /// <summary>
        /// Método que irá desfazer a autenticação, ou seja, vai "Deslogar".
        /// </summary>
        /// <returns></returns>
        public static void Deslogar()
        {
            FormsAuthentication.SignOut();
        }
        #endregion
    }
}