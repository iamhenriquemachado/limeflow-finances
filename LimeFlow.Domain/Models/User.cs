using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }

        public User(Guid id, string name, string email, string password, DateTime createdAt, DateTime lastUpdatedAt)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            CreatedAt = createdAt;
            LastUpdatedAt = lastUpdatedAt;
        }
    }
}
