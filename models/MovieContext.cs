using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


public class Context : IdentityDbContext<User>
{
    public Context(DbContextOptions<Context> options)
        : base(options)
    {
    }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Director> Directors { get; set; }

    public DbSet<User> Users {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany(u => u.reviews)
            .WithOne(r => r.user)
            .HasForeignKey(r => r.UserId);

        modelBuilder.Entity<Movie>()
            .HasMany(m => m.Reviews)
            .WithOne(r => r.movie)
            .HasForeignKey(r => r.MovieId);

        modelBuilder.Entity<Movie>()
            .HasOne(m => m.Director)
            .WithMany(d => d.Movies)
            .HasForeignKey(m => m.DirectorId);


    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        if (!optionsBuilder.IsConfigured){
            optionsBuilder.UseSqlite("Data source=database.db");
        }
    }

}

    