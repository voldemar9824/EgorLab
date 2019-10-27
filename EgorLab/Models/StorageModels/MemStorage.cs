using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgorLab.Models.StorageModels
{
    public class MemStorage : IStorage<Person>
    {
        private readonly List<Person> people;

        public MemStorage()
        {
            people = new List<Person>();
        }

        public IEnumerable<Person> All => people.Select(v => v);

        public Person this[Guid id]
        {
            get
            {
                return people.FirstOrDefault(v => v.Id == id);
            }
            set
            {
                var person = people.FirstOrDefault(v => v.Id == id);
                if (person == null)
                {
                    people.Add(value);
                }
                person.Name = value.Name;
                person.SurName = value.SurName;
                person.Age = value.Age;
            }
        }

        public void Add(Person person)
        {
            person.Id = Guid.NewGuid();
            people.Add(person);
        }

        public bool Has(Guid id)
        {
            return people.Any(v => v.Id == id);
        }

        public void RemoveAt(Guid id)
        {
            var person = people.FirstOrDefault(v => v.Id == id);
            if (person != null)
            {
                people.Remove(person);
            }
        }       
    }
}

