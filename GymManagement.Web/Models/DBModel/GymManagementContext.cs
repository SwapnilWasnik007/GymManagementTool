using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymManagement.Web.Models.DBModel
{
    public class GymManagementContext : DbContext
    {
        public GymManagementContext(DbContextOptions<GymManagementContext> options) : base(options)
        {
        }


        public DbSet<Users> Users { get; set; }
        public DbSet<UserFees> UserFees { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
    }
}
