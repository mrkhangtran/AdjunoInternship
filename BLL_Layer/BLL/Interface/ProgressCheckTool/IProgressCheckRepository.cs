using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_Layer.BLL.Interface.ProgressCheckTool
{
    interface IProgressCheckRepository
    {
        List<ProgressCheckModel> GetAll();
        void Add(ProgressCheckModel progressCheckModel);
        ProgressCheckModel Find(int id);
        void Delete(ProgressCheckModel progressCheckModel);
        ProgressCheckModel Edit(ProgressCheckModel progressCheckModel);
    }
}
