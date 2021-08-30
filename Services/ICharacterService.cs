using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services
{
    public interface ICharacterService
    {
        Task<List<Character>> GetAllCharacters(); // this gets all the characters from the
        Task<Character> GetCharacterById(int id); // gets single character based on id 
        Task<List<Character>> AddCharacter(Character newCharacter); // adds a single character
    }
}