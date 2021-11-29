using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskRequestApplication.Models;
using TaskRequestApplication.Repositories;

namespace TaskRequestApplication.Services
{
    public class EmployeeService
    {
        EmployeeRepository _employeeRepository; // Employee database'e bağlanır.


        public EmployeeService(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository; // Database işlemleri için bağlantı
        }

        /// <summary>
        /// Employee'ye task atamadan önce kontrol eder.
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="difficulty"></param>
        /// <param name="priority"></param>
        public void CheckEmployeeWorks(Employee employee,int difficulty,int priority)
        {
            var hour = priority * 8;
            var hours = employee.WorkingHour;
            var controlHours = hours + hour;

            if (difficulty >= 3)
            {
                throw new Exception("İş eklenemez.");
            }
            if (priority >= 4)
            {
                throw new Exception("İş eklenemez.");
            }
            if (controlHours >= 160)
            {
                throw new Exception("İş eklenemez.");
            }
        }
    }
}
