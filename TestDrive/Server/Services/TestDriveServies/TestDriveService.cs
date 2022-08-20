namespace TestDrive.Server.Services.TestDriveServices
{
    public class TestDriveService : ITestDriveService
    {
        private readonly DataContext _context;
        public TestDriveService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<MiddleWare>> GetAll()
        {
            var TestDrive = await _context.MyList.ToListAsync();
            return TestDrive;
        }
    }
}