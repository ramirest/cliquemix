using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using Cliquemix.Models;
using Microsoft.Reporting.WebForms;

namespace Cliquemix.Controllers
{
    public class TesteController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Teste
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(int s)
        {
            var dt = DateTime.Now;

            //string DataFormato3 = dt.ToString("G");
            var txt =
                dt.Year.ToString(format: "0000") +
                dt.Month.ToString(format: "00") +
                dt.Day.ToString(format: "00") +
                dt.Hour.ToString(format: "00") +
                dt.Minute.ToString(format: "00") +
                dt.Second.ToString(format: "00") +
                dt.Millisecond.ToString(format: "000");

            var sid = CriptografarSid(txt);
            //ProcessarPagamento(s);

            try
            {
                CriarTransacao(sid);
                post_data();
                //Redirect("http://pagsicove.host-up.com/ws/forma/?sid=" + sid);
            }
            catch (Exception)
            {
                throw;
            }
            

            //$sid = md5(date("YmdHisu"));
            // 2013091811394554321
            // 2013-09-18-11:39:45:54321
            
            return View();
        }

        public void CriarTransacao(string sid)
        {
            //Tipo = 0 (consultar)  |  Tipo = 1 (criartransacao)
            var doc = new XmlDocument();

            //(1) the xml declaration is recommended, but not mandatory
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            XmlElement element1 = doc.CreateElement(string.Empty, "xml", string.Empty);
            doc.AppendChild(element1);

            XmlElement element2 = doc.CreateElement(string.Empty, "transacao", string.Empty);
            XmlText text2 = doc.CreateTextNode("criartransacao");
            element2.AppendChild(text2);
            element1.AppendChild(element2);

            XmlElement element3 = doc.CreateElement(string.Empty, "sid", string.Empty);
            XmlText text3 = doc.CreateTextNode(sid);
            element3.AppendChild(text3);
            element1.AppendChild(element3);

            XmlElement element4 = doc.CreateElement(string.Empty, "codigoafiliacao", string.Empty);
            XmlText text4 = doc.CreateTextNode("1006993069");
            element4.AppendChild(text4);
            element1.AppendChild(element4);

            XmlElement element5 = doc.CreateElement(string.Empty, "chave", string.Empty);
            XmlText text5 = doc.CreateTextNode("25fbb99741c739dd84d7b06ec78c9bac718838630f30b112d033ce2e621b34f3");
            element5.AppendChild(text5);
            element1.AppendChild(element5);

            XmlElement element6 = doc.CreateElement(string.Empty, "credor", string.Empty);
            XmlText text6 = doc.CreateTextNode("Renato Montanha");
            element6.AppendChild(text6);
            element1.AppendChild(element6);

            XmlElement element7 = doc.CreateElement(string.Empty, "valor", string.Empty);
            XmlText text7 = doc.CreateTextNode("1200.00");
            element7.AppendChild(text7);
            element1.AppendChild(element7);

            XmlElement element8 = doc.CreateElement(string.Empty, "datavencimento", string.Empty);
            XmlText text8 = doc.CreateTextNode("2014-09-30");
            element8.AppendChild(text8);
            element1.AppendChild(element8);

            XmlElement element9 = doc.CreateElement(string.Empty, "descricao", string.Empty);
            XmlText text9 = doc.CreateTextNode("Pacote de Crédito 001");
            element9.AppendChild(text9);
            element1.AppendChild(element9);

            XmlElement element10 = doc.CreateElement(string.Empty, "cpf", string.Empty);
            XmlText text10 = doc.CreateTextNode("578.112.087-41");
            element10.AppendChild(text10);
            element1.AppendChild(element10);

            XmlElement element11 = doc.CreateElement(string.Empty, "cep", string.Empty);
            XmlText text11 = doc.CreateTextNode("35160000");
            element11.AppendChild(text11);
            element1.AppendChild(element11);

            XmlElement element12 = doc.CreateElement(string.Empty, "cidade", string.Empty);
            XmlText text12 = doc.CreateTextNode("Ipatinga");
            element12.AppendChild(text12);
            element1.AppendChild(element12);

            XmlElement element13 = doc.CreateElement(string.Empty, "estado", string.Empty);
            XmlText text13 = doc.CreateTextNode("MG");
            element13.AppendChild(text13);
            element1.AppendChild(element13);

            XmlElement element14 = doc.CreateElement(string.Empty, "bairro", string.Empty);
            XmlText text14 = doc.CreateTextNode("Bethânia");
            element14.AppendChild(text14);
            element1.AppendChild(element14);

            XmlElement element15 = doc.CreateElement(string.Empty, "endereco", string.Empty);
            XmlText text15 = doc.CreateTextNode("Avenida Selim José de Sales");
            element15.AppendChild(text15);
            element1.AppendChild(element15);

            XmlElement element16 = doc.CreateElement(string.Empty, "numero", string.Empty);
            XmlText text16 = doc.CreateTextNode("1500");
            element16.AppendChild(text16);
            element1.AppendChild(element16);

            XmlElement element17 = doc.CreateElement(string.Empty, "complemento", string.Empty);
            XmlText text17 = doc.CreateTextNode("Casa");
            element17.AppendChild(text17);
            element1.AppendChild(element17);

            XmlElement element18 = doc.CreateElement(string.Empty, "nmaximoparcelas", string.Empty);
            XmlText text18 = doc.CreateTextNode("5");
            element18.AppendChild(text18);
            element1.AppendChild(element18);

            XmlElement element19 = doc.CreateElement(string.Empty, "campolivre", string.Empty);
            XmlText text19 = doc.CreateTextNode("campo de texto livre");
            element19.AppendChild(text19);
            element1.AppendChild(element19);

            XmlElement element20 = doc.CreateElement(string.Empty, "urlretornoloja", string.Empty);
            XmlText text20 = doc.CreateTextNode("http://localhost:35543/");
            element20.AppendChild(text20);
            element1.AppendChild(element20);

            doc.Save(Server.MapPath("~/Arquivos/XmlPagamento/CriarTransacao.xml"));
        }


        #region "Criptografar Sid"
        public static string CriptografarSid(string sid)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(sid);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        #endregion


        #region "Validar Sid
        public static string ValidarSid(string sid)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(sid);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        #endregion


        public void post_data()
        {
            WebRequest req = null;
            WebResponse rsp = null;

            try
            {
                string filename = Server.MapPath("~/Arquivos/XmlPagamento/CriarTransacao.xml");
                string uri = "http://pagsicove.host-up.com/ws/";
                req = WebRequest.Create(uri);

                req.Method = "POST";
                //req.ContentType = "Text/xml";
                req.ContentType = "application/x-www-form-urlencoded";
                
                var writer = new StreamWriter(req.GetRequestStream());

                writer.WriteLine(this.GetTextFromXMLFile(filename));
                writer.Close();

                rsp = req.GetResponse();
            }
            catch (WebException webEx)
            {   }
            catch (Exception ex)
            {   }
            finally
            {
                if (req != null)
                    req.GetRequestStream().Close();

                if (rsp != null)
                    rsp.GetResponseStream().Close();
            }
        }


        private string GetTextFromXMLFile(string file)
        {
            StreamReader reader = new StreamReader(file);
            string ret = reader.ReadToEnd();
            reader.Close();
            return ret;
        }



        public void ConsultarTransacao()
        {
            //Tipo = 0 (consultar)  |  Tipo = 1 (criartransacao)
            var doc = new XmlDocument();

            //(1) the xml declaration is recommended, but not mandatory
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            XmlElement element2 = doc.CreateElement(string.Empty, "xml", string.Empty);
            doc.AppendChild(element2);

            XmlElement element3 = doc.CreateElement(string.Empty, "transacao", string.Empty);
            XmlText text1 = doc.CreateTextNode("consultar");
            element3.AppendChild(text1);
            element2.AppendChild(element3);

            XmlElement element4 = doc.CreateElement(string.Empty, "sid", string.Empty);
            XmlText text2 = doc.CreateTextNode("02b95fb5f996954aedb91f4dd0a90839x");
            element4.AppendChild(text2);
            element2.AppendChild(element4);

            doc.Save(Server.MapPath("~/Views/Pagamento/document.xml")); // "E:\\document.xml");     

            Response.Redirect("http://pagsicove.host-up.com/ws/" + doc);
        }

    }
}