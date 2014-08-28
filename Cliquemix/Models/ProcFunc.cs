using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Data.Entity;

namespace Cliquemix.Models
{
    public static class ProcFunc
    {

        #region "Declaração de Variáveis"
        //public static ApplicationDbContext db = new ApplicationDbContext();
        public static ApplicationDbContext db = new ApplicationDbContext();
        #endregion

        #region "Métodos"

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

        #region "Remover arquivo do diretório"
        public static void RemoverArquivos(string pOrigem)
        {
            if (System.IO.File.Exists(@pOrigem))
            {
                try
                {
                    System.IO.File.Delete(@pOrigem);
                }
                catch (System.IO.IOException e)
                {
                    return;
                }
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

        #region "Retornar Tipo do Usuário Logado"
        public static int RetornarTipoUsuarioLogado(string _nomeUsuario)
        {
            var a = (from usu in db.tbUsers where usu.username == _nomeUsuario select usu).First();
            return a.utid;
        }
        #endregion

        #region "Retornar Usuário Logado"
        public static int RetornarUsuarioLogado()
        {
            //var a = (from usu in db.tbUsers where usu.username == _nomeUsuario select usu).First();
            return 2;
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
            var it = (from config in db.tbConfigPadrao where config.cfgid==1 select config).First();
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
        public static void AtualizarCodTempAnunciosCampanha(int? pCodTempCampanha, int pCodAtualCampanha, int?pCodDestaqueAnunciante)
        {
            try
            {
                //Verifica qual é o destaque vinculado à campanha
                var destaque = db.tbDestaqueAnunciante.First(m => m.daid == pCodDestaqueAnunciante).tbDestaque;
                //Consulta a quantidade de cliques vinculado ao destaque
                var QtdeCliquesFinal = db.tbPacoteClique.First(m => m.pcid == destaque.pcid).qtdeCliques;
                //Lista o anúncio vinculado à campanha
                var AnuncioCampanha = db.tbCampanhaAnuncio.Where(m => m.ctid == pCodTempCampanha).ToList();
                foreach (var item in AnuncioCampanha)
                {
                    db.Entry(item).State = EntityState.Modified;
                    item.cid = pCodAtualCampanha;
                    item.contCliquesFinal = QtdeCliquesFinal;
                    item.contCliquesAtual = 0;
                    item.contCurtidas = 0;
                    item.contComentarios = 0;
                    item.contCompartilhadas = 0;
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

        #region "Retornar o Código do Status Padrão para um Anúncio Excluído"
        public static int RetornarStatusPadraoAnuncioExcluido()
        {
            var a = (from status in db.tbConfigPadrao select status).First();
            return (int)a.spea;
        }
        #endregion

        #region "Retornar o Código do Status Padrão para um Destaque Excluído"
        public static int RetornarStatusPadraoDestaqueExcluido()
        {
            var a = (from status in db.tbConfigPadrao select status).First();
            return (int)a.spde;
        }
        #endregion

        #region "Retornar o Código do Status Padrão para um Destaque Disponível"
        public static int RetornarStatusPadraoDestaqueDisponivel()
        {
            var a = (from status in db.tbConfigPadrao select status).First();
            return (int)a.spdd;
        }
        #endregion

        #region "Retornar o Código do Status Padrão para um Destaque Anunciante Disponível"
        public static int RetornarStatusPadraoDestaqueAnuncianteDisponivel()
        {
            var a = (from status in db.tbConfigPadrao select status).First();
            return (int)a.spdad;
        }
        #endregion

        #region "Retornar o Código do Status Padrão para um Destaque Anunciante Em Campanha"
        public static int RetornarStatusPadraoDestaqueAnuncianteEmCampanha()
        {
            var a = (from status in db.tbConfigPadrao select status).First();
            return (int)a.spdaec;
        }
        #endregion

        #region "Retornar o Código do Status Padrão para um Destaque Anunciante Comprado"
        public static int RetornarStatusPadraoDestaqueAnuncianteComprado()
        {
            var a = (from status in db.tbConfigPadrao select status).First();
            return (int)a.spdac;
        }
        #endregion

        #region "Retornar o Código do Status Padrão para uma Campanha Excluída"
        public static int RetornarStatusPadraoCampanhaExcluida()
        {
            var a = (from status in db.tbConfigPadrao select status).First();
            return (int)a.spec;
        }
        #endregion

        #region "Retornar o Código do Status Padrão para uma Campanha Desativada"
        public static int RetornarStatusPadraoCampanhaDesativada()
        {
            var a = (from status in db.tbConfigPadrao select status).First();
            return (int)a.spdc;
        }
        #endregion

        #region "Retornar o Código do Status Padrão para uma Campanha Programada"
        public static int RetornarStatusPadraoCampanhaProgramada()
        {
            var a = (from status in db.tbConfigPadrao select status).First();
            return (int)a.spcp;
        }
        #endregion

        #region "Retornar Quantidade de Dias Padrão para Programar Término da Campanha"
        public static int RetornarQtdeDiasPadraoTerminoCampanha()
        {
            var a = (from status in db.tbConfigPadrao select status).First();
            return (int)a.qdptcp;
        }
        #endregion

        #region "Mudar Status do Anúncio Excluído"
        public static void AnuncioExcluido(int pCodAnuncio)
        {
            var a = (from anuncio in db.tbAnuncio where anuncio.aid == pCodAnuncio select anuncio).First();
            a.asid = RetornarStatusPadraoAnuncioExcluido();
            db.SaveChanges();
        }
        #endregion

        #region "Inserir código do anúncio na tabela tbAnuncioCodTemp"
        public static void InserirCodAnuncioTbAnuncioCodTemp(int? pCodTemp, int pCodAnuncio)
        {
            var a = (from act in db.tbAnuncioCodTemp where act.actid == pCodTemp select act).First();
            a.aid = pCodAnuncio;
            db.SaveChanges();
        }
        #endregion

        #region "Inserir código do anúncio na tabela tbAnuncioImg"
        public static void InserirCodAnuncioTbAnuncioImg(int? pCodTemp, int pCodAnuncio, int pIdUser)
        {
            try
            {
                var img = db.tbAnuncioImg.Where(m => m.actid == pCodTemp).ToList();
                foreach (var item in img)
                {
                    item.aid = pCodAnuncio;
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                SalvarLog(pIdUser, e.Message, "Inserir código do anúncio na tabela tbAnuncioImg", "Ajuste Código Anúncio");
            }
        }
        #endregion

        #region "Alterar saldo do anunciante na tabela tbAnunciante"
        public static void AlterarSaldoAnunciante(int? pCodAnunciante, decimal pSaldoFinal, decimal pVlrMov)
        {
            int uid = 0;    decimal vlrAtual = 0;
            try
            {
                var anun = db.tbAnunciante.First(m => m.pid == pCodAnunciante);
                uid = (int) anun.uid;
                vlrAtual = (decimal) anun.saldoCreditos;
                anun.saldoCreditos = pSaldoFinal;

                db.Entry(anun).State = EntityState.Modified;
                db.SaveChanges();
                SalvarLogMovFinanceira(uid, vlrAtual, pVlrMov, 0, anun.pid, "tbAnunciante");
            }
            catch(Exception e)
            {
                SalvarLog(uid, e.Message, "Alteração no saldo de anunciante | Compra de Crédito | Saldo Final: "+
                    pSaldoFinal.ToString()+" | Valor Movimentado: "+pVlrMov.ToString(), "Ajuste no saldo do anunciante");
            }
        }
        #endregion

        #region "Utilizar Destaque do Anunciante na Campanha"
        public static void UtilizarDestaqueAnuncianteCampanha(int? pCodAnunciante, int? pCodDestaqueAnunciante)
        {
            try
            {
                //Filtra o anunciante responsável pelo campanha
                var anun = db.tbAnunciante.First(m => m.pid == pCodAnunciante);
                //Seta uma variável para armazenamento do usuário
                var uid = 0;
                try
                {
                    //Seta o código do usuário do anunciante
                    uid = (int) anun.uid;
                    //Filtra o destaque do anunciante que será vinculado na campanha
                    //Parâmetros: 
                    //Cód. do Destaque == pCodDestaque | Status Disponível | Cód. do Anunciante == pCodAnunciante
                    var da = db.tbDestaqueAnunciante.First(d => d.daid == pCodDestaqueAnunciante);
                    da.dasid = RetornarStatusPadraoDestaqueAnuncianteEmCampanha();

                    db.Entry(da).State = EntityState.Modified;
                    db.SaveChanges();
                    SalvarLog(uid, "DestaqueAnunciante "+da.daid+" Vinculado na Campanha", "CreateCampanha", "Nova Campanha");
                }
                catch (Exception e)
                {
                    SalvarLog(uid, e.Message, "Alteração no status do destaque do anunciante - Cód. do Anunciante: "+anun.pid,
                        "DestaqueAnunciante na Campanha");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region "Filtrar Cidades"
        public static IList<tbCidade> FiltrarCidades(string pCidade)
        {           
            var cidades = db.tbCidade.ToList();
            try
            {
                if (pCidade == "Todos")
                {
                    cidades = db.tbCidade.ToList();
                }
                else
                {
                    cidades = db.tbCidade.Where(m => m.nomeCidade.Contains(pCidade)).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return cidades;
        }
        #endregion

        #region "Salvar Log Movimentação Financeira"
        //int uid, datetime dataHoraLog, decimal vlrAtual, decimal vlrMovimento, int tipoMov (0 ou 1), int id, varchar tb
        public static void SalvarLogMovFinanceira
            (int pUID, decimal pVlrAtual, decimal pVlrMovimento, int pTipoMov, int pID, string pTB)
        {
            try
            {
                tbLogMovFinanceiro logMovFinanceiro = new tbLogMovFinanceiro();
                logMovFinanceiro.uid = pUID;
                logMovFinanceiro.dataHoraLog = DateTime.Now;
                logMovFinanceiro.vlrAtual = pVlrAtual;
                logMovFinanceiro.vlrMovimento = pVlrMovimento;
                logMovFinanceiro.tipoMov = pTipoMov;
                logMovFinanceiro.id = pID;
                logMovFinanceiro.tb = pTB;
                db.tbLogMovFinanceiro.Add(logMovFinanceiro);
                db.SaveChanges();
            }
            catch (Exception)
            {
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

