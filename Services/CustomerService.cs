using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskRequestApplication.Models;
using TaskRequestApplication.Repositories;

namespace TaskRequestApplication.Services
{
    public class CustomerService
    {
        CustomerRepository _customerRepository; // Customer repository'e bağlar

        public CustomerService(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository; // Customer işlemleri için repository'den instance alınır.
        }

        /// <summary>
        /// Repository üzerinden database'e Customer ekler.
        /// </summary>
        /// <param name="customer"></param>
        public void AddCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new Exception("Customer boş olamaz.");
            }
            if (string.IsNullOrEmpty(customer.CustomerName) || string.IsNullOrEmpty(customer.CustomerMailAddress))
            {
                throw new Exception("Customer bilgisi boş olamaz.");
            }

            customer.CustomerName = customer.CustomerName.Trim();
            customer.CustomerMailAddress = customer.CustomerMailAddress.Trim();

            _customerRepository.AddCustomer(customer);
        }

        /// <summary>
        /// Customer'a ticket ekler.
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="ticket"></param>
        public void AddTicket(Customer customer,Ticket ticket)
        {
            customer.tickets.Add(ticket);
        }
    }
}
