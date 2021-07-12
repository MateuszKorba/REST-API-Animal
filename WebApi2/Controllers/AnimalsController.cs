using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApi2.Models;
using WebApi2.Service;

namespace WebApi2.Controllers
{
    [ApiController]
    [Route("api/animals")]
    public class AnimalsController : ControllerBase
    {
        private IAnimalService _animalsService;

        public AnimalsController(IAnimalService animalsService)
        {
            _animalsService = animalsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnimals(string orderBy)
        {
            var listAnimal = await _animalsService.GetAnimals(orderBy);
            if (listAnimal.Count != 0)
            {
                return Ok(listAnimal);
            }
            else {
                return Problem("Baza jest pusta");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAnimalToDatabase([FromBody] Animal newAnimal)
        {
            var resultOperation = await _animalsService.AddAnimalToDatabase(newAnimal);
            if (resultOperation)
            {
                return Ok("Dodano " +newAnimal.AnimalName+ " do bazy");
            }
            else
            {
                return BadRequest("Pola name,category,area nie moga byc puste\nProsze sprawdzic czy wsystkie pola zostały uzupełnione");
            }
        }

        [HttpPut("{idAnimal}")]
        public async Task<IActionResult> UpdateAnimal([FromRoute] int idAnimal, [FromBody] Animal updateAnimal)
        {
            try {
                var resultOperation = await _animalsService.UpdateAnimal(idAnimal, updateAnimal);
                if (resultOperation)
                {
                    return Ok("Zaaktualizowano dane zwierzaka o ID = " + idAnimal);
                }
                else
                {
                    return NotFound("Brak zwierzecia o takim ID");
                }
            } catch (Exception e) {
                return BadRequest("Pola name,category,area nie moga byc puste\nProsze sprawdzic czy wsystkie pola zostały uzupełnione");
            }
        }

        [HttpDelete("{idAnimal}")]
        public async Task<IActionResult> DeleteAnimalFromDatabase([FromRoute] int idAnimal)
        {
            var resultOperation = await _animalsService.DeleteAnimalFromDatabase(idAnimal);
            if (resultOperation)
            {
                return Ok("Usunieto zwierze");
            }
            else
            {
                return NotFound("Brak zwierzecia o takim ID");
            }
        }

    }
}
