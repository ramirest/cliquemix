using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.ComponentModel.DataAnnotations;
using Cliquemix.Models;

namespace Cliquemix.Models
{
    public partial class Endereco
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region _Váriavies 

        private string _uf;
        private string _cidade;
        private string _bairro;
        private string _tipo_lagradouro;
        private string _lagradouro;
        private string _resultado;
        private string _resultato_txt;
        public string _cep;

        #endregion

        #region _Propiedades 

        public string cep
        {
            get { return _cep; }
            set { _cep = value; }
        }
        public string UF
        {
            get { return _uf; }
        }

        public string Cidade
        {
            get { return _cidade; }
        }

        public string Bairro
        {
            get { return _bairro; }
        }

        public string TipoLagradouro
        {
            get { return _tipo_lagradouro; }
        }

        public string Lagradouro
        {
            get { return _lagradouro; }
        }

        public string Resultado
        {
            get { return _resultado; }
        }

        public string ResultadoTXT
        {
            get { return _resultato_txt; }
        }

        #endregion

        #region _Método Localizar Endereço {Validação CEP}

        public void localizarEnd(string cep)
        {
            using (EntityConnection conn = new EntityConnection("name=cliquemixEntities"))
            {
                conn.Open();
                string _QrySQL =
                    @"SELECT tbCep.dsPais, tbCep.dsEstado, tbCep.dsCidade, tbCep.dsBairro, tbCep.dsLogradouro
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
                        /*
                        this.dsPais = rdr["dsPais"].ToString();
                        this.dsEstado = rdr["dsEstado"].ToString();
                        this.dsCidade = rdr["dsCidade"].ToString();
                        this.dsBairro = rdr["dsBairro"].ToString();
                        this.dsLogradouro = rdr["dsLogradouro"].ToString();
                        */
                    }
                }
            }
        }

        #endregion


        public Endereco()
        {
            _uf = "";
            _cidade = "";
            _bairro = "";
            _tipo_lagradouro = "";
            _lagradouro = "";
            _resultado = "0";
            _resultato_txt = "CEP não encontrado";
            _cep = string.Empty;
        }

        public void pesquisar()
        {
            //Cria um DataSet  baseado no retorno do XML  
            DataSet ds = new DataSet();
            ds.ReadXml("http://cep.republicavirtual.com.br/web_cep.php?cep=" + cep.Replace ("-","").Trim()+ "&formato=xml");

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    _resultado = ds.Tables[0].Rows[0]["resultado"].ToString();
                    switch (_resultado)
                    {
                        case "1":
                            _uf = ds.Tables[0].Rows[0]["uf"].ToString().Trim();
                            _cidade = ds.Tables[0].Rows[0]["cidade"].ToString().Trim();
                            _bairro = ds.Tables[0].Rows[0]["bairro"].ToString().Trim();
                            _tipo_lagradouro = ds.Tables[0].Rows[0]["tipo_logradouro"].ToString().Trim();
                            _lagradouro = ds.Tables[0].Rows[0]["logradouro"].ToString().Trim();
                            _resultato_txt = "CEP completo";
                            break;
                        case "2":
                            _uf = ds.Tables[0].Rows[0]["uf"].ToString().Trim();
                            _cidade = ds.Tables[0].Rows[0]["cidade"].ToString().Trim();
                            _bairro = "";
                            _tipo_lagradouro = "";
                            _lagradouro = "";
                            _resultato_txt = "CEP  único";
                            break;
                        default:
                            _uf = "";
                            _cidade = "";
                            _bairro = "";
                            _tipo_lagradouro = "";
                            _lagradouro = "";
                            _resultato_txt = "CEP não  encontrado";
                            break;
                    }
                }

            }
        }
}
}