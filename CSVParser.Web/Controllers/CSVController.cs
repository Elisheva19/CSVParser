using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using CSVParser.Data;
using System.Text;
using System.Globalization;
using System.IO;
using CsvHelper;
using CSVParser.Web.Models;

namespace CSVParser.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CSVController : ControllerBase
    {
     
        private readonly string _connectionString;

        public CSVController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }



        [HttpGet]

        [Route("generatecsv/{amount}")]
        
        public IActionResult GenerateCsv (int amount)
        {
            List<Person> ppl = GetPeople(amount);
            var csv = GetCsv(ppl);

            byte[] pplBytes = Encoding.UTF8.GetBytes(csv);
            return File(pplBytes, "text/csv", "people.csv");
        }

        [HttpPost]
        [Route("upload")]

        public void Upload(UploadViewModel vm)
        {
            int index = vm.Base64File.IndexOf(",") + 1;
            string base64 = vm.Base64File.Substring(index);
            byte[] filebytes = Convert.FromBase64String(base64);

            List<Person> people = GetFromCsvBytes(filebytes);
            var repo = new PeopleRepository(_connectionString);
            repo.AddPeople(people);
        }

        [HttpGet]
        [Route("getpeople")]

        public List<Person> GetPeople()
        {
            var repo = new PeopleRepository(_connectionString);
            return repo.GetPeople();
        }

        [HttpPost]

        [Route("deletepeople")]

        public void Delete()
        {
            var repo = new PeopleRepository(_connectionString);
           repo.DeleteAll();
        }


        static List<Person> GetPeople(int amount)
        {
            return Enumerable.Range(1, amount).Select(_ =>
            {
                return new Person
                {
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last(),
                    Age = Faker.RandomNumber.Next(1, 120),
                    Address = Faker.Address.StreetAddress(),
                    Email = Faker.Internet.Email()
                };
            }).ToList();

        }


        static string GetCsv(List<Person> people)
        {
            var builder = new StringBuilder();
            var stringwriter = new StringWriter(builder);
            using var csv = new CsvWriter(stringwriter, CultureInfo.InvariantCulture);
            csv.WriteRecords(people);
            return builder.ToString();
        }
        static List<Person> GetFromCsvBytes(byte[] csvbytes)
        {
            using var memoryStream = new MemoryStream(csvbytes);
            var streamReader = new StreamReader(memoryStream);
            using var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            return reader.GetRecords<Person>().ToList();

        }
    }
}
