using ShippingMgr.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingMgr.Core.ViewModels
{
    public class OrderVM: BaseVM
    {
        public long CustomerId { get; set; }
        public CustomerVM? Customer { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public ICollection<OrderItemVM> Items { get; set; }
    }
}
