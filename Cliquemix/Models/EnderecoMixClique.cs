using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models
{

    public partial class EnderecoMixClique
    {
        public EnderecoMixClique()
        { }

        private ApplicationDbContext db = new ApplicationDbContext();
        public string RazaoSocial { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }


        public void BuscarEnderecoMixClique()
        {
            var a = (from status in db.tbConfigPadrao select status).First();
            this.RazaoSocial = a.razaoSocial;
            this.Endereco = a.endereco;
            this.Bairro = a.bairro;
            this.Cidade = a.cidade;
            this.Estado = a.uf;
            this.Cep = a.cep;
            this.Tel1 = a.tel1;
            this.Tel2 = a.tel2;
        }
    }
}