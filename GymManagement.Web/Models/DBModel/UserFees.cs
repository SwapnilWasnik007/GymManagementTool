using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GymManagement.Web.Models.DBModel
{
    public class UserFees
    {
        [Key]
        public Guid Id { get; set; }
        public double TotalAmount { get; set; }
        public double PaidAmount { get; set; }
        public double BalanceAmount { get; set; }
        [ForeignKey("Users")]
        public Guid UserId { get; set; }
        public DateTime PaidDate { get; set; }
        public string Package { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }

        public Users Users { get; set; }

    }
}
