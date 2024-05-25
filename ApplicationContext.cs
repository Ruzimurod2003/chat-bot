using Microsoft.EntityFrameworkCore;

namespace ChatBot;
public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {

    }
    public DbSet<Statistic> Statistics { get; set; }
}