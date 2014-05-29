using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Anunciante
{
    public class cl_campanha_anuncio_status
    {
        #region _Atributos 
        private Int32 _casid;     //Código da  campanha anúncio
        private string _dsCamAnuStatus; //Descriçao do status da campanha anúncio;
        #endregion

        #region _Propriedades 
        public int Casid
        {
            get { return _casid; }
        }
        public string DsCamAnuStatus
        {
            get { return _dsCamAnuStatus; }
            set { _dsCamAnuStatus = value; }
        }
        #endregion

        #region _Métodos

        #region _Método Novo Status Campanha/Anúncio
        public void _novo(string @PDescStatusCampAnun)
        {
            try
            {
                DsCamAnuStatus = PDescStatusCampAnun;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Editar Status Campanha/Anúncio
        public void _editar(Int32 @PCodStatusCampAnun, string @PDescStatusCampAnun)
        {
            try
            {
                //where casid = PCodStatusCampAnun
                DsCamAnuStatus = PDescStatusCampAnun;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Excluir Status Campanha/Anúncio
        public void _excluir(Int32 @PCodStatusCampAnuncio)
        {
            try
            {
                //Delete SQL where casid = PCodStatusCampAnuncio
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #endregion

        #region _Construtores
        public cl_campanha_anuncio_status() // Contrutor Padrão
        {
            _casid = 0;
            _dsCamAnuStatus = string.Empty;
        }
        #endregion

        #region _Destrutores
        public ~cl_campanha_anuncio_status() // Destrutor Padrão
        {
            _casid = 0;
            _dsCamAnuStatus = string.Empty;
        }
        #endregion
    }
}