using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Administrador
{
    public class cl_banner
    {
        #region _Atributos

        private Int32 _bid;                  //codigo do banner;
        private string _tituloBanner;       // titulo do banner;

        #endregion
        
        #region _Propriedades 

        public int Bid
        {
            get { return _bid; }
        }

        public string TituloBanner
        {
            get { return _tituloBanner; }
            set { _tituloBanner = value; }
        }

        #endregion
        
        #region _Métodos 

        #region _Metodo Novo Banner 
        public void _novo(string @ptituloBanner)
        {
            try
            {
                TituloBanner = ptituloBanner;
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
            #endregion

        #region _Metodo Editar Banner

        public void _editar(Int32 @pBid, string @pTituloBanner)
        {
            try
            {
                //where _bid == Pbid;
                TituloBanner = @pTituloBanner;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
            #endregion

        #region _Metodo Excluir Banner

        public void _excluir(Int32 @pCodBanner)
        {
            //Delete SQL
        }

            #endregion

        #endregion

        #region _Construtores 

            public cl_banner()
            {
                _bid = 0;
                _tituloBanner = string.Empty;
            }
        #endregion

        #region _ Destrutores 
            public ~cl_banner()
            {
                _bid = 0;
                _tituloBanner = string.Empty;
            }
        #endregion


    }
}