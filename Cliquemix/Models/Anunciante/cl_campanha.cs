using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Anunciante
{
    public class cl_campanha
    {
        #region _Atributos 
        private Int32 _cid; //Código da Campanha;
        private string _tituloCampanha; //Título da Campanha
        private DateTime _dtInicio; //Data de Início
        private DateTime _dtTermino; //Data de Término
        private Int32 _did; //Código Destaque
        private Int32 _scid; //Código do Status da Campanha
        #endregion

        #region _Propriedades 
        public int Cid
        {
            get { return _cid; }
        }

        public string TituloCampanha
        {
            get { return _tituloCampanha; }
            set { _tituloCampanha = value; }
        }

        public DateTime DtInicio
        {
            get { return _dtInicio; }
            set { _dtInicio = value; }
        }

        public DateTime DtTermino
        {
            get { return _dtTermino; }
            set { _dtTermino = value; }
        }

        public int Did
        {
            get { return _did; }
            set { _did = value; }
        }

        public int Scid
        {
            get { return _scid; }
            set { _scid = value; }
        }
        #endregion

        #region _Métodos 

        #region _Método Novo Campanha 
        public void _novo(string @pTituloCampanha, DateTime @pDataInicio, DateTime @pDataTermino, Int32 @pCodDestaque, Int32 @pCodStatusCampanha)
        {
            try
            {
                TituloCampanha = pTituloCampanha;
                DtInicio = pDataInicio;
                DtTermino = pDataTermino;
                Did = pCodDestaque;
                Scid = pCodStatusCampanha;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Editar Campanha 
        public void _novo(Int32 @pCodCampanha, string @pTituloCampanha, DateTime @pDataInicio, DateTime @pDataTermino, Int32 @pCodDestaque, Int32 @pCodStatusCampanha)
        {
            try
            {
                //where _cid == CodCampanha
                TituloCampanha = pTituloCampanha;
                DtInicio = pDataInicio;
                DtTermino = pDataTermino;
                Did = pCodDestaque;
                Scid = pCodStatusCampanha;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Excluir Anuncio 
        public void _excluir(Int32 @pCodCampanha)
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
        public cl_campanha() // Contrutor Padrão
        {
            _cid = 0;
            _tituloCampanha = string.Empty;
            _dtInicio = DateTime.Now;
            _dtTermino = DateTime.Now;
            _did = 0;
            _scid = 0;
        }
        #endregion

        #region _Destrutores 
        public ~cl_campanha() // Destrutor Padrão
        {
            _cid = 0;
            _tituloCampanha = string.Empty;
            _dtInicio = DateTime.Now;
            _dtTermino = DateTime.Now;
            _did = 0;
            _scid = 0;
        }
        #endregion
    }
}