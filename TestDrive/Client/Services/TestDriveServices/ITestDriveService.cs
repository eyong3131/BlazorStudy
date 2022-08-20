namespace TestDrive.Client.Services.TestDriveServices
{
    public interface ITestDriveService
    {
        List<MiddleWare> MyList {get; set;}

        Task GetMyList();
    }
}