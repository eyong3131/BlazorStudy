using System.Net.Http.Json;
namespace TestDrive.Client.Services.TestDriveServices
{
    public class TestDriveService : ITestDriveService
    {
        private readonly HttpClient _http;
        public TestDriveService(HttpClient http)
        {
            _http = http;
        }

        public List<MiddleWare> MyList {get;set;} = new List<MiddleWare>();
        public async Task GetMyList()
        {
            MyList = await _http.GetFromJsonAsync<List<MiddleWare>>("api/MyTestDrive");
        }
    }
}