using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Cliquemix.Models
{
    public class PermissaoProvider : RoleProvider
    {

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            var db = new ApplicationDbContext();

            var usu = db.tbUsers.FirstOrDefault(u => u.username == username);

            //No teoricamente improvável caso o usuário não existir
            if (usu == null)
                return new string[] { };

            //Lista de permissões do usuário
            List<String> permissoes = usu.tbUsersPermissao.Select(p => p.tbPermissao.dsPermissao).ToList();

            return permissoes.ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

    }

    public class PermissoesFiltro : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            //Caso o usuário não foi autorizado, ele será enviado a página /Home/Negado
            if (filterContext.Result is HttpUnauthorizedResult && filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.HttpContext.Response.Redirect("/Account/Login");
            }
        }
    }
}