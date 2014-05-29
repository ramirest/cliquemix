using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Administrador
{
    public class cl_banner_img
    {
        #region _Atributos 
        private Int32 _imgid;
        private Int32 _bid ;
        private Boolean _ativo ;
        private string _imgBanner;
        #endregion

        #region _Propriedades 

        public int Imgid
        {
            get { return _imgid; }
        }

        public int Bid
        {
            get { return _bid; }
            set { _bid = value; }
        }

        public Boolean Ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }

        public string ImgBanner
        {
            get { return _imgBanner; }
            set { _imgBanner = value; }
        }
        #endregion

        #region _Métodos 

        #region _Metodo Novo Imagem do Banner 
        public void _novo(Int32 @pCodBanner, Boolean @pAtivo, string @pImgBanner)
        {
            try
            {
                Bid = pCodBanner;
                Ativo = pAtivo;
                ImgBanner = pImgBanner;
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
        #endregion

        #region _Metodo Editar Imagem do Banner 
        public void _editar(Int32 @pCodImgBanner, Int32 @pCodBanner, Boolean @pAtivo, string @pImgBanner)
        {
            try
            {
                //where imgid = pCodImgBanner
                Bid = pCodBanner;
                Ativo = pAtivo;
                ImgBanner = pImgBanner;
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
        #endregion

        #region _Metodo Excluir Imagem do Banner
        public void _excluir(Int32 @pCodImgBanner)
        {
            //Delete SQL
        }
        #endregion

        #endregion

        #region _Construtores 
        public cl_banner_img()
        {
            _imgid = 0;
            _bid = 0;
            _ativo = false;
            _imgBanner = string.Empty;
        }
        #endregion

        #region _ Destrutores 
        ~cl_banner_img()
        {
            _imgid = 0;
            _bid = 0;
            _ativo = false;
            _imgBanner = string.Empty;
        }
        #endregion
    }
}