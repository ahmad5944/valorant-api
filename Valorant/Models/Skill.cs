using System.Text.Json.Serialization;

namespace Valorant.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Damage { get; set; } = string.Empty;
        [JsonIgnore]
        public Character Character { get; set; }
    }
}
