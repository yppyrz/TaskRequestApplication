using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskRequestApplication.Models;
using TaskRequestApplication.Repositories;
using TaskRequestApplication.Services;

namespace TaskRequestApplication.Pages.ClosedTasks
{
    public class ClosedTasksPageModel : PageModel
    {
        private readonly CustomerRepository _customerRepository;
        private readonly CustomerService _customerService;

        private readonly EmployeeRepository _employeeRepository;

        private readonly TicketRepository _ticketRepository;
        private readonly TicketService _ticketService;
        public ClosedTasksPageModel(CustomerRepository customerRepository, CustomerService customerService, TicketRepository ticketRepository, TicketService ticketService, EmployeeRepository employeeRepository)
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
        public Ticket ticket { get; set; }

        [BindProperty]
        public List<string> empList { get; set; } = new List<string>();
        [BindProperty]
        public List<Employee> Employees { get; set; } = new List<Employee>();
        [BindProperty]
        public Employee[] employees { get; set; }

        [BindProperty]
        public string id { get; set; }

        public void OnGet()
        {
            var ticketList = _ticketRepository.GetAllTicket();
            tickets = ticketList.Where(x => x.TicketStatus == TicketStatusType.Closed).ToList();
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
        public void OnPostReview(string Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ticket = _ticketRepository.FindTicket(id);
                    _ticketService.ReviewTicket(ticket);

                    var result = _ticketRepository.FindTicket(ticketId: ticket.TicketID); // Ticket database'de var mý

                    if (result.TicketStatus == TicketStatusType.Review)
                    {
                        ViewData["Message"] = "Kayýt Baþarýlýdýr";
                    }
                    else
                    {
                        ViewData["Message"] = "Tekrar deneyiniz";
                    }

                    var employeeid = ticket.EmployeeID;
                    var employee = _employeeRepository.FindEmployee(employeeid);
                    var employeeMailAddress = employee.EmployeeMailAddress; // Employee'ye review maili atýlacak.

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Hata", ex.Message);
                    ViewData["Message"] = "Tekrar deneyiniz";
                }
            }

        }
        public void OnPostCompleted(string id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ticket = _ticketRepository.FindTicket(id);
                    _ticketService.CompletedTicket(ticket);


                    var result = _ticketRepository.FindTicket(ticketId: ticket.TicketID); // Ticket database'de var mý

                    if (result.TicketStatus == TicketStatusType.Completed)
                    {
                        ViewData["Message"] = "Kayýt Baþarýlýdýr";
                    }
                    else
                    {
                        ViewData["Message"] = "Tekrar deneyiniz";
                    }

                    var customerid = ticket.TicketCustomerID;
                    var customer = _customerRepository.FindCustomer(customerid);
                    var customerMailAddress = customer.CustomerMailAddress; // Customer'a completed maili atýlacak.

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
