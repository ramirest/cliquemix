using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Todos
{
    public class cl_img_premio
    {
        #region _Atributos 
        private Int32 _imgid;//Código da imagem do prêmio
        private Int32 _pid; //Código do prêmio
        private string _premio_img; //Caminho da imagem do prêmio
        #endregion

        #region _Propriedades 
        public int Imgid
        {
            get { return _imgid; }
            set { _imgid = value; }
        }

        public int Pid
        {
            get { return _pid; }
            set { _pid = value; }
        }

        public string PremioImg
        {
            get { return _premio_img; }
            set { _premio_img = value; }
        }
        #endregion
    }
}