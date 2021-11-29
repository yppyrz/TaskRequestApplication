using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskRequestApplication.Repositories;

namespace TaskRequestApplication.Services
{
    public class ManagerService
    {
        ManagerRepository _managerRepository;

        public ManagerService(ManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }
    }
}
