using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TaskServiceAPI.DAL_Lyr
{
    public partial class TaskDBContext : DbContext
    {
        public TaskDBContext()
            : base("name=TasksDBEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }

        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<tblTask> tblTasks { get; set; }
        public virtual DbSet<tblUser> tblUsers { get; set; }
    }
}