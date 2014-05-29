using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Todos
{
    public class cl_nivel_premio
    {
        #region _Atributos 
        private Int32 _codPremio;
        private string _tituloNivelPremio;
        private string _dsPremio;
        private string _premioImg;
        #endregion

        #region _Propriedades 
        public int CodPremio
        {
            get { return _codPremio; }
        }

        public string TituloNivelPremio
        {
            get { return _tituloNivelPremio; }
            set { _tituloNivelPremio = value; }
        }

        public string DescPremio
        {
            get { return _dsPremio; }
            set { _dsPremio = value; }
        }

        public string PremioImg
        {
            get { return _premioImg; }
            set { _premioImg = value; }
        }
        #endregion

        #region _Métodos 

        #region _Método Novo Nível Prêmio 
        public void _novo(string @PtituloNivelPremio, string @PdsPremio, string @PpremioImg)
        {
            try
            {
                TituloNivelPremio = PtituloNivelPremio;
                DescPremio = PdsPremio;
                PremioImg = PpremioImg;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Editar Nível de Prêmio
        public void _editar(string @PtituloNivelPremio, string @PdsPremio, string @PpremioImg)
        {
            try
            {
                TituloNivelPremio = PtituloNivelPremio;
                DescPremio = PdsPremio;
                PremioImg = PpremioImg;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Excluir Nível de Prêmio
        public void _excluir(Int32 @codNivelPremio)
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
        public cl_nivel_premio() // Contrutor Padrão
        {
            _codPremio = 0;
            _tituloNivelPremio = string.Empty;
            _dsPremio = string.Empty;
            _premioImg = string.Empty;
        }
        #endregion

        #region _Destrutores 
        ~cl_nivel_premio() // Destrutor Padrão
        {
            _codPremio = 0;
            _tituloNivelPremio = string.Empty;
            _dsPremio = string.Empty;
            _premioImg = string.Empty;
        }        
        #endregion
    }
}