using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskRequestApplication.Models;

namespace TaskRequestApplication.Repositories
{
    public class ManagerRepository
    {
        private readonly TaskRequestApplicationDBContext _db;

        public ManagerRepository()
        {
            _db = new TaskRequestApplicationDBContext();
        }

        /// <summary>
        /// Database'de bulunan manager'i id üzerinden bulunur
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public Manager FindManager(string managerId)
        {
            return _db.Managers.Find(managerId);
        }

        /// <summary>
        /// Database'de bulunan Manager tablosuna liste olarak erişim sağlatır.
        /// </summary>
        /// <returns></returns>
        public List<Manager> GetAllTicket()
        {
            return _db.Managers.ToList();
        }


        /// <summary>
        /// Database'de manager tablosunu günceller.
        /// </summary>
        /// <param name="ticket"></param>
        public void UpdateManager(Manager manager)
        {
            _db.Managers.Update(manager);
            _db.SaveChanges();
        }

    }
}
