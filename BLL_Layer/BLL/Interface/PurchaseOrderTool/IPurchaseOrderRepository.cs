using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Models;

namespace BLL_Layer.BLL.Interface
{
    public interface IPurchaseOrderRepository 
    {
        void Add(OrderModel order);
        void Edit(int Id, OrderModel newOrder);
        OrderModel Find(int Id);
    }
}
