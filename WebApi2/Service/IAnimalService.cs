using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi2.Models;

namespace WebApi2.Service
{
    public interface IAnimalService
    { 
        public Task<List<Animal>> GetAnimals(string orderBy);
        public Task<bool> AddAnimalToDatabase(Animal newAnimal);
        public Task<bool> UpdateAnimal(int idAnimal, Animal animal);
        public Task<bool> DeleteAnimalFromDatabase(int idAnimal);
    }
}
