using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskRequestApplication.Models
{
    public class Customer
    {
        public string CustomerID { get; set; } = Guid.NewGuid().ToString(); // Müşterinin sistemdeki ID numarası.
        public string CustomerName { get;  set; } // Müşterinin sistemde kayıtlı ismi.
        public string CustomerMailAddress { get;  set; } // Müşterinin sistemde kayıtlı mail adresi.
        
        public List<Ticket> tickets = new List<Ticket>(); // Müşterinin oluşturduğu ticket listesi.
    }
}
