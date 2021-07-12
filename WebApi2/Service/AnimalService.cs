using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebApi2.Models;

namespace WebApi2.Service
{
    public class AnimalService : IAnimalService
    {
        private string _connectionString = "Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True";

        public async Task<List<Animal>> GetAnimals(string rekordOrderBy)
        {
            var sql = "SELECT * FROM Animal";
            List<Animal> listOfanimals = new List<Animal>();
            using (var connection = new SqlConnection(_connectionString)) {
                using (var command = new SqlCommand()) {
                    command.Connection = connection;
                    connection.Open();
                    command.CommandText = sql;
                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read()) {
                        listOfanimals.Add(new Animal
                        {
                            IDAnimal = (int)dataReader["IdAnimal"],
                            AnimalName = (string)dataReader["Name"],
                            Description = (string)dataReader["Description"],
                            Category = (string)dataReader["Category"],
                            Area = (string)dataReader["Area"]
                        });
                    }
                }
            }
            return orderByMethod(listOfanimals, rekordOrderBy);
        }

        public async Task<bool> AddAnimalToDatabase(Animal newAnimal)
        {
            bool isOK = true;
            if (string.IsNullOrWhiteSpace(newAnimal.AnimalName) || string.IsNullOrWhiteSpace(newAnimal.Category) || string.IsNullOrWhiteSpace(newAnimal.Area))
            {
                isOK = false;
            }
            
            if (isOK == true)
            {
                var sql = $"INSERT INTO Animal (Name,Description,Category,Area) VALUES ('{newAnimal.AnimalName}','{newAnimal.Description}','{newAnimal.Category}','{newAnimal.Area}')";
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        connection.Open();
                        command.CommandText = sql;
                        await command.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            else {
                return false;
            }
            
        }

        public async Task<bool> UpdateAnimal(int idAnimal, Animal updateAnimal)
        {
            var listAnimal = GetListOfAnimals();
            Animal animalResult = null;
            foreach (Animal animal in listAnimal)
            {
                if (animal.IDAnimal.Equals(idAnimal))
                {
                    animalResult = animal;
                }
            }
            bool isOK = true;
            if (string.IsNullOrWhiteSpace(updateAnimal.AnimalName) || string.IsNullOrWhiteSpace(updateAnimal.Category) || string.IsNullOrWhiteSpace(updateAnimal.Area))
            {
                isOK = false;
                throw new Exception("Blad");
            }
           
            if (animalResult != null)
            {
                var sql = $"UPDATE Animal SET Name='{updateAnimal.AnimalName}',Description='{updateAnimal.Description}',Category='{updateAnimal.Category}',Area='{updateAnimal.Area}' WHERE IdAnimal = {idAnimal}";

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        connection.Open();
                        command.CommandText = sql;
                        await command.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteAnimalFromDatabase(int idAnimal)
        {
            var listAnimal = GetListOfAnimals();
            Animal animalResult = null;
            foreach (Animal animal in listAnimal)
            {
                if (animal.IDAnimal.Equals(idAnimal))
                {
                    animalResult = animal;
                }
            }
            if (animalResult != null)
            {
                var sql = $"DELETE FROM Animal WHERE IdAnimal = {idAnimal}";
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        connection.Open();
                        command.CommandText = sql;
                        await command.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private List<Animal> orderByMethod(List<Animal> list, string orderBy)
        {
            if (orderBy == null) {
                return list.OrderBy(a => a.AnimalName).ToList();
            }
            else if(orderBy == "name" || orderBy == "NAME" || orderBy == "Name") {
                return list.OrderBy(a => a.AnimalName).ToList();
            }
            else if (orderBy == "description" || orderBy == "DESCRIPTION" || orderBy == "Description") {
                return list.OrderBy(a => a.Description).ToList();
            }
            else if (orderBy == "category" || orderBy == "CATEGORY" || orderBy == "Category") {
                return list.OrderBy(a => a.Category).ToList();
            }
            else if (orderBy == "area" || orderBy == "AREA" || orderBy == "Area") {
                return list.OrderBy(a => a.Area).ToList();
            }
            else {
                return list;
            }
        }

        public List<Animal> GetListOfAnimals()
        {
            var sql = "SELECT * FROM Animal";
            List<Animal> listOfanimals = new List<Animal>();
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    connection.Open();
                    command.CommandText = sql;
                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        listOfanimals.Add(new Animal
                        {
                            IDAnimal = (int)dataReader["IdAnimal"],
                            AnimalName = (string)dataReader["Name"],
                            Description = (string)dataReader["Description"],
                            Category = (string)dataReader["Category"],
                            Area = (string)dataReader["Area"]
                        });
                    }
                }
            }
            return listOfanimals;
        }
    }
}
