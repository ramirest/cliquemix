using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Todos
{
    public class cl_nivel_premio
    {
        #region _Atributos 
        private Int32 _cod_premio;
        private string _titulo_nivel_premio;
        private string _ds_Premio;
        private string _premio_img;
        #endregion

        #region _Propriedades 
        public int CodPremio
        {
            get { return _cod_premio; }
            set { _cod_premio = value; }
        }

        public string TituloNivelPremio
        {
            get { return _titulo_nivel_premio; }
            set { _titulo_nivel_premio = value; }
        }

        public string DescPremio
        {
            get { return _ds_Premio; }
            set { _ds_Premio = value; }
        }

        public string PremioImg
        {
            get { return _premio_img; }
            set { _premio_img = value; }
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
            _cod_premio = 0;
            _titulo_nivel_premio = string.Empty;
            _ds_Premio = string.Empty;
            _premio_img = string.Empty;
        }
        #endregion

        #region _Destrutores 
        public ~cl_nivel_premio() // Destrutor Padrão
        {
            _cod_premio = 0;
            _titulo_nivel_premio = string.Empty;
            _ds_Premio = string.Empty;
            _premio_img = string.Empty;
        }        
        #endregion

    }
}