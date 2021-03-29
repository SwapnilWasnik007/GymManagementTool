using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GymManagement.Web.Models.DBModel
{
    public class Users
    {
        [Key]
        public Guid Id { get; set; }
        public int RegistrationNumber { get; set; }
        public string Name { get; set; }
        public long Mobile { get; set; }
        public string Batch { get; set; }
        public string Address { get; set; }
        public string CurrentPackage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double BalanceAmount { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
    }
}
