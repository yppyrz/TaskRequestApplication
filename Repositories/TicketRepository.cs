using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskRequestApplication.Models;

namespace TaskRequestApplication.Repositories
{
    public class TicketRepository
    {
        private readonly TaskRequestApplicationDBContext _db; // Database bağlantısı.

        public TicketRepository()
        {
            _db = new TaskRequestApplicationDBContext(); // Repository oluşturulurken database'den instance alınır.
        }

        /// <summary>
        /// Database'e yeni bir ticket eklenir.
        /// </summary>
        /// <param name="ticket"></param>
        public void AddTicket(Ticket ticket)
        {
            _db.Tickets.Add(ticket);
            _db.SaveChanges();
        }

        /// <summary>
        /// Database'de ticket tablosunu günceller.
        /// </summary>
        /// <param name="ticket"></param>
        public void UpdateTicket(Ticket ticket)
        {
            _db.Tickets.Update(ticket);
            _db.SaveChanges();
        }


        /// <summary>
        /// Database'de olan bir ticket'ı TicketId üzerinden sorgulama sağlar.
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        public Ticket FindTicket(string ticketId)
        {
            return _db.Tickets.Find(ticketId);
        }

        /// <summary>
        /// Database'de bulunan Ticket tablosuna liste olarak erişim sağlatır.
        /// </summary>
        /// <returns></returns>
        public List<Ticket> GetAllTicket()
        {
            return _db.Tickets.ToList();
        }



    }
}
