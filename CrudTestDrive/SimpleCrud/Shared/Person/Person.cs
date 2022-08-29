namespace SimpleCrud.Shared.Person
{
    public class Person
    {
        public int Id {get; set;}
        public string FirstName {get;set;} = string.Empty;
        public string LastName {get;set;} = string.Empty;
        public Occupation? Occupation {get;set;}
        public int OccupationId {get;set;}
        public Company? Company {get;set;}
        public int CompanyId {get;set;}
    }
}