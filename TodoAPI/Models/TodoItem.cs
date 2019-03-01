using System;
namespace TodoAPI.Models
{

    // THIS IS AN EXAMPLE OF A MODEL CLASS
    // A MODEL CLASS IS A SET OF CLASSES THAT REPRESENT THE DATA THAT THE APP MANAGES.
    // THE MODEL FOR THIS APP IS A SINGLE TodoItem CLASS.

    public class TodoItem
    { 
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }

    }
}