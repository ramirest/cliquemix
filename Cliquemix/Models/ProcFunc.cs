using System;
using System.IO;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Web.Mvc;
using System.Security;

namespace Cliquemix.Models
{
    public static class ProcFunc
    {
        #region "Métodos"

            #region "Retornar o Próximo Código Temporário da Imagem do Anúncio"
            public static int RetornaProxCodTempImgAnuncio()
            {
                try
                {
                    ApplicationDbContext db = new ApplicationDbContext();
                    var item = db.tbAnuncioImg.OrderByDescending(i => i.idTemp).FirstOrDefault();
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
            public static int RetornarCodigoAnunciante(string pUsuario)
            {
                try
                {
                    ApplicationDbContext db = new ApplicationDbContext();
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

