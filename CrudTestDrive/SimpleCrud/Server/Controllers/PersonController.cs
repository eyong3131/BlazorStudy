using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SimpleCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly DataContext _context;
        public PersonController(DataContext context)
        {
            _context = context;
        }
        /*
        * Default data Getters when initializing list of person
        *
        */
        [HttpGet]
        public async Task<ActionResult<List<Person>>> GetPersons()
        {
            /*
            * await means wait for the result of object Persons because its still
            * getting information from the server or cloud
            * Include all relatational data like in SQL query but in  
            * EntityFrameWorkCore method style
            */
            var PersonList = await _context.Persons
                .Include(sh => sh.Company)
                .Include(sh => sh.Occupation)
                .ToListAsync();
            return Ok(PersonList);
        }
        /*
        *
        * Getting Companies, this is also called when we are trying to list 
        * Information about person Occupation and what Company he/she work.
        * We use ToListAsync since we needed it to be a list and also we
        * need the program to wait for the Information coming from server/cloud.
        * async is always used when dealing with information with a little delay
        */
        [HttpGet("company")]
        public async Task<ActionResult<List<Company>>> GetCompanies()
        {
            var CompanyList = await _context.Companies.ToListAsync();
            return Ok(CompanyList);
        }
        [HttpGet("occupation")]
        public async Task<ActionResult<List<Occupation>>> GetOccupations()
        {
            var occupationList = await _context.Occupations.ToListAsync();
            return Ok(occupationList);
        }

        /*
        * Getting Single Person from the database, we needed the Id passed from 
        * Parameter from Client Services (root is from pages)
        */
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetSinglePerson(int id)
        {
            /*
            * we tell the dotnet that we needed to wait the person object and
            * Include relational data 
            * FirstorDefaultAsync is part of EntityFramWork 
            * to learn more about EF See 
            * Querying data with EF Core for more information.
            */
            var SinglePerson = await _context.Persons
                .Include(h => h.Company)
                .Include(h => h.Occupation)
                .FirstOrDefaultAsync(h => h.Id == id);
            if (SinglePerson == null)
            {
                /*
                * we just need a default output so the program 
                * do not crash
                */
                return NotFound("Person Not Found");
            }
            /*
            * status 200
            */
            return Ok(SinglePerson);
        }
        /*
        * We need to post Information to the server
        * we used the default API link but with post method.
        * we also need to create an object as a container for the new 
        * information we are about to Post to the Server
        * I don't know if this is the standard way of Inserting new 
        * Information but I only follow what others do.
        */
        [HttpPost]
        public async Task<ActionResult<Person>> CreatePerson(Person person)
        {
            /*
            * This is a static Information post,
            * I needed to modify this part so that we can add 
            * Company and Occupation dynamical
            * Do this later on
            *
            */
            person.Company = null;
            person.Occupation = null;

            /*
            * don't get confused here, 
            * from Person base Object 
            * to Persons inherit Object
            *
            *
            */

            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return Ok(await GetDbPersons());
        }

        /*
        *
        * prety much self explenatory code here on
        * but we used
        * HttpGet/API
        * to update information from the server.
        * again I don't know if this is safe or the standard way of doing this
        * but I just follow what others do here.
        *
        */

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Person>>> UpdatePerson(Person person, int id)
        {
            /*
            * we are getting information from the database here,
            * we are including relational data in one query and we are
            * checking if the Id matches the id requested in client side
            *
            */
            var dbPerson = await _context.Persons
                .Include(sh => sh.Occupation)
                .Include(sh => sh.Company)
                .FirstOrDefaultAsync(sh => sh.Id == id);
            if (dbPerson == null)
            {
                return NotFound("Person not found on database");
            }
            /*
            *
            * overwrite the information given by the database but not including the 
            * person ID. again I needed to modify this part so that we can
            * add Company and Occupation Dynamically
            * doing this later
            *
            */
            dbPerson.FirstName = person.FirstName;
            dbPerson.LastName = person.LastName;
            dbPerson.OccupationId = person.OccupationId;
            dbPerson.CompanyId = person.CompanyId;

            await _context.SaveChangesAsync();

            return Ok(await GetDbPersons());
        }
        /*
        * we use HttpDelete/API to delete information from the database
        * unlike update, we only need to tell the program to delete a column in table
        * " Remove() " 
        *
        */
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Person>>> DeletePerson(int id)
        {
            var dbPerson = await _context.Persons
                .Include(sh => sh.Occupation)
                .Include(sh => sh.Company)
                .FirstOrDefaultAsync(sh => sh.Id == id);
            if (dbPerson == null)
            {
                return NotFound("Person not found on database");
            }
            _context.Persons.Remove(dbPerson);
            await _context.SaveChangesAsync();

            return Ok(await GetDbPersons());
        }

        /*
        *
        * Simple getting information from the server for other function that 
        * requires it
        *
        *
        */

        private async Task<List<Person>> GetDbPersons()
        {
            /*
            *
            * again, do not get confused on 
            * Person base Object 
            * to
            * Persons inherit Object
            *
            */
            return await _context.Persons
                .Include(h => h.Occupation)
                .Include(h => h.Company)
                .ToListAsync();
        }
        /*
        *
        * We are now creating dynamic input for relational database
        *
        *
        */
        private async Task<List<Company>> GetDbCompany()
        {
            return await _context.Companies.ToListAsync();
        }
        private async Task<List<Occupation>> GetDbOccupation()
        {
            return await _context.Occupations.ToListAsync();
        }
}
}