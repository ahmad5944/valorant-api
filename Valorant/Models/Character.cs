using System.Text.Json.Serialization;

namespace Valorant.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // [JsonIgnore] 
        public User User { get; set; }
        public int UserId { get; set; }
        public Weapon Weapon { get; set; }
        // public int WeaponId { get; set; }
        // public List<Skill> Skill { get; set; }
        public Skill Skill { get; set; }
        public int SkillId { get; set; }

    }
}
