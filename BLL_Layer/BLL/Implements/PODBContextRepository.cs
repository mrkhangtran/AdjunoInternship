using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_Layer.DAL.DBContext;
using BLL_Layer.BLL.Interface;

namespace BLL_Layer.BLL.Implements
{
    public class PODBContextRepository : IPODBContext
    {
        private PODBContext db = new PODBContext();

        public PODBContext GetDB()
        {
            return db;
        }
    }
}
