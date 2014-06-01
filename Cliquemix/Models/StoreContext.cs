using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;


namespace Cliquemix.Models
{
    public class StoreContext:ObjectContext
    {
        /// <summary> 
        /// Construtor informando conectionstring e nome do container 
        /// </summary> 
        
        public StoreContext() : 
            base("name=StoreEntityDataModelContainer", "StoreEntityDataModelContainer") { }


    }
}