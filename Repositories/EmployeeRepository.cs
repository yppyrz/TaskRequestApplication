using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskRequestApplication.Models;

namespace TaskRequestApplication.Repositories
{
    public class EmployeeRepository
    {
        private readonly TaskRequestApplicationDBContext _db; // Database.

        public EmployeeRepository()
        {
            _db = new TaskRequestApplicationDBContext(); // Repository için db'den instance alındı.
        }

        public List<string> employeeTickets { get; set; } = new List<string>();


        /// <summary>
        /// Database'de bulunan Employee tablosundan id üzerinden sorgulama sağlar.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Employee FindEmployee(string employeeId)
        {
            return _db.Employees.Find(employeeId);
        }

        /// <summary>
        /// Database'de bulunan Employee tablosunu liste olarak erişim olanağı sağlar. 
        /// </summary>
        /// <returns></returns>
        public List<Employee> GetAllEmployee()
        {
            return _db.Employees.ToList();
        }

        /// <summary>
        /// Employee'nin ticketId listesine id ekler.
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="id"></param>
        public void AddEmployeeTickets(Employee employee,string id)
        {
            employeeTickets.Add(id);

        }

        /// <summary>
        /// Database'de employee tablosunu günceller.
        /// </summary>
        /// <param name="employee"></param>
        public void UpdateEmployee(Employee employee)
        {
            _db.Employees.Update(employee);
            _db.SaveChanges();
        }
    }
}
