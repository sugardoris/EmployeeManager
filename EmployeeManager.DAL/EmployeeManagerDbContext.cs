using EmployeeManager.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.DAL
{
    public class EmployeeManagerDbContext : IdentityDbContext<AppUser>
    {
        public EmployeeManagerDbContext(DbContextOptions<EmployeeManagerDbContext> options) : base(options)
        {
            
        }
        
        public DbSet<City> Cities { get; set; }
        public DbSet<Gameweek> Gameweeks { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentContract> StudentContracts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<City>().HasData(new City() {Id = 1, Name = "Zagreb"});
            modelBuilder.Entity<City>().HasData(new City() {Id = 2, Name = "Split"});
            modelBuilder.Entity<City>().HasData(new City() {Id = 3, Name = "Rijeka"});
            modelBuilder.Entity<City>().HasData(new City() {Id = 4, Name = "Zadar"});
            modelBuilder.Entity<City>().HasData(new City() {Id = 5, Name = "Osijek"});

            modelBuilder.Entity<League>().HasData(new League() 
                {Id = 1, Name = "Prva hrvatska nogometna liga", NumberOfGameweeks = 36});
            modelBuilder.Entity<League>().HasData(new League() 
                {Id = 2, Name = "Premier liga", NumberOfGameweeks = 38});
            modelBuilder.Entity<League>().HasData(new League() 
                {Id = 3, Name = "Serie A", NumberOfGameweeks = 40});
            modelBuilder.Entity<League>().HasData(new League() 
                {Id = 4, Name = "French Ligue 1", NumberOfGameweeks = 38});
            modelBuilder.Entity<League>().HasData(new League() 
                {Id = 5, Name = "Bundesliga", NumberOfGameweeks = 38});
            
        }
    }
}