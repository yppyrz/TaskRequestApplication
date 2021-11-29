using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskRequestApplication.Models;
using TaskRequestApplication.Repositories;
using TaskRequestApplication.Services;

namespace TaskRequestApplication.Pages.OpenTickets
{
    public class OpenTicketsPageModel : PageModel
    {

        private readonly TicketService _ticketService;

        public OpenTicketsPageModel(TicketService ticketService)
        {

            _ticketService = ticketService;

        }
        [BindProperty]
        public List<Ticket> OpenTickets { get; set; } // Listelenecek a��k ticket'lar.
        public void OnGet()
        {
            OpenTickets = _ticketService.FindOpenTicket(); // Stat�s� OPEN olan ticketlar.
            OpenTickets = OpenTickets.OrderByDescending(x => x.TicketCreatedDate).ToList(); // Tarihe g�re s�rala.
        }
    }
}
