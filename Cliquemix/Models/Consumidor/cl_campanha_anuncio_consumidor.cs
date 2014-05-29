using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Consumidor
{
    //Tabela relacionada ao vínculo Anúncio x Consumidor
    public class cl_campanha_anuncio_consumidor
    {
        #region _Atributos 
        private Int32 _cacid; //Código da campanha anúncio consumidor
        private Int32 _cid; //Código do consumidor
        private Int32 _caid; //Código da campanha anúncio
        private Int32 _contCliqueConsu; //Quantidade de cliques que o consumidor já consumiu
        #endregion

        #region _Propriedades 
        public Int32 Cacid
        {
            get { return _cacid; }
        }
        public Int32 Cid
        {
            get { return _cid; }
            set { _cid = value; }
        }
        public Int32 Caid
        {
            get { return _caid; }
            set { _caid = value; }
        }
        public Int32 Count_Cliques_Consumidor
        {
            get { return _contCliqueConsu; }
            set { _contCliqueConsu = value; }
        }
        #endregion

        #region _Métodos

        #region _Método Novo Campanha Anúncio Consumidor 
        public void _novo(Int32 @pCodConsumidor, Int32 @pCodCampanhaAnuncio)
        {
            try
            {
                Cid = pCodConsumidor;
                Caid = pCodCampanhaAnuncio;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Editar Campanha Anúncio Consumidor 
        public void _editar(Int32 @pCodCampAnunConsu, Int32 @pCodConsumidor, Int32 @pCodCampanhaAnuncio)
        {
            try
            {
                //where cacid == pCodCampAnunConsu
                Cid = pCodConsumidor;
                Caid = pCodCampanhaAnuncio;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Excluir Campanha Anúncio Consumidor 
        public void _excluir(Int32 @codCampAnunConsu)
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
        public cl_campanha_anuncio_consumidor() // Contrutor Padrão
        {
            _cacid = 0;
            _caid = 0;
            _cid = 0;
            _contCliqueConsu = 0;
        }
        #endregion

        #region _Destrutores 
        public ~cl_campanha_anuncio_consumidor() // Destrutor Padrão
        {
            _cacid = 0;
            _caid = 0;
            _cid = 0;
            _contCliqueConsu = 0;
        }        
        #endregion
    }
}