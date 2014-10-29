using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.IO;
using System.Xml;
using Cliquemix.Models;

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
        public ActionResult Index(int tipo, int s)
        {
            var dt = DateTime.Now;

            string txt =
                dt.Year.ToString(format: "0000") +
                dt.Month.ToString(format: "00") +
                dt.Day.ToString(format: "00") +
                dt.Hour.ToString(format: "00") +
                dt.Minute.ToString(format: "00") +
                dt.Second.ToString(format: "00") +
                dt.Millisecond.ToString(format: "000");

            //$sid = md5(date("YmdHisu"));
            // 2013091811394554321
            // 2013-09-18-11:39:45:54321

            string sid = string.Empty;

            using (MD5 md5Hash = MD5.Create())
            {
                sid = CriptografarSid(md5Hash, txt);
            }

            try
            {
                if (tipo == 1)  //Criar Transação
                    CriarTransacao(sid);
                else if (tipo == 0)  //Consultar Transação
                    ConsultarTransacao(sid);

                post_data(sid);
            }
            catch (Exception)
            {
                throw;
            }

            
            return View();
        }


        public void ConsultarTransacao(string sid)
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
            XmlText text2 = doc.CreateTextNode("consultar");
            element2.AppendChild(text2);
            element1.AppendChild(element2);

            XmlElement element3 = doc.CreateElement(string.Empty, "sid", string.Empty);
            XmlText text3 = doc.CreateTextNode(sid);
            element3.AppendChild(text3);
            element1.AppendChild(element3);

            doc.Save(Server.MapPath("~/Arquivos/XmlPagamento/" + sid + ".xml"));
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
            XmlText text7 = doc.CreateTextNode("1800.00");
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

            doc.Save(Server.MapPath("~/Arquivos/XmlPagamento/" + sid + ".xml"));
        }


        #region "Criptografar Sid"
        public static string CriptografarSid(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash. 
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes 
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string. 
            return sBuilder.ToString();
        }

        #endregion


        #region "Validar Sid"
        static bool ValidarSid(MD5 md5Hash, string input, string hash)
        {
            // Hash the input. 
            string hashOfInput = CriptografarSid(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


        public void post_data(string sid)
        {

            Stream requestStream = null;
            WebResponse response = null;
            StreamReader reader = null;

            string url = "http://pagsicove.host-up.com/ws/";

            WebRequest request = WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Post;
            request.ContentType = "application/x-www-form-urlencoded";
            
            byte[] byteBuffer = null;
            var fileName = Server.MapPath(@"~/Arquivos/XmlPagamento/" + sid + ".xml");

            var stringXml = this.GetTextFromXMLFile(fileName);
            var postData = String.Format("mensagem={0}", stringXml);

            byteBuffer = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = byteBuffer.Length;
            requestStream = request.GetRequestStream();
            requestStream.Write(byteBuffer, 0, byteBuffer.Length);
            requestStream.Close();
            

            response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            
            System.Text.Encoding encoding = System.Text.Encoding.Default;
            if (responseStream != null) 
                reader = new StreamReader(responseStream, encoding);

            Char[] charBuffer = new Char[256];

            int count = reader.Read(charBuffer, 0, charBuffer.Length);
            
            var Dados = new StringBuilder();

            while (count > 0)
            {
                Dados.Append(new String(charBuffer, 0, count));
                count = reader.Read(charBuffer, 0, charBuffer.Length);
            }

            Response.Write(Dados.ToString());
        }


        private string GetTextFromXMLFile(string file)
        {
            var reader = new StreamReader(file);
            string ret = reader.ReadToEnd();
            reader.Close();
            return ret;
        }

    }
}
