using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rpg.DTOS.Character;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters(); // this gets all the characters from the
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

        // not that now Character has been replaced by GetCharacterDTO. 
        Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id); // gets single character based on id 

        Task<ServiceResponse<List<GetCharacterDto>>>
            AddCharacter(AddCharacterDto newCharacter); // adds a single character

        Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacterDto);

        Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id); // deletes
    }
}