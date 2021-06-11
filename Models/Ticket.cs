using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Back_EndTest.Models
{
    public class Ticket
    {
        [Key]
        public int id_ticket { get; set; }
        public string user { get; set; }
        public DateTime creation_date { get; set; }
        public int id_status_fk { get; set; }

        public virtual Status status { get; set; }


    }
}
