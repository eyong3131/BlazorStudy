namespace SimpleCrud.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<Company>().HasData(
                new Company { Id = 1, CompanyName = "Microsoft" },
                new Company { Id = 2, CompanyName = "Google" }
            );
            modelBuilder.Entity<Occupation>().HasData(
                new Occupation {Id = 1, OccupationName = "Programmer"},
                new Occupation {Id = 2, OccupationName = "Tech Support"}
            );
            modelBuilder.Entity<Person>().HasData(
                new Person {
                    Id = 1,
                    FirstName = "Leo",
                    LastName = "De Guzman",
                    OccupationId = 1,
                    CompanyId = 1
                },
                new Person {
                    Id = 2,
                    FirstName = "Angelika",
                    LastName = "Bruto",
                    OccupationId = 2,
                    CompanyId = 2
                }
            );
        }
        public DbSet<Person> Persons {get;set;}
        public DbSet<Occupation> Occupations {get;set;}
        public DbSet<Company> Companies {get;set;}
    }
}