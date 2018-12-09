using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Models;
using DTOs;

namespace BLL_Layer.BLL.Interface
{
    public interface IPurchaseOrderRepository 
    {
        void AddOrUpdate(OrderDTO order);
        OrderDTO Find(int id);

        void AddOrUpdateItem(OrderDetailDTO orderDetail);
        OrderDetailDTO FindOrderDetail(int id);

        bool UniquePONum(int PONumber, int? id);
        bool UniqueItemNum(int itemNum, int? id);
    }
}
