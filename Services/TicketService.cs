using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskRequestApplication.Models;
using TaskRequestApplication.Repositories;

namespace TaskRequestApplication.Services
{
    public class TicketService
    {
        TicketRepository _ticketRepository; // Ticket repository'ye bağlar.

        public TicketService(TicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository; // Ticket işlemleri için repository bağlantısı kurulur.
        }

        /// <summary>
        /// Sisteme ilk defa ticket açmak için.
        /// </summary>
        /// <param name="ticket"></param>
        public void AddTicket(Ticket ticket)
        {
            if (ticket == null)
            {
                throw new Exception("Ticket boş olamaz.");
            }
            if (string.IsNullOrEmpty(ticket.TicketID) || ticket.TicketCustomerID == "value")
            {
                throw new Exception("Ticket Customer bilgisi boş olamaz.");
            }
            if (string.IsNullOrEmpty(ticket.TicketSubject) || ticket.TicketSubject.Length > 50)
            {
                throw new Exception("Ticket konu bölümü boş veya 50 karakterden fazla olamaz.");
            }
            if (string.IsNullOrEmpty(ticket.TicketDescription) || ticket.TicketDescription.Length > 500)
            {
                throw new Exception("Ticket açıklama bölümü boş veya 500 karakterden fazla olamaz.");
            }
            if (string.IsNullOrEmpty(ticket.TicketID))
            {
                throw new Exception("TicketID boş olamaz.");
            }

            ticket.TicketStatus = TicketStatusType.OPEN;
            ticket.TicketCreatedDate = DateTime.Now;
            _ticketRepository.AddTicket(ticket);
        }

        /// <summary>
        /// Ticket statüsü OPEN olan ticketları liste olarak döndürür.
        /// </summary>
        /// <returns></returns>
        public List<Ticket> FindOpenTicket()
        {
            List<Ticket> ticketList = new List<Ticket>();
            var list =_ticketRepository.GetAllTicket();
            foreach (var item in list)
            {
                if (item.TicketStatus == TicketStatusType.OPEN)
                {
                    ticketList.Add(item);

                }
            }
            return ticketList;
        }

        /// <summary>
        /// Ticket güncelle
        /// </summary>
        /// <param name="ticket"></param>
        public void Update(Ticket ticket)
        {
            _ticketRepository.UpdateTicket(ticket);
        }

        /// <summary>
        /// Ticket seviyeleri kontrol edilir, Statü ReadyForAssignment atandıktan sonra Ticket Update edilir.
        /// </summary>
        /// <param name="ticket"></param>
        public void CheckTicketLevels(Ticket ticket)
        {
            if (ticket.TicketDifficultyLevel == null)
            {
                throw new Exception("Difficulty level boş bırakılamaz.");
            }
            if (ticket.TicketPriority == 0)
            {
                throw new Exception("Priority boş bırakılamaz.");
            }
            ticket.TicketStatus = TicketStatusType.ReadyForAssignment;
            _ticketRepository.UpdateTicket(ticket);
        }

        /// <summary>
        /// Ticket'a employee tanımlar.
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="id"></param>
        public void AssignedEmployee(Ticket ticket, string id)
        {
            ticket.EmployeeID = id;
            ticket.TicketStatus = TicketStatusType.Assigment;
            ticket.TicketAssigneeDate = DateTime.Now;
            _ticketRepository.UpdateTicket(ticket);
        }

        /// <summary>
        /// Employee'ye ait olan ticketları atar.
        /// </summary>
        /// <param name="employee"></param>
        public void EmployeeTickets(Employee employee)
        {
            var x =_ticketRepository.GetAllTicket();
            foreach (var ticket in x)
            {
                if (ticket.EmployeeID == employee.EmployeeID)
                {
                    employee.ticketsID.Add(ticket.TicketID);
                }            
            }
        }

        /// <summary>
        /// Ticket'ı closed olarak tanımlar.
        /// </summary>
        /// <param name="ticket"></param>
        public void ClosedTicket(Ticket ticket)
        {
            ticket.TicketStatus = TicketStatusType.Closed;
            ticket.TicketClosedDate = DateTime.Now;
            _ticketRepository.UpdateTicket(ticket);
        }

        /// <summary>
        /// Ticket'ı review olarak tanımlar.
        /// </summary>
        /// <param name="ticket"></param>
        public void ReviewTicket(Ticket ticket)
        {
            ticket.TicketStatus = TicketStatusType.Review;
            ticket.TicketReviewDate = DateTime.Now;
            _ticketRepository.UpdateTicket(ticket);
        }

        /// <summary>
        /// Ticket'ı completed tanımlar.
        /// </summary>
        /// <param name="ticket"></param>
        public void CompletedTicket(Ticket ticket)
        {
            ticket.TicketStatus = TicketStatusType.Completed;
            ticket.TicketCompletedDate = DateTime.Now;
            _ticketRepository.UpdateTicket(ticket);
        }
    }
}
