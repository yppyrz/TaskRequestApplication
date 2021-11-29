using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskRequestApplication.Models;
using TaskRequestApplication.Repositories;
using TaskRequestApplication.Services;

namespace TaskRequestApplication.Pages.TicketTechDetails
{
    public class TicketTechDetailsPageModel : PageModel
    {
        private readonly CustomerRepository _customerRepository;


        private readonly TicketRepository _ticketRepository;
        private readonly TicketService _ticketService;

        public TicketTechDetailsPageModel(CustomerRepository customerRepository, TicketRepository ticketRepository, TicketService ticketService)
        {
            _customerRepository = customerRepository;
            _ticketRepository = ticketRepository;
            _ticketService = ticketService;
        }

        [BindProperty]
        public string customerName { get; set; } // Ticket'� olu�turan customer'�n ad�
        [BindProperty]
        public Ticket ticket { get; set; }
        [BindProperty]
        public Ticket ticket2 { get; set; }
        [BindProperty]
        public string TicketSubject { get; set; }
        [BindProperty]
        public string TicketDescription { get; set; }
        [BindProperty]
        public DateTime TicketDate { get; set; } // Ticket olu�turulma tarihi
        
        [BindProperty]
        public string[] difficultyLevels { get; set; } = new[] {"Kolay","Orta","Zor","�ok Zor"};
        [BindProperty]
        public int[] Priorities { get; set; } = new[] {1,2,3,4,5};
        [BindProperty]
        public string difficultyLevel { get; set; } // Se�ilen difficultylevel
        [BindProperty]
        public int Priority { get; set; } // Se�ilen priority

        public void OnGet(string id)
        {
            var ticketList = _ticketRepository.GetAllTicket();
            ticket = ticketList.Find(x => x.TicketID == id); // Set tech details'a bas�lan ticket'� bulur.
            ticket2 = ticket;

            TicketSubject = ticket.TicketSubject;
            TicketDescription = ticket.TicketDescription;
            TicketDate = ticket.TicketCreatedDate;
            
            customerName = _customerRepository.FindCustomer(ticket.TicketCustomerID).CustomerName;


        }
        public void OnPostSave()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ticket.TicketPriority = Priority;
                    ticket.TicketDifficultyLevel = difficultyLevel;
                    _ticketService.CheckTicketLevels(ticket); // Ticket seviyeleri kontrol edilip, stat� ayarlan�r ve database'e kaydedilir.

                    var result = _ticketRepository.FindTicket(ticketId: ticket.TicketID); // Ticket database'de var m�
                   
                    if (result.TicketStatus == TicketStatusType.ReadyForAssignment)
                    {
                        ViewData["Message"] = "Kay�t Ba�ar�l�d�r";
                    }
                    else
                    {
                        ViewData["Message"] = "Tekrar deneyiniz";
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Hata", ex.Message);
                    ViewData["Message"] = "Tekrar deneyiniz";
                }
            }

        }
    }
}
