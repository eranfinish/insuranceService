using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Models
{
    public class InsurancePolicy
    {
        public int ID { get; set; }
        public string PolicyNumber { get; set; }
        public decimal InsuranceAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserID { get; set; }
        public User? User { get; set; }
    }

}
