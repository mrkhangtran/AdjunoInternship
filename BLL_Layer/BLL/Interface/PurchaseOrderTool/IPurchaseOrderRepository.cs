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
        OrderDTO Find(string id);

        void AddOrUpdateItem(OrderDetailDTO orderDetail);
        OrderDetailDTO FindOrderDetail(string id);

        bool UniquePONum(string PONumber, int? id);
        bool UniqueItemNum(string itemNum, int? id);
    }
}
