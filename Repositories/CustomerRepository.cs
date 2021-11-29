using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskRequestApplication.Models;

namespace TaskRequestApplication.Repositories
{
    public class CustomerRepository
    {
        private readonly TaskRequestApplicationDBContext _db; // Database.

        public CustomerRepository()
        {
            _db = new TaskRequestApplicationDBContext(); // Repository için db'den instance alındı.
        }

        /// <summary>
        /// Database'e Customer ekler.
        /// </summary>
        /// <param name="customer"></param>
        public void AddCustomer(Customer customer)
        {
            _db.Customers.Add(customer);
            _db.SaveChanges();
        }

        /// <summary>
        /// Database'de bulunan Customer tablosundan CustomerName üzerinden sorgulama sağlar.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Customer FindCustomer(string customerId)
        {
            return _db.Customers.Find(customerId);
        }

        /// <summary>
        /// Database'de bulunan Customer tablosunu liste olarak erişim olanağı sağlar. 
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetAllCustomer()
        {
            return _db.Customers.ToList();
        }
    }
}
