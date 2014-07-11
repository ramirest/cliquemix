using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace Cliquemix.Models
{
    public static class Endereco
    {
        #region "Váriaveis"
        private static int _cepid;
        private static string _cep;
        private static string _pais;
        private static string _uf;
        private static string _cidade;
        private static string _bairro;
        private static string _tipo_logradouro;
        private static string _logradouro;
        private static string _resultado;
        private static string _resultato_txt;
        #endregion

        #region "Propiedades"
        public static int Cepid
        {
            get { return _cepid; }
            set { _cepid = value; }
        }
        public static string Cep
        {
            get { return _cep; }
            set { _cep = value; }
        }
        public static string UF
        {
            get { return _uf; }
            set { _uf = value; }
        }
        public static string Pais
        {
            get { return _pais; }
            set { _pais = value; }
        }
        public static string Cidade
        {
            get { return _cidade; }
            set { _cidade = value; }
        }
        public static string Bairro
        {
            get { return _bairro; }
            set { _bairro = value; }
        }
        public static string TipoLogradouro
        {
            get { return _tipo_logradouro; }
            set { _tipo_logradouro = value; }
        }
        public static string Logradouro
        {
            get { return _logradouro; }
            set { _logradouro = value; }
        }
        public static string Resultado
        {
            get { return _resultado; }
            set { _resultado = value; }
        }
        public static string ResultadoTXT
        {
            get { return _resultato_txt; }
            set { _resultato_txt = value; }
        }
        #endregion

        #region "Construtor"

        /// <summary>  
        /// WebService para Busca de CEP  
        ///  </summary>  
        /// <param  name="CEP"></param>  
        static Endereco()
        {
            Cep = "";
            UF = "";
            Pais = "";
            Cidade = "";
            Bairro = "";
            TipoLogradouro = "";
            Logradouro = "";
            Resultado = "0";
            ResultadoTXT = "CEP não encontrado";
        }
        #endregion

        #region "Pesquisar CEP WebService"
        public static void PesquisarWeb(string CEP)
        {
            UF = "";
            Cidade = "";
            Bairro = "";
            TipoLogradouro = "";
            Logradouro = "";
            Resultado = "0";
            ResultadoTXT = "CEP não encontrado";

            //Cria um DataSet  baseado no retorno do XML  
            DataSet ds = new DataSet();
            ds.ReadXml("http://cep.republicavirtual.com.br/web_cep.php?cep=" + CEP.Replace("-", "").Trim() + "&formato=xml");

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Resultado = ds.Tables[0].Rows[0]["resultado"].ToString();
                    switch (_resultado)
                    {
                        case "1":
                            UF = ds.Tables[0].Rows[0]["uf"].ToString().Trim();
                            Cidade = ds.Tables[0].Rows[0]["cidade"].ToString().Trim();
                            Bairro = ds.Tables[0].Rows[0]["bairro"].ToString().Trim();
                            TipoLogradouro = ds.Tables[0].Rows[0]["tipo_logradouro"].ToString().Trim();
                            Logradouro = ds.Tables[0].Rows[0]["logradouro"].ToString().Trim();
                            ResultadoTXT = "CEP completo";
                            break;
                        case "2":
                            UF = ds.Tables[0].Rows[0]["uf"].ToString().Trim();
                            Cidade = ds.Tables[0].Rows[0]["cidade"].ToString().Trim();
                            Bairro = "";
                            TipoLogradouro = "";
                            Logradouro = "";
                            ResultadoTXT = "CEP  único";
                            break;
                        default:
                            UF = "";
                            Cidade = "";
                            Bairro = "";
                            TipoLogradouro = "";
                            Logradouro = "";
                            ResultadoTXT = "CEP não  encontrado";
                            break;
                    }
                }
            }
            //Exemplo do retorno da  WEB  
            //<?xml version="1.0"  encoding="iso-8859-1"?>  
            //<webservicecep>  
            //<uf>RS</uf>  
            //<cidade>Porto  Alegre</cidade>  
            //<bairro>Passo  D'Areia</bairro>  
            //<tipo_logradouro>Avenida</tipo_logradouro>  
            //<logradouro>Assis Brasil</logradouro>  
            //<resultado>1</resultado>  
            //<resultado_txt>sucesso - cep  completo</resultado_txt>  
            //</webservicecep>  
        }
        #endregion

        #region "Pesquisar CEP Banco de Dados Local"
        public static void PesquisarLocal(string CEP)
        {
            using (EntityConnection conn = new EntityConnection("name=cliquemixEntities"))
            {
                conn.Open();
                string _QrySQL = @"SELECT tbCep.cepid, tbCep.cep, tbCep.tipoLogradouro, tbCep.dsLogradouro, tbBairro.nomeBairro, tbCidade.nomeCidade, tbEstado.sgEstado, tbPais.nomePais
                                   FROM cliquemixEntities.tbCep 
                                        inner join cliquemixEntities.tbBairro on tbCep.baiid = tbBairro.baiid
		                                inner join cliquemixEntities.tbCidade on tbCep.cid = tbCidade.cid
                                        inner join cliquemixEntities.tbEstado on tbCep.eid = tbEstado.eid
                                        left join cliquemixEntities.tbPais on tbEstado.paid = tbPais.paid
                                   WHERE tbCep.cep like @pCep";

                EntityCommand cmd = new EntityCommand(_QrySQL, conn);

                // Create two parameters and add them to 
                // the EntityCommand's Parameters collection 
                EntityParameter param1 = new EntityParameter();
                param1.ParameterName = "pCep";
                param1.Value = CEP;

                cmd.Parameters.Add(param1);
                try
                {
                    using (DbDataReader rdr = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                    {
                        if (rdr.HasRows)
                        {
                            ResultadoTXT = "CEP completo";
                            // Iterate through the collection of Contact items.
                            while (rdr.Read())
                            {
                                Cepid = Convert.ToInt32(rdr["cepid"].ToString());
                                Cep = rdr["cep"].ToString();
                                TipoLogradouro = rdr["tipoLogradouro"].ToString();
                                Logradouro = rdr["dsLogradouro"].ToString();
                                Bairro = rdr["nomeBairro"].ToString();
                                Cidade = rdr["nomeCidade"].ToString();
                                UF = rdr["sgEstado"].ToString();
                                Pais = rdr["nomePais"].ToString();
                            }
                        }
                        else
                        {
                            retornaVazio();
                        }
                    }
                }
                catch (Exception)
                {
                    retornaVazio();
                    throw;
                }

            }
        }
        #endregion

        #region "Retorna vazio"
        public static void retornaVazio()
        {
            Pais = "";
            UF = "";
            Cidade = "";
            Bairro = "";
            TipoLogradouro = "";
            Logradouro = "";
            ResultadoTXT = "CEP não encontrado";
        }
        #endregion

    }
}