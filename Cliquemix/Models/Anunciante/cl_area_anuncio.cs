using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Anunciante
{
    public class cl_area_anuncio
    {
        #region _Atributos 
        private Int32 _aaid; //Código da área do anúncio
        private string _tituloAreaAnuncio; //Título da área do anúncio
        #endregion

        #region _Propriedades 
        public int Aaid
        {
            get { return _aaid; }
        }

        public string TituloAreaAnuncio
        {
            get { return _tituloAreaAnuncio; }
            set { _tituloAreaAnuncio = value; }
        }
        #endregion

        #region _Métodos

        #region _Método Nova Área do Anúncio
        public void _novo(string @PTituloAreaAnun)
        {
            try
            {
                TituloAreaAnuncio = PTituloAreaAnun;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Editar Área Anúncio
        public void _editar(Int32 @PCodAreaAnuncio, string @PTituloAreaAnuncio)
        {
            try
            {
                //where aaid = PCodAreaAnuncio
                TituloAreaAnuncio = PTituloAreaAnuncio;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Excluir Área Anúncio
        public void _excluir(Int32 @PCodAreaAnuncio)
        {
            try
            {
                //Delete SQL where aaid = PCodAreaAnuncio
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #endregion

        #region _Construtores
        public cl_area_anuncio() // Contrutor Padrão
        {
            _aaid = 0;
            _tituloAreaAnuncio = string.Empty;
        }
        #endregion

        #region _Destrutores
        public ~cl_area_anuncio() // Destrutor Padrão
        {
            _aaid = 0;
            _tituloAreaAnuncio = string.Empty;
        }
        #endregion
    }
}