using System.Data.Odbc;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Cliquemix.Database
{
    public class connMySQL:DbConfiguration
    {
        #region _Atributos 
        private string _strConexaoDataBase;
        private OdbcConnection _conn;
        private string _strSQL;
        #endregion

        #region _Propriedades 

        public string StrSql
        {
            get { return _strSQL; }
            set { _strSQL = value; }
        }
        public string StrConexaoDataBase
        {
            get { return _strConexaoDataBase; }
        }
        public OdbcConnection Conn
        {
            get { return _conn; }
        }
        #endregion

        #region _Construtores, Destrutores e Métodos 
        public connMySQL()
        {
            _strConexaoDataBase = @"Dsn=cliquemix;uid=evsicove;pwd=evs1c0v3";
            _conn = new OdbcConnection(_strConexaoDataBase);
        }
        #endregion
    }
}