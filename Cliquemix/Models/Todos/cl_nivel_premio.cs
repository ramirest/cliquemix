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
    }
}