using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.ComponentModel.DataAnnotations;
using Cliquemix.Models;

namespace Cliquemix.Models
{
    public class LoginModel
    {
        public int CodUsuario { get; set; }
        public int CodTipoUsuario { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool Remember { get; set; }
        private ApplicationDbContext db = new ApplicationDbContext();

        #region "Método UserIsValid {Validação login de usuário}"
        public bool UserIsValid(string user, string pwd)
        {
            using (EntityConnection conn = new EntityConnection("name=cliquemixEntities"))
            {
                conn.Open();
                string _QrySQL = @"SELECT tbUsers.uid, tbUsers.username, tbUsers.utid
                    FROM cliquemixEntities.tbUsers 
                    WHERE tbUsers.username = @usu AND tbUsers.pwd = @senha";

                EntityCommand cmd = new EntityCommand(_QrySQL, conn);

                // Create two parameters and add them to 
                // the EntityCommand's Parameters collection 
                EntityParameter param1 = new EntityParameter();
                param1.ParameterName = "usu";
                param1.Value = user;

                EntityParameter param2 = new EntityParameter();
                param2.ParameterName = "senha";
                param2.Value = pwd;

                cmd.Parameters.Add(param1);
                cmd.Parameters.Add(param2);

                using (DbDataReader rdr = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                {
                    // Iterate through the collection of Contact items.
                    while (rdr.Read())
                    {
                        CodUsuario = Convert.ToInt32(rdr["uid"].ToString());
                        Username = rdr["username"].ToString();
                        CodTipoUsuario = Convert.ToInt32(rdr["utid"].ToString());
                        this._salvarLog();
                        return true;
                    }
                    return false;
                }                
            }
        }
        #endregion

        public void _salvarLog()
        {
            tbUsersLogAcesso logAcesso = new tbUsersLogAcesso();
            logAcesso.uid = CodUsuario;
            logAcesso.dataHoraLog = DateTime.Now;
            logAcesso.timeOutLog = 60;

            db.TbUsersLogAcessos.Add(logAcesso);
            db.SaveChanges();
        }        

    }
}