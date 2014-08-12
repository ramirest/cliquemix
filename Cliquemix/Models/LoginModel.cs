using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.ComponentModel.DataAnnotations;
using Cliquemix.Models;
using Microsoft.Ajax.Utilities;

namespace Cliquemix.Models
{
    public class LoginModel
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        public int CodUsuario { get; set; }
        public int CodTipoUsuario { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool Remember { get; set; }

        #region "Método UserIsValid {Validação login de usuário}"
        public bool UserIsValid(string user, string pwd)
        {
            var senha = ProcFunc.CryptographyPass(pwd);
            var u = (from usu in db.tbUsers 
                     where usu.username == user  && usu.pwd == senha
                     select usu).ToList();
            if (u.Count > 0)
            {
                CodUsuario = u.First().uid;
                Username = u.First().username;
                CodTipoUsuario = u.First().utid;
                return true;
            }
            return false;
        }
        #endregion

        public static void _salvarLog(int pCodUsuario, int pTimeOut, int pIdSessao, string pSessao)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            tbUsersLogAcesso logAcesso = new tbUsersLogAcesso();
            logAcesso.uid = pCodUsuario;
            logAcesso.dataHoraLog = DateTime.Now;
            logAcesso.timeOutLog = pTimeOut;
            logAcesso.dataHoraExpiracao = logAcesso.dataHoraLog.AddMinutes(pTimeOut);
            logAcesso.idSessao = pIdSessao;
            logAcesso.sessaoExpirada = 0;
            logAcesso.nomeSessao = pSessao;
            db.tbUsersLogAcesso.Add(logAcesso);
            db.SaveChanges();
        }        

    }
}