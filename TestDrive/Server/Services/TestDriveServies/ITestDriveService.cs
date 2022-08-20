namespace TestDrive.Server.Services.TestDriveServices
{
        public interface ITestDriveService
    { 
        Task<List<MiddleWare>> GetAll();
    }
}