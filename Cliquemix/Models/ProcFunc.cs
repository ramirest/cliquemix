using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace Cliquemix.Models
{
    public static class ProcFunc
    {
        
        #region "Métodos"

            #region "Retornar o Próximo Código Temporário da Imagem do Anúncio"
            public static int RetornaProxCodTempImgAnuncio()
            {
                using (EntityConnection conn = new EntityConnection("name=cliquemixEntities"))
                {
                    conn.Open();
                    string _QrySQL = @"SELECT MAX(tbAnuncioImg.idTemp)
                                       FROM cliquemixEntities.tbAnuncioImg";

                    EntityCommand cmd = new EntityCommand(_QrySQL, conn);
                
                    try
                    {
                        using (DbDataReader rdr = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                        {
                            if (rdr.HasRows)
                            {                            
                                // Iterate through the collection of Contact items.
                                while (rdr.Read())
                                {
                                    return Convert.ToInt32(rdr[0].ToString()) + 1;
                                }
                            }
                            else
                            {
                                return 1;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        return 1;
                        throw;
                    }

                }
                return 1;
            }
            #endregion
                
        #endregion


    }
}

