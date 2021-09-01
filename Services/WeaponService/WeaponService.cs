using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.DTOS.Character;
using dotnet_rpg.DTOS.Weapon;
using dotnet_rpg.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public WeaponService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        #region Methods AddWeapon(AddWeaponDto newWeapon)

        #region Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon) --> adds a weapon to the characters.

        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character =
                    await _context.Character.FirstOrDefaultAsync(c =>
                        c.Id == newWeapon.CharacterId && c.User.Id == GetUserId());

                #region code expalanation

                /*
                 *  await _context.Character.FirstOrDefaultAsync(c =>c.Id == newWeapon.CharacterId && c.User.Id == GetUserId());
                 * c.Id == newWeapon.CharacterId --> make sure weaponId matches with with character Selected Id
                 * c.User.Id == GetUserId() --> make sure only aceesed user can modify the data. 
                 */

                #endregion

                if (character == null)
                {
                    response.Success = false;
                    response.Message = "no character found";
                    return response;
                }

                var weapon = new Weapon
                {
                    Name = newWeapon.Name,
                    Damage = newWeapon.Damage,
                    Character = character
                };
                _context.Weapons.Add(weapon);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetCharacterDto>(character); // get the character and map it to data. 
                return response;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
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