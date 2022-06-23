using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingMgr.Core.Models
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set;}
        public DateTime UpdatedAt { get; set; }
    }
}
