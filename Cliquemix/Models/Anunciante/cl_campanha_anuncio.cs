using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Anunciante
{
    public class cl_campanha_anuncio
    {
        #region _Atributos 
        private Int32 _caid;     //Código da  campanha anúncio
        private Int32 _aid;      //codigo do anuncio;
        private Int32 _cid;      //codigo da campanha;        
        private Int32 _casid;    //codigo do status do anuncio;
        private Int32 _qtCliqueMax; //Quantidade de cliques máximo do consumidor;
        #endregion

        #region _Propriedades
        public int Caid
        {
            get { return _caid; }
        }
        public int Aid
        {
            get { return _aid; }
            set { _aid = value; }
        }
        public int Cid
        {
            get { return _cid; }
            set { _cid = value; }
        }
        public int Casid
        {
            get { return _casid; }
            set { _casid = value; }
        }
        public int Qtde_Cliques_Max
        {
            get { return _qtCliqueMax; }
            set { _qtCliqueMax = value; }
        }
        #endregion

        #region _Métodos

        #region _Método Nova Campanha Anúncio
        public void _novo(Int32 @PCodAnuncio, Int32 @PCodCampanha, Int32 @PCodStatusAnuncio, Int32 @PQtdeCliquesMax)
        {
            try
            {
                Aid = PCodAnuncio;
                Cid = PCodCampanha;
                Casid = PCodStatusAnuncio;
                Qtde_Cliques_Max = PQtdeCliquesMax;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Editar Campanha Anúncio
        public void _editar(Int32 @PCodCampAnuncio, Int32 @PCodAnuncio, Int32 @PCodCampanha, Int32 @PCodStatusAnuncio, Int32 @PQtdeCliquesMax)
        {
            try
            {
                //where caid = PCodCampAnuncio
                Aid = PCodAnuncio;
                Cid = PCodCampanha;
                Casid = PCodStatusAnuncio;
                Qtde_Cliques_Max = PQtdeCliquesMax;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Excluir Campanha/Anúncio
        public void _excluir(Int32 @PCodCampAnuncio)
        {
            try
            {
                //Delete SQL where caid = @PCodCampAnuncio
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #endregion

        #region _Construtores
        public cl_campanha_anuncio() // Contrutor Padrão
        {
            _caid = 0;
            _aid = 0;
            _cid = 0;
            _casid = 0;
            _qtCliqueMax = 0;
        }
        #endregion

        #region _Construtores
        public ~cl_campanha_anuncio() // Destrutor Padrão
        {
            _caid = 0;
            _aid = 0;
            _cid = 0;
            _casid = 0;
            _qtCliqueMax = 0;
        }
        #endregion
    }
}