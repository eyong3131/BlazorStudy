namespace SimpleCrud.Client.Services.PersonServices
{
    public interface IPersonService
    {
        List<Person> Persons {get;set;}
        List<Occupation> Occupations {get;set;}
        List<Company> Companies {get;set;}
        Task GetCompanies();
        Task GetOccupations();
        Task GetPersons();
        Task<Person> GetSinglePerson(int id);
        Task UpdatePerson(Person person);
        Task CreatePerson(Person person);
        Task DeletePerson(int id);
    }
}