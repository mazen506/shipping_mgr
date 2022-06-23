using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShippingMgr.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingMgr.Core.ViewModels
{
    public class CustomerVM: BaseVM
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? Phone { get; set; }
    }
}
