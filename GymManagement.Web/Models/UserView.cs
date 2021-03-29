using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymManagement.Web.Models
{
    public class UserView
    {
        public int RegistrationNumber { get; set; } //Used for Main datatable
        public string Name { get; set; }
        public long Mobile { get; set; }
        public string Batch { get; set; }
        public string Address { get; set; }
        public string Package { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalAmt { get; set; }
        public double PaidAmt { get; set; }
        public double BalanceAmt { get; set; }
    }
    public class DataTableUserView
    {
        public string RegistrationNumber { get; set; } //Used for Main datatable
        public string Name { get; set; }
        public long Mobile { get; set; }
        //public string Batch { get; set; }
        //public string Address { get; set; }
        public string Package { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public double BalanceAmt { get; set; }
    }


    public class UserPaginationModel
    {
        public string SortColumn { get; set; }
        public string SortColumnDirection { get; set; }
        public string SearchValue { get; set; }
        public int Skip { get; set; }
        public int PageSize { get; set; }
    }
}
