using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;

namespace Cliquemix.Models.Database
{

    public class database
    {
        public OdbcConnection conn;
        public string caminho;
        
        public database ()
        {
            caminho = "Driver={MySQL ODBC 3.51 Driver}; " +
                      "Server=aa15cdlwzjztwet.cwc8q91bdfgt.sa-east-1.rds.amazonaws.com; " +
                      "Database=cliquemix; UID=evsicove; PWD=evs1c0v3;";
            conn = new OdbcConnection(caminho);
        }

    }

}