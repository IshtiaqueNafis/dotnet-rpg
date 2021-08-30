using System.Collections.Generic;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services
{
    public interface ICharacterService
    {
        List<Character> GetAllCharacters(); // this gets all the characters from the
        Character GetCharacterById(int id); // gets single character based on id 
        List<Character> AddCharacter(Character newCharacter); // adds a single character
    }
}