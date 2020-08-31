using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace TodoApi.Models
{
    public class ToDoListContext : DbContext
    {
    // ToDoListContext
        public ToDoListContext(DbContextOptions<ToDoListContext> options) : base(options)
        {
        }

        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<ListItem> ListItems { get; set; }
    }
}