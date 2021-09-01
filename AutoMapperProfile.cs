using AutoMapper;
using dotnet_rpg.DTOS.Character;
using dotnet_rpg.DTOS.Weapon;
using dotnet_rpg.Models;

namespace dotnet_rpg
{
    public class AutoMapperProfile : Profile // this will map classes to automapper class 
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<Weapon,GetWeaponDto>();
        }
    }
}