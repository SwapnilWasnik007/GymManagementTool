using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GymManagement.Web.Models.DBModel
{
    public class Attendance
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Users")]
        public Guid UserId { get; set; }
        public int RegistrationNumber { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedOn { get; set; }

        public Users Users { get; set; }
    }
}
