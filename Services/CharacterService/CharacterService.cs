using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.DTOS.Character;
using dotnet_rpg.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper; // this will map objects. 
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #region Methods GetAllCharacters(), GetCharacterById(int id),AddCharacter(Character newCharacter)

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters =
                await _context.Character.Where(c => c.User.Id == GetUserId())
                    .ToListAsync(); // get the database characters async 
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
            var dbCharacters = await _context.Character.ToListAsync(); // get the database characters async 
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
            Character character = _mapper.Map<Character>(newCharacter); // adding new characters to the list 
            character.User =
                await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId()); // links users to characters. 
            _context.Character.Add(character);
            await _context.SaveChangesAsync(); // writes to database. 
            serviceResponse.Data = await _context.Character.Where(c => c.User.Id == GetUserId())
                .Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();

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
                    await _context.Character.Include(c => c.User)
                        .FirstOrDefaultAsync(c =>
                            c.Id == updateCharacterDto.Id); // find the character id based on Characters. 
                if (character.User.Id == GetUserId())
                {
                    character.Name = updateCharacterDto.Name;
                    character.HitPoint = updateCharacterDto.HitPoint;
                    character.Strength = updateCharacterDto.Strength;
                    character.Intelligence = updateCharacterDto.Intelligence;
                    character.Defense = updateCharacterDto.Defense;
                    character.Class = updateCharacterDto.Class;

                    _context.Character.Update(character); // this is nedded to update the characters 
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "character Not found";
                }
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
                    await _context.Character.FirstAsync(c =>
                        c.Id == id); // find the character id based on Characters. 
                _context.Character.Remove(character);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _context.Character.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }

            return serviceResponse;
        }

        #endregion

        #region GetUserId() --> get the looged in UserId

        private int GetUserId() =>
            int.Parse(_httpContextAccessor.HttpContext.User
                .FindFirstValue(ClaimTypes.NameIdentifier)); // get the user vased on ClaimTypes.NameIdentifier. 

        #endregion

        #endregion
    }
}