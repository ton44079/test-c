using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TestC.Repositories.Interface;

namespace TestC.Repositories.Implements
{
    public class PropertiesRepository : Repository<Entity.property> , IPropertiesRepository
    {
        public PropertiesRepository(DbContext context) : base(context)
        {
        }
    }
}