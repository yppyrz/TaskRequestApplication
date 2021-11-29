using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskRequestApplication.Models
{
    public class Manager
    {
        public string ManagerID { get; set; } = Guid.NewGuid().ToString(); // Manager id number
        public string ManagerName { get; set; } // Manager name
        public string ManagerMailAddress { get; set; } // Manager mail adresi
        public List<string> employeeList { get; set; } // Manager'in yönettiği employee listesi
    }
}
