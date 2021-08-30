using System.Collections.Generic;
using System.Linq;
using dotnet_rpg.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController] // does api repsoneses, 
    [Route("[controller]")] // this means this is the controller for the route  
    public class CharacterController : ControllerBase
    {
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character { Id = 1, Name = "Sam" }
        };

        // returns a list of characters 
        [HttpGet("GetAll")] // means this is a http get method and has GETAll method alongside with with it. 
        public ActionResult<List<Character>> Get() // means will be expecting a character 
        {
            return Ok(characters); // sense respoense data in 200 which which means data is foind 
        }

        [HttpGet("{id}")]
        public ActionResult<Character> GetSingle(int id) // it is case sensititive make sure this Id and getSingle are both same. 
        {
            return Ok(characters.FirstOrDefault(c => c.Id == id)); // sense respoense data
        }
    }
}