using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Anunciante
{
    public class cl_funcionalidade
    {
        #region _Atributos 
        private Int32 _fid; //codigo da funcionalidade
        private string _dsFuncionalidade; //Descrição da funcionalidade
        #endregion

        #region _Propriedades 

        public int Fid
        {
            get { return _fid; }
            set { _fid = value; }
        }

        public string Dsfuncionalidade
        {
            get { return _dsFuncionalidade; }
            set { _dsFuncionalidade = value; }
        }
        #endregion

        #region _Métodos 

        #region _Método Nova Funcionalidade 
        public void _novo(string @pDsFuncionalidade)
        {
            try
            {
                Dsfuncionalidade = pDsFuncionalidade;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Editar Funcionalidade 
        public void _editar(Int32 @pCodFuncionalidade, string @pDsFuncionalidade)
        {
            try
            {
                //where fid == CodFuncionalidade
                Dsfuncionalidade = pDsFuncionalidade;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Excluir Funcionalidade 
        public void _excluir(Int32 @pCodFuncionalidade)
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
        public cl_funcionalidade() // Contrutor Padrão
        {
            _fid = 0;
            _dsFuncionalidade = string.Empty;
        }
        #endregion

        #region _Destrutores 
        ~cl_funcionalidade() // Destrutor Padrão
        {
            _fid = 0;
            _dsFuncionalidade = string.Empty;
        }
        #endregion
    }
}