using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductManagementSystem.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public System.DateTime OrderDate { get; set; }
        public Nullable<int> UId { get; set; }
    }
}