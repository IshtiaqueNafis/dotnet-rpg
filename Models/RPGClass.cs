using System.Text.Json.Serialization;

namespace dotnet_rpg.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))] // will show thesse as a text isntead of array 
    public enum RpgClass
    {
        Knight,
        Healer,
        Cleric
    }
}