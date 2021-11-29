using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskRequestApplication.Models
{
    public enum TicketStatusType
    {
        OPEN=1,
        ReadyForAssignment=2,
        Assigment=3,
        Closed=4,
        Review=5,
        Completed=6
    }

    /// <summary>
    /// Müşterinin bir iş isteği oluşturması için girilen bilgileri tutan class
    /// </summary>
    public class Ticket
    {
        public string TicketID { get; set; } = Guid.NewGuid().ToString(); // Müşteri ticket oluşturduğunda otomatik olarak oluşturulur.
        public string TicketSubject { get;  set; } // Müşterinin iş için gireceği max 50 karakterlik konu.
        public string TicketDescription { get;  set; } // Müşterinin iş için gireceği max 500 karakterlik açıklama.

        public DateTime TicketCreatedDate { get;  set; } // Müşterinin ticket oluşturduğu tarih.
        public DateTime TicketAssigneeDate { get; set; } // Müşterinin ticket oluşturduğu tarih.
        public DateTime TicketReviewDate { get; set; } // Müşterinin ticket oluşturduğu tarih.
        public DateTime TicketClosedDate { get; set; } // Müşterinin ticket oluşturduğu tarih.
        public DateTime TicketCompletedDate { get; set; } // Müşterinin ticket oluşturduğu tarih.


        public TicketStatusType TicketStatus { get;  set; } // Ticket durumunu belirten tip.
        public string TicketCustomerID { get;  set; } // Ticket'ı oluşturan müşteri bilgisi.
        public string EmployeeID { get; set; } // Ticket'in atandığı employee
       
        public int TicketPriority { get; set; } // Ticket'in priority derecesi
        public string TicketDifficultyLevel { get; set; } // Ticket'in difficulty derecesi
   






    }
}
