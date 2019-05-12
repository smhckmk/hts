using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace hts.Entity
{
    public class DataInitiliazer:DropCreateDatabaseIfModelChanges<htsContext>
    {
        protected override void Seed(htsContext context)
        {           
            base.Seed(context);
        }
    }
}