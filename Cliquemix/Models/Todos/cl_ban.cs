using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cliquemix.Models.Todos
{
    public class cl_ban
    {
        #region _Atributos 
        private Int32 _baid; //Código do ban
        private Int32 _btid; //Código do tipo do ban
        private Int32 _uid; //Código do usuário banido
        private DateTime _dtOcorrido; //Data do ban ocorrido
        #endregion

        #region _Propriedades 
        public Int32 Baid
        {
            get { return _baid; }
        }

        public Int32 Btid
        {
            get { return _btid; }
            set { _btid = value; }
        }

        public Int32 Uid
        {
            get { return _uid; }
            set { _uid = value; }
        }

        public DateTime DtOcorrido
        {
            get { return _dtOcorrido; }
            set { _dtOcorrido = value; }
        }
        #endregion

        #region _Métodos 

        #region _Método Novo Ban 
        public void _novo(Int32 @pCodTipoBan, Int32 @pCodUsuBan, DateTime @pDtOcorrido)
        {
            try
            {
                Btid = pCodTipoBan;
                Uid = pCodUsuBan;
                DtOcorrido = pDtOcorrido;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Editar Ban 
        public void _novo(Int32 @pCodBan, Int32 @pCodTipoBan, Int32 @pCodUsuBan, DateTime @pDtOcorrido)
        {
            try
            {
                //where btid == pCodBan
                Btid = pCodTipoBan;
                Uid = pCodUsuBan;
                DtOcorrido = pDtOcorrido;
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion

        #region _Método Excluir Ban 
        public void _excluir(Int32 @pCodBan)
        {
            try
            {
                //Delete SQL
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #endregion

        #region _Construtores 
        public cl_ban() // Contrutor Padrão
        {
            _baid = 0;
            _btid = 0;
            _uid = 0;
            _dtOcorrido = DateTime.Now;
        }
        #endregion

        #region _Destrutores 
        public ~cl_ban() // Destrutor Padrão
        {
            _baid = 0;
            _btid = 0;
            _uid = 0;
            _dtOcorrido = DateTime.Now;
        }        
        #endregion
    }
}