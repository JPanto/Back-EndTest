using Back_EndTest.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Back_EndTest.Models
{
    public class Status
    {
        public Status()
        {
            Tickets = new HashSet<Ticket>();
        }
        private Status(StatusEnum @enum)
        {
            id_status = (int)@enum;
            status = @enum.ToString();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_status { get; set; }
        public string status { get; set; }

        public static implicit operator Status(StatusEnum @enum) => new Status(@enum);
        public static implicit operator StatusEnum(Status status) => (StatusEnum)status.id_status;
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
