using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskRequestApplication.Models;

namespace TaskRequestApplication.Services
{
    public class EmailService
    {
        public string from { get; set; } = "yppyrz@gmail.com";
        void SendMailCustomer(Customer customer, Ticket ticket)
        {
            var to = customer.CustomerMailAddress;
            var messega = $"{ticket.TicketID} numaralı ticket sisteme başarılı şekilde kaydedilmiştir.";
            var subject = "Başarılı Ticket Kaydı.";
            
        }
    }
}
