using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Anunciante
{
    public class cl_destaque
    {
        #region _Atributos 
        private Int32 _did; //Código do destaque do anúncio
        private string _tituloDestaque; //Título do destaque
        private float _qtCredito; //Qtde de crédito para o destaque
        private float _tmpEspera; //Tempo mínimo de espera
        private string _durCampanha; //Tipo de duração da campanha (horas, dias, semanas, meses, etc.)
        private float _qtDurCampanha; //Qtde de duração da campanha pelo tipo acima
        private string _dsDestaque; //Descrição do destaque
        private string _imgDestaque; //Caminho da imagem
        #endregion

        #region _Propriedades 
        public int Did
        {
            get { return _did; }
        }

        public string TituloDestaque
        {
            get { return _tituloDestaque; }
            set { _tituloDestaque = value; }
        }

        public float QtCredito
        {
            get { return _qtCredito; }
            set { _qtCredito = value; }
        }

        public float TmpEspera
        {
            get { return _tmpEspera; }
            set { _tmpEspera = value; }
        }

        public string DurCampanha
        {
            get { return _durCampanha; }
            set { _durCampanha = value; }
        }

        public float QtDurCampanha
        {
            get { return _qtDurCampanha; }
            set { _qtDurCampanha = value; }
        }

        public string DsDestaque
        {
            get { return _dsDestaque; }
            set { _dsDestaque = value; }
        }

        public string ImgDestaque
        {
            get { return _imgDestaque; }
            set { _imgDestaque = value; }
        }
        #endregion

        #region _Métodos 

        #region _Método Novo Destaque 
        public void _novo(string @pTituloDestaque, float @pQtdeCredito, float @pTmpEspera, string @pDurCampanha, float @pQtDurCampanha, 
            string @pDsDestaque, string @pCaminhoImgDestaque)
        {
            try
            {
                TituloDestaque = pTituloDestaque;
                QtCredito = pQtdeCredito;
                TmpEspera = pTmpEspera;
                DurCampanha = pDurCampanha;
                QtDurCampanha = pQtDurCampanha;
                DsDestaque = pDsDestaque;
                ImgDestaque = @pCaminhoImgDestaque;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Editar Destaque 
        public void _editar(Int32 @pCodDestaqueAnuncio, string @pTituloDestaque, float @pQtdeCredito, float @pTmpEspera, string @pDurCampanha, float @pQtDurCampanha, 
            string @pDsDestaque, string @pCaminhoImgDestaque)
        {
            try
            {
                //where _did == CodDestaqueAnuncio
                TituloDestaque = pTituloDestaque;
                QtCredito = pQtdeCredito;
                TmpEspera = pTmpEspera;
                DurCampanha = pDurCampanha;
                QtDurCampanha = pQtDurCampanha;
                DsDestaque = pDsDestaque;
                ImgDestaque = @pCaminhoImgDestaque;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Excluir Destaque 
        public void _excluir(Int32 @pCodDestaqueAnuncio)
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
        public cl_destaque() // Contrutor Padrão
        {
            _did = 0;
            _tituloDestaque = string.Empty;
            _qtCredito = 0;
            _tmpEspera = 0;
            _durCampanha = string.Empty;
            _qtDurCampanha = 0;
            _dsDestaque = string.Empty;
            _imgDestaque = string.Empty;
        }
        #endregion

        #region _Destrutores 
        ~cl_destaque() // Contrutor Padrão
        {
            _did = 0;
            _tituloDestaque = string.Empty;
            _qtCredito = 0;
            _tmpEspera = 0;
            _durCampanha = string.Empty;
            _qtDurCampanha = 0;
            _dsDestaque = string.Empty;
            _imgDestaque = string.Empty;
        }
        #endregion
    }
}