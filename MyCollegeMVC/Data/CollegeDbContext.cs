using Microsoft.EntityFrameworkCore;
using MyCollegeMVC.Models;

namespace MyCollegeMVC.Data
{
    public class CollegeDbContext : DbContext
    {
        public CollegeDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<College> CollegeData { get; set; }
        public DbSet<Department> DepartmentData { get; set; }

        public DbSet<MyCollegeMVC.Models.AddCollege> AddCollege { get; set; }

        public DbSet<MyCollegeMVC.Models.UpdateCollege> UpdateCollege { get; set; }
    }
}
