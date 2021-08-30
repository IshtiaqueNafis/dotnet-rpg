using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<Character>>> GetAllCharacters(); // this gets all the characters from the
        // Interface is wrapped with this. 
        // Task<ServiceResponse<List<Character>>>
        /*
         * going to read from left to right to make sense
         *Task<ServiceResponse<List<Character>>>
         *  --> will return a listOfcharacter,
         * --> followed by a list of ServiceResponse and then will be task
         * thus the simplest form --> 1st --> retutrn a list of characters, then a service Respoense and then Task 
         */

        //added service response to have acess to generic class. 
        Task<ServiceResponse<Character>> GetCharacterById(int id); // gets single character based on id 
        Task<ServiceResponse<List<Character>>> AddCharacter(Character newCharacter); // adds a single character
    }
}