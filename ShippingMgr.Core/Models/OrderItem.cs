using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ShippingMgr.Core.Models
{
    public class OrderItem: BaseEntity
    {
        [ForeignKey("Order")]
        public long OrderId { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }    

        [ForeignKey("Item")]
        public long ItemId { get; set; }
        public Item Item { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public string Note { get; set; }
        
        
    }
}
