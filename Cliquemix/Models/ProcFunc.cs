using System;
using System.Collections.Generic;
using System.IO;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Web.Mvc;
using System.Security;
using BLToolkit.Data.Sql;

namespace Cliquemix.Models
{
    public static class ProcFunc
    {

        #region "Declaração de Variáveis"
        public static ApplicationDbContext db = new ApplicationDbContext();
        #endregion

        #region "Métodos"

        #region "Retornar o Próximo Código Temporário da Imagem do Anúncio"
        public static int RetornarProxCodTempImgAnuncio()
        {
            try
            {
                var item =
                    db.tbAnuncioImg.Where(m => m.tempRenomeado == false)
                        .OrderByDescending(i => i.idTemp)
                        .FirstOrDefault();
                //var item = db.tbAnuncioImg.MaxBy(i => i.Value);
                //var a = (from img in db.tbAnuncioImg select img).Last();
                if (item != null)
                    return (int) (item.idTemp + 1);
                else
                    return 1;
            }
            catch (Exception)
            {
                return 1;
                throw;
            }
        }
        #endregion

        #region "Criar Diretórios - Somente Raíz"
        public static void CriarDiretorio(string pFolderRaiz)
        {
            try
            {
                string pathString = @pFolderRaiz;
                if (!Directory.Exists(pathString))
                {
                    Directory.CreateDirectory(pathString);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region "Criar Diretórios - Raíz + SubFolder1"
        public static void CriarDiretorio(string pFolderRaiz, string pSubFolder1)
        {
            try
            {
                string pathString = Path.Combine(@pFolderRaiz, @pSubFolder1);
                if (!Directory.Exists(pathString))
                {
                    Directory.CreateDirectory(pathString);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region "Criar Diretórios - Raíz + SubFolder1 + SubFolder2"
        public static void CriarDiretorio(string pFolderRaiz, string pSubFolder1, string pSubFolder2)
        {
            try
            {
                string pathString = Path.Combine(@pFolderRaiz, @pSubFolder1, @pSubFolder2);
                if (!Directory.Exists(pathString))
                {
                    Directory.CreateDirectory(pathString);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
            
        #region "Mover arquivos de um local ao outro"
        public static void MoverArquivosEntrePastas(string pOrigem, string pDestino)
        {
            try
            {
                Directory.Move(pOrigem, pDestino);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region "Retornar o Código do Anunciante"
        //Parâmetro é o cnpj do anunciante
        public static int RetornarCodigoAnuncianteCnpj(string _cnpj)
        {
            try
            {
                var a = (from cont in db.tbAnunciante where cont.cnpj == _cnpj select cont).First();
                return a.pid;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }
        //Parâmetro é o nome do usuário logado
        public static int RetornarCodigoAnuncianteUsuario(string pUsuario)
        {
            try
            {
                var u = (from usu in db.tbUsers where usu.username == pUsuario select usu).First();
                var a = (from anunciante in db.tbAnunciante where anunciante.uid == u.uid select anunciante).First();
                return a.pid;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }            
        //Parâmetro é o código do usuário logado
        public static int RetornarCodigoAnuncianteCodUsuario(int _codUsuario)
        {
            try
            {
                var a = (from anunciante in db.tbAnunciante where anunciante.uid == _codUsuario select anunciante).First();
                return a.pid;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }
        #endregion

        #region "Retornar o Código do Usuário"
        public static int RetornarCodigoUsuario(string _nomeUsuario)
        {
            var a = (from usu in db.tbUsers where usu.username == _nomeUsuario select usu).First();
            return a.uid;
        }
        #endregion

        #region "Retornar o Código do Tipo do Usuário"
        public static int RetornarCodigoTipoUsuario(string _tipo)
        {
            var a = (from tipo in db.tbUsersTipo where tipo.dsUsersTipo == _tipo select tipo).First();
            return a.utid;
        }
        #endregion

        #region "Retornar o Código do Status Padrão para um Novo Anúncio"
        public static int RetornarStatusPadraoAnuncio()
        {
            var a = (from status in db.tbConfigPadrao select status).First();
            return (int)a.spna;
        }
        #endregion

        #region "Retornar Início e Término padrão de publicação da Campanha"
        public static int RetornarInicioTerminoPadraoPublicacaoCampanha()
        {
            var it = (from config in db.tbConfigPadrao select config).First();
            if (it.itppc != null) return (int) it.itppc;
            else return 0;
        }
        #endregion

        #region "Retornar o Código do Status Padrão para uma Nova Campanha"
        public static int RetornarStatusPadraoCampanha()
        {
            var a = (from status in db.tbConfigPadrao select status).First();
            if (a.spnc != null) return (int) a.spnc;
            else return 0;
        }

    #endregion

        #region "Criar Código Temporário para Campanha"
        public static int CriarCodTempCampanha(string pSession)
        {
            tbCampanhaTmp campanhaTmp = new tbCampanhaTmp();
            campanhaTmp.sessionID = pSession;
            db.tbCampanhaTmp.Add(campanhaTmp);
            db.SaveChanges();
            var a = db.tbCampanhaTmp.Max(c => c.ctid);
            return a;
        }
        #endregion

        #region "Atualizar Códigos Temporários dos Anúncios da Campanha"
        public static void AtualizarCodTempAnunciosCampanha(int? pCodTempCampanha, int pCodAtualCampanha)
        {
            try
            {
                var AnuncioCampanha = db.tbCampanhaAnuncio.Where(m => m.ctid == pCodTempCampanha).ToList();
                foreach (var item in AnuncioCampanha)
                {
                    db.Entry(item).State = EntityState.Modified;
                    item.cid = pCodAtualCampanha;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region "Métodos para inserir Anúncio na Campanha"

        //Verificar se o Anúncio está Disponível para entrar na Campanha
        public static bool VerificarAnuncioDisponivelParaCampanha(int pCodAnuncio)
        {
            var c = (from status in db.tbConfigPadrao select status).First();
            var a =
                (from anuncio in db.tbAnuncio select anuncio).
                Where(m => m.asid == c.spadc).
                Where(t => t.aid == pCodAnuncio);
            if (a.Any())
                return true;
            else
                return false;
        }            

        //Retornar o Código do Status Padrão de Anúncio Disponível para Campanha
        public static int RetornarStatusPadraoAnuncioDisponivelParaCampanha()
        {
            var a = (from status in db.tbConfigPadrao select status).First();
            if (a.spadc != null) return (int)a.spadc;
            else return 0;
        }

        #endregion

        #region "Validação da campanha para novos anúncios"

        // Tudo = 4 [Não existe anúncio para vincular] *
        public static bool ExisteAnuncioParaVincular(int pCodAnunciante)
        {
            try
            {
                int s = RetornarStatusPadraoAnuncioDisponivelParaCampanha();
                var a = (from ad in db.tbAnuncio where ad.asid == s select ad).ToList();
                var c = (from ca in a where ca.pid == pCodAnunciante select ca).ToList();

                if (c.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        // Tudo = 2 [Campanha já contém um anúncio vinculado] *
        public static bool CampanhaContemAnuncio(int pCodCampanha)
        {
            try
            {
                var a = (from ca in db.tbCampanhaAnuncio where ca.ctid == pCodCampanha select ca).First();
                if (a.caid > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        #endregion

        #region "Salvar Log do Sistema"
        public static void SalvarLog(int pIdUser, string pDescricao, string pControle, string pTipoMov)
        {
            try
            {
                tbLogSistema logSistema = new tbLogSistema();
                logSistema.uid = pIdUser;
                logSistema.dataHoraLog = DateTime.Now;
                logSistema.dsLog = pDescricao;
                logSistema.dsControle = pControle;
                logSistema.tipoMovimento = pTipoMov;
                db.tbLogSistema.Add(logSistema);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region "Criptografar Senha"
        public static string CryptographyPass(string input)
        {                
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        #endregion

        #region "Descriptografar Senha"
        public static string ValidCryptographyPass(string input)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        #endregion

        #endregion

    }
}

