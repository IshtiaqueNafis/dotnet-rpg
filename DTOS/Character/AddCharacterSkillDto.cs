namespace dotnet_rpg.DTOS.Character
{
    public class AddCharacterSkillDto // this table will connect CharacterId to SKillId. 
    {
        public int CharacterId { get; set; }
        public int SkillId { get; set; }
    }
}