using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Anunciante
{
    public class cl_campanha_anuncio_localizacao
    {
        #region _Atributos 
        private Int32 _calid; //Código da campanha anúncio localização
        private Int32 _caid; //Código campanha anúncio
        private Int32 _pid; //Código do país
        private Int32 _eid; //Código do estado
        private Int32 _cid; //Código da cidade
        #endregion

        #region _Propriedades 
        public int Calid
        {
            get { return _calid; }
        }
        public int Caid
        {
            get { return _caid; }
            set { _caid = value; }
        }
        public int Pid
        {
            get { return _pid; }
            set { _pid = value; }
        }
        public int Eid
        {
            get { return _eid; }
            set { _eid = value; }
        }
        public int Cid
        {
            get { return _cid; }
            set { _cid = value; }
        }
        #endregion

        #region _Métodos

        #region _Método Nova Localização da Campanha/Anúncio 
        public void _novo(Int32 @PCodCampAnuncio, Int32 @PCodPais, Int32 @PCodEstado, Int32 @PCodCidade)
        {
            try
            {
                Caid = PCodCampAnuncio;
                Pid = PCodPais;
                Eid = PCodEstado;
                Cid = PCodCidade;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Editar Localização da Campanha/Anúncio 
        public void _editar(Int32@PCodCampAnunLoc, Int32 @PCodCampAnuncio, Int32 @PCodPais, Int32 @PCodEstado, Int32 @PCodCidade)
        {
            try
            {
                //where Calid = PCodCampAnunLoc
                Caid = PCodCampAnuncio;
                Pid = PCodPais;
                Eid = PCodEstado;
                Cid = PCodCidade;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Excluir Localização da Campanha/Anúncio
        public void _excluir(Int32 @PCodCampAnunLoc)
        {
            try
            {
                //Delete SQL where Calid = PCodCampAnunLoc
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #endregion

        #region _Construtores
        public cl_campanha_anuncio_localizacao() // Contrutor Padrão
        {
            _calid = 0;
            _caid = 0;
            _pid = 0;
            _eid = 0;
            _cid = 0;
        }
        #endregion

        #region _Destrutores
        public ~cl_campanha_anuncio_localizacao() // Destrutor Padrão
        {
            _calid = 0;
            _caid = 0;
            _pid = 0;
            _eid = 0;
            _cid = 0;
        }
        #endregion
    }
}