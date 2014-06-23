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
    public partial class Endereco
    {
        public string cep { get; set; }
        public string dsPais { get; set; }
        public string dsEstado { get; set; }
        public string dsCidade { get; set; }
        public string dsBairro { get; set; }
        public string dsLogradouro { get; set; }

        private ApplicationDbContext db = new ApplicationDbContext();

        #region _Método Localizar Endereço {Validação CEP}
        public void localizarEnd(string cep)
        {
            using (EntityConnection conn = new EntityConnection("name=cliquemixEntities"))
            {
                conn.Open();
                string _QrySQL = @"SELECT tbCep.dsPais, tbCep.dsEstado, tbCep.dsCidade, tbCep.dsBairro, tbCep.dsLogradouro
                    FROM cliquemixEntities.tbCep 
                    WHERE tbCep.cep = @pCep";

                EntityCommand cmd = new EntityCommand(_QrySQL, conn);
                //
                // Create two parameters and add them to 
                // the EntityCommand's Parameters collection 
                EntityParameter param1 = new EntityParameter();
                param1.ParameterName = "pCep";
                param1.Value = cep;

                cmd.Parameters.Add(param1);

                using (DbDataReader rdr = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                {
                    // Iterate through the collection of Contact items.
                    while (rdr.Read())
                    {
                        this.dsPais = rdr["dsPais"].ToString();
                        this.dsEstado = rdr["dsEstado"].ToString();
                        this.dsCidade = rdr["dsCidade"].ToString();
                        this.dsBairro = rdr["dsBairro"].ToString();
                        this.dsLogradouro = rdr["dsLogradouro"].ToString();
                    }
                }
            }
        }
        #endregion

    }
}