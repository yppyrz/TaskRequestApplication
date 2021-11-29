using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskRequestApplication.Models;
using TaskRequestApplication.Repositories;
using TaskRequestApplication.Services;

namespace TaskRequestApplication.Pages.TicketForm
{
    public class TicketCreateModel : PageModel
    {
        private readonly CustomerRepository _customerRepository;
        private readonly CustomerService _customerService;

        private readonly TicketRepository _ticketRepository;
        private readonly TicketService _ticketService;

        public TicketCreateModel(CustomerRepository customerRepository, CustomerService customerService, TicketRepository ticketRepository, TicketService ticketService)
        {
            _customerRepository = customerRepository;
            _customerService = customerService;
            _ticketRepository = ticketRepository;
            _ticketService = ticketService;
        }


        [BindProperty]
        public string Subject { get; set; } // Ticket subject
        [BindProperty]
        public string Description { get; set; } // Ticket description
        [BindProperty]
        public string SelectedCustomerID { get; set; } // Seçilen customer'ýn id'si
        [BindProperty]
        public List<SelectListItem> Customers { get; set; } = new List<SelectListItem>(); // Sistemde kayýtlý olan customer'lar
        [BindProperty]
        public List<Customer> CustomerList { get; set; } // Sistemde kayýtlý olan customer'lar

        public void OnGet()
        
        {
            CustomerList = _customerRepository.GetAllCustomer();
            Customers = CustomerList.Select(a => new SelectListItem
            {
                Value = a.CustomerID,
                Text = a.CustomerName
            }).ToList(); // Kayýtlý customer'larý listeledik.
        }

        public void OnPostSave()
        {
            if (ModelState.IsValid)
            {

                try
                {

                    var ticket = new Ticket();
                    ticket.TicketSubject = Subject;
                    ticket.TicketDescription = Description;
                    ticket.TicketCustomerID = SelectedCustomerID;
                    _ticketService.AddTicket(ticket);
                    // Customer'a mail atýlacak.
                    var SelectedCustomer = _customerRepository.FindCustomer(SelectedCustomerID);
                    _customerService.AddTicket(SelectedCustomer, ticket);
                    var result = _ticketRepository.FindTicket(ticketId: ticket.TicketID); // Ticket database'de var mý

                    if (result != null)
                    {
                        ViewData["Message"] = "Kayýt Baþarýlýdýr";
                    }
                    else
                    {
                        ViewData["Message"] = "Tekrar deneyiniz";
                    }
                     
                    var emailCustomer = SelectedCustomer.CustomerMailAddress; // Customer mail, TicketID mail atýlacak...
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
