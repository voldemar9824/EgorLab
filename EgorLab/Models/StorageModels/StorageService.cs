
using EgorLab.Models;
using EgorLab.Controllers;
using EgorLab.Configuration;
using System.Linq;

namespace EgorLab.Models.StorageModels
{
    public class StorageService
    {
        private readonly IStorage<Person> _storage;

        public StorageService(IStorage<Person> storage)
        {
            _storage = storage;
        }

        public string GetStorageType()
        {
            return _storage.StorageType;
        }
        public int GetNumberOfItems()
        {
            return _storage.All.Count();
        }
        
    }
}