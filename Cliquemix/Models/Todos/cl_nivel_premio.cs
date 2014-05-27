using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Todos
{
    public class cl_nivel_premio
    {
        #region _Atributos 
        private Int32 cod_premio;
        private string titulo_nivel_premio;
        private string ds_Premio;
        private string premio_img;
        #endregion

        #region _Propriedades 
        public int CodPremio
        {
            get { return cod_premio; }
            set { cod_premio = value; }
        }

        public string TituloNivelPremio
        {
            get { return titulo_nivel_premio; }
            set { titulo_nivel_premio = value; }
        }

        public string DescPremio
        {
            get { return ds_Premio; }
            set { ds_Premio = value; }
        }

        public string PremioImg
        {
            get { return premio_img; }
            set { premio_img = value; }
        }
        #endregion
    }
}