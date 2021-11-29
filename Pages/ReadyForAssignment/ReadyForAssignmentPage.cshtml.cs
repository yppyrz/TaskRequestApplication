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

namespace TaskRequestApplication.Pages.ReadyForAssignment
{
    public class ReadyForAssignmentPageModel : PageModel
    {
        private readonly CustomerRepository _customerRepository;
        private readonly CustomerService _customerService;

        private readonly EmployeeRepository _employeeRepository;
        private readonly EmployeeService _employeeService;

        private readonly TicketRepository _ticketRepository;
        private readonly TicketService _ticketService;
        public ReadyForAssignmentPageModel(CustomerRepository customerRepository, CustomerService customerService, TicketRepository ticketRepository, TicketService ticketService, EmployeeRepository employeeRepository,EmployeeService employeeService)
        {
            _customerRepository = customerRepository;
            _customerService = customerService;
            _ticketRepository = ticketRepository;
            _ticketService = ticketService;
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
        }

        [BindProperty]
        public List<Ticket> tickets { get; set; } = new List<Ticket>();
        [BindProperty]
        public Ticket[] Tickets { get; set; }
        [BindProperty]
        public Ticket ticket { get; set; }
        [BindProperty]
        public string SelectedTicketID { get; set; }
        [BindProperty]
        public List<Ticket> ticketlist { get; set; } = new List<Ticket>();


        [BindProperty]
        public List<SelectListItem> Employeee { get; set; } = new List<SelectListItem>();
        [BindProperty]
        public List<Employee> EmployeeList { get; set; } = new List<Employee>();
        [BindProperty]
        public string SelectedEmployeeID { get; set; }
        [BindProperty]
        public Employee employee { get; set; }



        public int difficultyTicketNumber { get; set; }
        public int priorityTicketNumber { get; set; }

        public void OnGet()
        {
            var ticketList = _ticketRepository.GetAllTicket();
            tickets = ticketList.Where(x => x.TicketStatus == TicketStatusType.ReadyForAssignment).ToList();// Statüsü ReadyForAssignment olanlar listelendi.
            Tickets = tickets.ToArray();// Ticket sýralanmak için dizi haline getirildi.

            EmployeeList = _employeeRepository.GetAllEmployee();
            Employeee = EmployeeList.Select(a => new SelectListItem
            {
                Value = a.EmployeeID,
                Text = a.EmployeeName
            }).ToList();//Ticket'a employee atamak için database'de bulunan employee'ler listelendi.

        }
       
        public void OnPostSave()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    employee = _employeeRepository.FindEmployee(SelectedEmployeeID);// alttaki ticket hata veriyo.
                    ticket = _ticketRepository.FindTicket(SelectedTicketID);
                    _ticketService.EmployeeTickets(employee);
                    var hour = ticket.TicketPriority * 8;
                    if (employee.ticketsID.Count == 0)
                    {
                        employee.WorkingHour += hour;
                        _employeeRepository.AddEmployeeTickets(employee,SelectedTicketID);
                        employee.ticketsID.Add(SelectedTicketID); // Employee'ye ticket atanýr.
                        _employeeRepository.UpdateEmployee(employee);
                        _ticketService.AssignedEmployee(ticket: ticket, id: SelectedEmployeeID);
                    }
                    else
                    {
                        foreach (var tickets in employee.ticketsID)
                        {
                            var t = _ticketRepository.FindTicket(tickets);
                            if (t.TicketCreatedDate <= DateTime.Now.AddMonths(1))
                            {
                                ticketlist.Add(t);
                            }
                        }

                        var diffucltyTicketList = ticketlist.Where(x => x.TicketDifficultyLevel == "Zor" || x.TicketDifficultyLevel == "Çok Zor");
                        var priorityTicketList = ticketlist.Where(x => x.TicketPriority == 4 || x.TicketPriority == 5);

                        difficultyTicketNumber = diffucltyTicketList.ToArray().Length;
                        priorityTicketNumber = priorityTicketList.ToArray().Length;

                        _employeeService.CheckEmployeeWorks(employee: employee, difficulty: difficultyTicketNumber, priority: priorityTicketNumber);


                        employee.WorkingHour += hour;
                        employee.ticketsID.Add(SelectedTicketID); // Employee'ye ticket atanýr.
                        _employeeRepository.UpdateEmployee(employee);
                        _ticketService.AssignedEmployee(ticket: ticket, id: SelectedEmployeeID);
                    }
                    
                    var result = _ticketRepository.FindTicket(ticketId: ticket.TicketID); // Ticket database'de var mý
                    
                    if (result.TicketStatus == TicketStatusType.Assigment)
                    {
                        ViewData["Message"] = "Kayýt Baþarýlýdýr";
                    }
                    else
                    {
                        ViewData["Message"] = "Tekrar deneyiniz";
                    }

                    var employeeMailAddress = employee.EmployeeMailAddress; // Employee'ye assignde maili atýlabilir.
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
