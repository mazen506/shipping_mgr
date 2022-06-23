using ShippingMgr.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingMgr.Core.ViewModels
{
    public class OrderItemVM: BaseVM
    {
        public long OrderId { get; set; }
        public long ItemId { get; set; }
        public ItemVM? Item { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public string Note { get; set; }
    }
}
