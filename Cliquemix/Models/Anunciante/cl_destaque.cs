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
        private string _titulo_destaque; //Título do destaque
        private float _qtde_credito; //Qtde de crédito para o destaque
        private Int32 _tmp_espera; //Tempo mínimo de espera
        private string _duracao_campanha; //Tipo de duração da campanha (horas, dias, semanas, meses, etc.)
        private Int32 _qtde_duracao_campanha; //Qtde de duração da campanha pelo tipo acima
        private string _ds_destaque; //Descrição do destaque
        private string _img_destaque; //Caminho da imagem
        #endregion

        #region _Propriedades 
        public int Did
        {
            get { return _did; }
            set { _did = value; }
        }

        public string TituloDestaque
        {
            get { return _titulo_destaque; }
            set { _titulo_destaque = value; }
        }

        public float QtdeCredito
        {
            get { return _qtde_credito; }
            set { _qtde_credito = value; }
        }

        public int TmpEspera
        {
            get { return _tmp_espera; }
            set { _tmp_espera = value; }
        }

        public string DuracaoCampanha
        {
            get { return _duracao_campanha; }
            set { _duracao_campanha = value; }
        }

        public int QtdeDuracaoCampanha
        {
            get { return _qtde_duracao_campanha; }
            set { _qtde_duracao_campanha = value; }
        }

        public string DsDestaque
        {
            get { return _ds_destaque; }
            set { _ds_destaque = value; }
        }

        public string ImgDestaque
        {
            get { return _img_destaque; }
            set { _img_destaque = value; }
        }
        #endregion
    }
}