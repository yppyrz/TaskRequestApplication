using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskRequestApplication.Models;
using TaskRequestApplication.Repositories;
using TaskRequestApplication.Services;

namespace TaskRequestApplication.Pages.ReviewTasks
{
    public class ReviewTasksPageModel : PageModel
    {
        private readonly CustomerRepository _customerRepository;
        private readonly CustomerService _customerService;

        private readonly EmployeeRepository _employeeRepository;

        private readonly TicketRepository _ticketRepository;
        private readonly TicketService _ticketService;

        public ReviewTasksPageModel(CustomerRepository customerRepository, CustomerService customerService, TicketRepository ticketRepository, TicketService ticketService, EmployeeRepository employeeRepository)
        {
            _customerRepository = customerRepository;
            _customerService = customerService;
            _ticketRepository = ticketRepository;
            _ticketService = ticketService;
            _employeeRepository = employeeRepository;
        }

        [BindProperty]
        public List<Ticket> tickets { get; set; }
        [BindProperty]
        public Ticket[] Tickets { get; set; }
        [BindProperty]
        public List<string> empList { get; set; } = new List<string>();
        [BindProperty]
        public List<Employee> Employees { get; set; } = new List<Employee>();
        [BindProperty]
        public Employee[] employees { get; set; }
        [BindProperty]
        public string id { get; set; }
        [BindProperty]
        public Ticket ticket { get; set; }
        [BindProperty]
        public List<Ticket> ticketlist { get; set; } = new List<Ticket>();



        public void OnGet()
        {
            var ticketList = _ticketRepository.GetAllTicket();
            tickets = ticketList.Where(x => x.TicketStatus == TicketStatusType.Review).ToList();
            tickets = tickets.OrderByDescending(x => x.TicketClosedDate).ToList();
            Tickets = tickets.ToArray();

            foreach (var item in tickets)
            {
                var x = item.EmployeeID;
                empList.Add(x);
            }
            foreach (var item in empList)
            {
                Employees.Add(_employeeRepository.FindEmployee(item));
            }
            employees = Employees.ToArray();
        }
        public void OnPostSave(string Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    id = Id;
                    ticket = _ticketRepository.FindTicket(id);
                    var employeeId = ticket.EmployeeID;
                    var employee = _employeeRepository.FindEmployee(employeeId);
                    _ticketService.EmployeeTickets(employee);
                    foreach (var tickets in employee.ticketsID)
                    {
                        var t = _ticketRepository.FindTicket(tickets);
                        if (t.TicketCreatedDate <= DateTime.Now.AddMonths(1))
                        {
                            ticketlist.Add(t);
                        }
                    }
                    bool control = ticketlist.Any(x => x.TicketPriority > ticket.TicketPriority);
                    if (control)
                    {
                        throw new Exception("Bu iþ kapatýlamaz.");
                    }
                    _ticketService.ClosedTicket(ticket);

                    var result = _ticketRepository.FindTicket(ticketId: ticket.TicketID); // Ticket database'de var mý

                    if (result.TicketStatus == TicketStatusType.Closed)
                    {
                        ViewData["Message"] = "Kayýt Baþarýlýdýr";
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
