using System.Collections.Generic;
using System.Linq;
using dotnet_rpg.Models;
using dotnet_rpg.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController] // does api repsoneses, 
    [Route("[controller]")] // this means this is the controller for the route  
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService
            _characterService; // dependecy for Icharacter which is part of characters sevice. 

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        #region ActionResult<List<Character>> Get() ,ActionResult<Character> GetSingle(int id)

        #region ActionResult<List<Character>> Get() GetMETHOD returns list of characters

        [HttpGet("GetAll")]
        public ActionResult<List<Character>> Get() =>
            Ok(_characterService.GetAllCharacters()); // sense respoense data in 200 which which means data is foind 

        #endregion
        


        #region ActionResult<Character> GetSingle(int id)  --> returns single character based on ID Get MEthod

        [HttpGet("{id}")]
        public ActionResult<Character> GetSingle(int id) =>
            Ok(_characterService.GetCharacterById(id)); // sense respoense data

        #endregion
        
        

        #region MyRegion ActionResult<List<Character>> AddCharacter(Character newCharacter) --> a post method used to create a character

        [HttpPost]
        public ActionResult<List<Character>> AddCharacter(Character newCharacter) =>
            Ok(_characterService.AddCharacter(newCharacter));

        #endregion

        #endregion
    }
}