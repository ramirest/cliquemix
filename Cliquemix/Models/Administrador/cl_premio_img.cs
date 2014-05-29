using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Todos
{
    public class cl_premio_img
    {
        #region _Atributos 
        private Int32 _piid;//Código da imagem do prêmio
        private Int32 _pid; //Código do prêmio
        private string _premioImg; //Caminho da imagem do prêmio
        #endregion

        #region _Propriedades 
        public int Piid
        {
            get { return _piid; }
        }

        public int Pid
        {
            get { return _pid; }
            set { _pid = value; }
        }

        public string PremioImg
        {
            get { return _premioImg; }
            set { _premioImg = value; }
        }
        #endregion

        #region _Métodos 

        #region _Método Novo Prêmio Imagem 
        public void _novo(Int32 @pCodPremio, string @pCaminhoImg)
        {
            try
            {
                Pid = pCodPremio;
                PremioImg = @pCaminhoImg;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Editar Prêmio Imagem 
        public void _editar(Int32 @pCodPremioImg, Int32 @pCodPremio, string @pCaminhoImg)
        {
            try
            {
                //where piid
                Pid = pCodPremio;
                PremioImg = pCaminhoImg;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Excluir Prêmio Imagem 
        public void _excluir(Int32 @codPremioImg)
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
        public cl_premio_img() // Contrutor Padrão
        {
            _piid = 0;
            _pid = 0;
            _premioImg = string.Empty;
        }
        #endregion

        #region _Destrutores 
        public ~cl_premio_img() // Destrutor Padrão
        {
            _piid = 0;
            _pid = 0;
            _premioImg = string.Empty;
        }        
        #endregion
    }
}