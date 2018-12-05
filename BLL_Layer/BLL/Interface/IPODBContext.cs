using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL_Layer.DAL.DBContext;

namespace BLL_Layer.BLL.Interface
{
    public interface IPODBContext
    {
        PODBContext GetDB();
    }
}
