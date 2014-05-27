using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Administrador
{
    public class cl_banner_img
    {
        #region _Atributos 

        private Int32 _imgid;
        private Int32 _bid ;
        private float _ativo ;
        private string _img_banner;

        #endregion

        #region _Propriedades

        public int Imgid
        {
            get { return _imgid; }
            set { _imgid = value; }
        }

        public int Bid
        {
            get { return _bid; }
            set { _bid = value; }
        }

        public float Ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }

        public string ImgBanner
        {
            get { return _img_banner; }
            set { _img_banner = value; }
        }
        #endregion

    }
}