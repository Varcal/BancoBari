using System;


namespace BancoBari.Domain.Entities
{
    public class HelloWorld
    {
        public Guid Id { get; set; }
        public string Description { get; set; }



        public HelloWorld()
        {
            Id = Guid.NewGuid();
            Description = "HelloWorld";
        }
    }
}
