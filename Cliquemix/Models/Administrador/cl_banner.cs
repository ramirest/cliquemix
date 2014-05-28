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
        private string _titulo_banner;       // titulo do banner;

        #endregion
        //
        #region _Propriedades 

        public int Bid
        {
            get { return _bid; }
            set { _bid = value; }
        }

        public string TituloBanner
        {
            get { return _titulo_banner; }
            set { _titulo_banner = value; }
        }

        #endregion

        #region _Métodos 

            #region _Metodo Novo Banner 
        public void _novo(Int32 @Pbid, string @Ptitulo_banner)
        {
            try
            {
                Bid = Pbid;
                TituloBanner = Ptitulo_banner;
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
            #endregion

            #region _Metodo Editar Banner

        public void _editar(Int32 @Pbid, string @Ptitulo_banner)
        {
            try
            {
                Bid = Pbid;
                TituloBanner = Ptitulo_banner;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
            #endregion

            #region _Metodo Excluir Banner

        public void _excluir(Int32 @CdBanner)
        {
            //Delete SQL
        }

            #endregion

        #endregion

        #region _Construtores 

            public cl_banner()
            {
                _bid = 0;
                _titulo_banner = string.Empty;
            }
        #endregion

        #region _ Destrutores 
            public ~cl_banner()
            {
                _bid = 0;
                _titulo_banner = string.Empty;
            }
        #endregion


    }
}