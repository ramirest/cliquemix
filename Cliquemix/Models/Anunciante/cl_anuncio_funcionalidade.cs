using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Anunciante
{
    public class cl_anuncio_funcionalidade
    {
        #region _Atributos 
        private Int32 _afid; //Código do anúncio funcionalidade (PK)
        private Int32 _aid; //Código do anúncio (FK)
        private Int32 _fid; //Código da funcionalidade (FK)
        #endregion

        #region _Propriedades 
        public int Afid
        {
            get { return _afid; }
        }

        public int Aid
        {
            get { return _aid; }
            set { _aid = value; }
        }

        public int Fid
        {
            get { return _fid; }
            set { _fid = value; }
        }
        #endregion

        #region _Métodos 

        #region _Metodo Novo Anúncio Funcionalidade 
        public void _novo(Int32 @pCodAnuncio, Int32 @pCodFuncionalidade)
        {
            try
            {
                Aid = pCodAnuncio;
                Fid = pCodFuncionalidade;
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
        #endregion

        #region _Metodo Editar Anúncio Funcionalidade 
        public void _editar(Int32 @pCodAnunFuncio, Int32 @pCodAnuncio, Int32 @pCodFuncionalidade)
        {
            try
            {
                //where _afid == pCodAnunFuncio
                Aid = pCodAnuncio;
                Fid = pCodFuncionalidade;
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
        #endregion

        #region _Metodo Excluir Anúncio Funcionalidade 
        public void _excluir(Int32 @pCodAnunFuncio)
        {
            //Delete SQL
        }
        #endregion

        #endregion

        #region _Construtores 
        public cl_anuncio_funcionalidade()
        {
            _afid = 0;
            _aid = 0;
            _fid = 0;
        }
        #endregion

        #region _ Destrutores 
         ~cl_anuncio_funcionalidade()
        {
            _afid = 0;
            _aid = 0;
            _fid = 0;
        }
        #endregion
    }
}