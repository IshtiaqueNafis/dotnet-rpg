using System.Threading.Tasks;
using dotnet_rpg.DTOS.Character;
using dotnet_rpg.DTOS.Weapon;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
    }
}