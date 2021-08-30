using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.DTOS.Character;
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

        private readonly IMapper _mapper; // this will map objects. 

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }

        #region Methods GetAllCharacters(), GetCharacterById(int id),AddCharacter(Character newCharacter)

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            // this must match the what function is going to return from the
            // this will return a list with all the characters. 
            // the ServiceList<T> --> must follow  ServiceResponse<List<Character>>() to make sure the function matches. 
            serviceResponse.Data =
                characters.Select(c => _mapper.Map<GetCharacterDto>(c))
                    .ToList(); // then set all the ccharacters to data. 
            return serviceResponse; // return it. 
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse =
                new ServiceResponse<GetCharacterDto>(); // create a new isntance of ServiceResponse with Get Chracter DTO Object. 
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(characters.FirstOrDefault(c => c.Id == id));

            #region CodeExpalantion _mapper.Map<GetCharacterDto>(characters.FirstOrDefault(c => c.Id == id));

            /*
             * _mapper.Map will be mapper to GetCharacterDTO from characters. 
             */

            #endregion

            return serviceResponse;
        }


        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter); // adding new characters to the list 
            character.Id = characters.Max(c => c.Id) + 1; // get the max number of id then add plus + 1 to it. 
            characters.Add(character); // then add it to the array 
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();

            #region code expalanation

            /*
             *  var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
             /*
           Create a new Service Response Object with GetCharactTer DTO 
             characters.Add(_mapper.Map<Character>(newCharacter)); // adding new characters to the list 
             add NewCharacter on AddCharacter is expecting a character but AddCharacterDTO is expecting a new character.
               --> _mapper.Map<Character>(newCharacter) --> maps addNewCharacter to characters. 
              ** serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
               --> serviceResponse.Data  this expects a new Data but has to be .Data with GetCharacterDto thus 
                 characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList(); 
                 --> using lambda this will loop through Charaters.Select(c=> _mapper.map(c=> go through each eahlist and map it to mapper and map to GetcharacterDTO.
             */

            #endregion

            return serviceResponse;
        }

        #endregion
    }
}