using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Todos
{
    public class cl_nivel_premio
    {
        #region _Atributos 
        private Int32 _npid; //Código do nível do prêmio
        private string _titulo_nivel_premio; //Título do nível de prêmio
        private string _dspremio; //Descrição do nível do prêmio
        private string _premio_img; //Imagem do nível de prêmio
        #endregion

        #region _Propriedades 
        public int npid
        {
            get { return _npid; }
            set { _npid = value; }
        }

        public string TituloNivelPremio
        {
            get { return _titulo_nivel_premio; }
            set { _titulo_nivel_premio = value; }
        }

        public string DsPremio
        {
            get { return _dspremio; }
            set { _dspremio = value; }
        }

        public string PremioImg
        {
            get { return _premio_img; }
            set { _premio_img = value; }
        }
        #endregion
    }
}