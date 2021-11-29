using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskRequestApplication.Models
{
    public class Employee
    {
        public string EmployeeID { get; set; } = Guid.NewGuid().ToString(); // Employee'nin sistemdeki ID numarası.
        public string EmployeeName { get; set; } // Employee'nin sistemde kayıtlı ismi.
        public string EmployeeMailAddress { get; set; } // Employee'nin sistemde kayıtlı mail adresi.
        public int WorkingHour { get; set; } = 0; // Çalışma saati

        public List<string> ticketsID = new List<string>(); // Employee'ye at ticket listesi.
        public string managerId { get; set; } // Employee nin manageri
    }
}
