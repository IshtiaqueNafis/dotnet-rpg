using System.Collections.Generic;
using System.Linq;
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

        public List<Character> GetAllCharacters() => characters;

        public Character GetCharacterById(int id) => characters.FirstOrDefault(c => c.Id == id);

        public List<Character> AddCharacter(Character newCharacter)
        {
            characters.Add(newCharacter);
            return characters;
        }

        #endregion
    }
}