using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
namespace SimpleCrud.Client.Services.PersonServices
{
    public class PersonService : IPersonService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public PersonService(HttpClient http,NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public List<Person> Persons { get; set; } = new List<Person>();
        public List<Occupation> Occupations { get; set; } = new List<Occupation>();
        public List<Company> Companies { get; set; } = new List<Company>();

        public async Task GetCompanies()
        {
            var companiesList = await _http.GetFromJsonAsync<List<Company>>("api/person/company");
            if (companiesList != null)
            {
                Companies = companiesList;
            }

        }

        public async Task GetOccupations()
        {
            var occupationsList = await _http.GetFromJsonAsync<List<Occupation>>("api/person/occupation");
            if (occupationsList != null)
            {
                Occupations = occupationsList;
            }

        }

        public async Task GetPersons()
        {
            var personsList = await _http.GetFromJsonAsync<List<Person>>("api/person");
            if (personsList != null)
            {
                Persons = personsList;
            }

        }

        public async Task SetPerson(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Person>>();
            Persons = response;
            _navigationManager.NavigateTo("persons");
        }

        public async Task<Person> GetSinglePerson(int id)
        {
            var result = await _http.GetFromJsonAsync<Person>($"api/person/{id}");
            if(result != null)
            {
                return result;
            }
            throw new Exception("Person not found");
        }

        public async Task UpdatePerson(Person person)
        {
            var result = await _http.PutAsJsonAsync($"api/person/{person.Id}",person);
            await SetPerson(result);
            //throw new Exception("Something when wrong");
        }
        public async Task CreatePerson(Person person)
        {
            var result = await _http.PostAsJsonAsync("api/person",person);
            await SetPerson(result);
            //throw new NotImplementedException();
        }

        public async Task DeletePerson(int id)
        {
            var result = await _http.DeleteAsync($"api/person/{id}");
            await SetPerson(result);
            //throw new NotImplementedException();
        }
    }
}