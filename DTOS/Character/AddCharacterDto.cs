using dotnet_rpg.Models;

namespace dotnet_rpg.DTOS.Character
{
    public class AddCharacterDto
    { //no id here. 
        public string Name { get; set; } = "Frodo";
        public int HitPoint { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Knight;
    }
}