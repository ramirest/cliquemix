using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Anunciante
{
    public class cl_ramo_atividade
    {
        #region _Atributos 
            private Int32 _raid;         //Codigo do ramo de atividade
            private string _descricao;   // descrição do ramo de atividade
        #endregion

        #region _Propriedades 
        public Int32 Raid
        {
            get { return _raid; }
        }

        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }
	    #endregion
            
        #region _Métodos 

        #region _Método Novo Ramo de Atividade 
        public void _novo(string @pDescricao)
        {
            try
            {
                Descricao = pDescricao;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Editar Ramo de Atividade 
        public void _editar(Int32 @pCodRamoAtiv, string @pDescricao)
        {
            try
            {
                //where _raid == CodRamoAtiv
                Descricao = pDescricao;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Excluir Ramo de Atividade 
        public void _excluir(Int32 @pCodRamoAtiv)
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
        public cl_ramo_atividade() // Contrutor Padrão
        {
            _raid = 0;
            _descricao = string.Empty;
        }
        #endregion

        #region _Destrutores
        public ~cl_ramo_atividade() // Destrutor Padrão
        {
            _raid = 0;
            _descricao = string.Empty;
        }
        #endregion        



    }
}