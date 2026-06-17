using System;
using Microsoft.EntityFrameworkCore;
using LimeFlow.Domain;
using LimeFlow.Domain.Models;

namespace LimeFlow.Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
