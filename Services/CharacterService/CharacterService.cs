using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.DTOS.Character;
using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;

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
        private readonly DataContext context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            this.context = context;
        }

        #region Methods GetAllCharacters(), GetCharacterById(int id),AddCharacter(Character newCharacter)

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await context.Character.ToListAsync(); // get the database characters async 
            // this must match the what function is going to return from the
            // this will return a list with all the characters. 
            // the ServiceList<T> --> must follow  ServiceResponse<List<Character>>() to make sure the function matches. 
            serviceResponse.Data =
                dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c))
                    .ToList(); // then set all the ccharacters to data. 
            return serviceResponse; // return it. 
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse =
                new ServiceResponse<GetCharacterDto>(); // create a new isntance of ServiceResponse with Get Chracter DTO Object. 
            var dbCharacters = await context.Character.ToListAsync(); // get the database characters async 
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacters.FirstOrDefault(c => c.Id == id));

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
            context.Character.Add(character);
            await context.SaveChangesAsync(); // writes to database. 
            serviceResponse.Data = await context.Character.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();

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

        #region Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacterDto) --> updates characters

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacterDto)


        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {
                Character character =
                    await context.Character.FirstOrDefaultAsync(c =>
                        c.Id == updateCharacterDto.Id); // find the character id based on Characters. 
                character.Name = updateCharacterDto.Name;
                character.HitPoint = updateCharacterDto.HitPoint;
                character.Strength = updateCharacterDto.Strength;
                character.Intelligence = updateCharacterDto.Intelligence;
                character.Defense = updateCharacterDto.Defense;
                character.Class = updateCharacterDto.Class;

                context.Character.Update(character); // this is nedded to update the characters 
                await context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            try
            {
                Character character =
                    await context.Character.FirstAsync(c =>
                        c.Id == id); // find the character id based on Characters. 
                context.Character.Remove(character);
                await context.SaveChangesAsync();
                serviceResponse.Data = context.Character.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }

            return serviceResponse;
        }

        #endregion

        #endregion
    }
}