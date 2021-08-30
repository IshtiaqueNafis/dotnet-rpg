using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character { Id = 1, Name = "Sam" }
        };


        #region Methods GetAllCharacters(), GetCharacterById(int id),AddCharacter(Character newCharacter)

        public async Task<ServiceResponse<List<Character>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<Character>>();
            // this must match the what function is going to return from the
            // this will return a list with all the characters. 
            // the ServiceList<T> --> must follow  ServiceResponse<List<Character>>() to make sure the function matches. 
            serviceResponse.Data = characters; // then set all the ccharacters to data. 
            return serviceResponse; // return it. 
        }

        public async Task<ServiceResponse<Character>> GetCharacterById(int id)
        {
            ServiceResponse<Character> serviceResponse = new ServiceResponse<Character>();
            Character character = characters.FirstOrDefault(c => c.Id == id);
            serviceResponse.Data = character;
            return serviceResponse;
        }


        public async Task<ServiceResponse<List<Character>>> AddCharacter(Character newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<Character>>();
            characters.Add(newCharacter); // adding new characters to the list 
            serviceResponse.Data = characters; // data becomes added on the data. 
            return serviceResponse;
        }

        #endregion
    }
}