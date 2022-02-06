using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FakeYouTubeApi.Models;

namespace FakeYouTubeApi.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Contact> Contact { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {            
        base.OnModelCreating(builder);
    }
}