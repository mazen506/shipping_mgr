using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingMgr.Core.Models
{
    public class Order:BaseEntity
    {
        [ForeignKey("Customer")]
        public long CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public Order()
        {
            Date = DateTime.Now;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}