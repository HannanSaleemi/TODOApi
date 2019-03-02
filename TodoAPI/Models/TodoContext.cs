using System;
using Microsoft.EntityFrameworkCore;
namespace TodoAPI.Models
{

    // DATABASE CONTEXT
    // THE DATABSE CONTEXT IS THE MAIN CLASS THAT COORDINATES ENTITY FRAMEWORK FUNCTIONALITY FOR A DATA MODEL.
    // THIS CLASS IS CREATED BY DERIVING FROM THE Micorsoft.EntityFrameworkCore.DbContext CLASS.

    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            :base(options)
        {
            
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}