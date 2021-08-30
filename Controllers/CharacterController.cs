using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult<ServiceResponse<List<Character>>>> Get() =>
            Ok(await _characterService
                .GetAllCharacters()); // sense respoense data in 200 which which means data is foind 

        #endregion


        #region ActionResult<Character> GetSingle(int id)  --> returns single character based on ID Get MEthod

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Character>>> GetSingle(int id) =>
            Ok(await _characterService.GetCharacterById(id)); // sense respoense data

        #endregion


        #region MyRegion ActionResult<List<Character>> AddCharacter(Character newCharacter) --> a post method used to create a character

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<Character>>>> AddCharacter(Character newCharacter) =>
            Ok(await _characterService.AddCharacter(newCharacter));

        #endregion

        #endregion
    }
}