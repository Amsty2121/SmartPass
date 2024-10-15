using Microsoft.EntityFrameworkCore;
using SmartPass.Repository.Models.Entities;

namespace SmartPass.Repository.Contexts
{
    public class SmartPassContext : DbContext
    {
        public SmartPassContext(DbContextOptions<SmartPassContext> options) : base(options)
        { }

        public DbSet<CardReader> CardReaders { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<AccessCard> AccessCards { get; set; }
        public DbSet<AccessLevel> AccessLevels { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
