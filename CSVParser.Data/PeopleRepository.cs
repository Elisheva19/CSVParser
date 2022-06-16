using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVParser.Data
{
 public  class PeopleRepository
    {
        public readonly string _connection;
        public PeopleRepository(string connect)
        {
            _connection = connect;

        }

        public void AddPeople(List<Person> ppl)
        {
            var context = new PeopleDataContext(_connection);
            context.People.AddRange(ppl);
            context.SaveChanges();
        }

        public List<Person> GetPeople()
        {
            var context = new PeopleDataContext(_connection);
            return context.People.ToList();
        }

        public void DeleteAll()
        {
            var context = new PeopleDataContext(_connection);
            context.Database.ExecuteSqlRaw("delete from people");
                  
        }

    }
}
