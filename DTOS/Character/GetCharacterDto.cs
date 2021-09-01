using System.Collections.Generic;
using dotnet_rpg.DTOS.Skill;
using dotnet_rpg.DTOS.Weapon;
using dotnet_rpg.Models;

namespace dotnet_rpg.DTOS.Character
{
    public class GetCharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Frodo";
        public int HitPoint { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Knight;
        public GetWeaponDto Weapon { get; set; }
        public List<GetSkillDto> Skills { get; set; } // this will show all the skills regarding the user. 
    }
}