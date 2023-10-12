using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Valorant.Data;
using Valorant.Dto;
using Valorant.Cache;

namespace Valorant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class CharacterController : ControllerBase{
        private readonly DataContext _context;
        // private readonly ICacheService _cacheService;
        public CharacterController(DataContext context) 
        // public CharacterController(DataContext context, ICacheService cacheService) 
        { 
            _context = context;
            // _cacheService = cacheService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Character>>> Get (int userId){
            var characters = await _context.Characters
                .Where(c => c.UserId == userId)
                .Include(c => c.Weapon)
                .Include(c => c.Skill)
                .ToListAsync();
            return characters;
        }

        [HttpPost]
        public async Task<ActionResult<List<Character>>> Create (CharacterDto request){
            var user = await _context.Users.FindAsync(request.UserId);
            if (user is null)
                return NotFound("user not found!");

            var weapon = await _context.Weapons.FindAsync(request.WeaponId);
            var skill = await _context.Skills.FindAsync(request.Skilld);

            var character = new Character{
                Name = request.Name,
                User = user,
                Weapon = weapon,
                Skill = skill,
            };

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            return await Get(request.UserId);
        }

        // [HttpPost("Skill-Master")]
        // public async Task<ActionResult<List<Skill>>> AddSkillCharacter (SkillMasterDto request){
        //     var skill = new Skill{
        //         Name = request.Name,
        //         Damage = request.Damage
        //     };

        //     _context.Skills.Add(skill);
        //     await _context.SaveChangesAsync();

        //     return _context.Skills.ToList();
        // }

        // [HttpPost("Weapon")]
        // public async Task<ActionResult<Character>> AddWeapon (WeaponDto request){
        //     var character = await _context.Characters.FindAsync(request.CharacterId);
        //     if (character is null)
        //         return NotFound("character not found!");

        //     var weapon = new Weapon{
        //         Name = request.Name,
        //         Damage = request.Damage,
        //         Character = character
        //     };

        //     _context.Weapons.Add(weapon);
        //     await _context.SaveChangesAsync();

        //     return character;           
        // }

        // [HttpPost("Skill")]
        // public async Task<ActionResult<Character>> AddSkill (SkillDto request){
        //     var character = await _context.Characters
        //         .Where(c => c.Id == request.CharacterId)
        //         .Include(c => c.Skill)
        //         .FirstOrDefaultAsync();

        //     if (character is null)
        //         return NotFound("character not found!");

        //     var skill = await _context.Skills.FindAsync(request.SkillId);
        //     if(skill is null)
        //         return NotFound("skill not found!");

        //     character.Skill.Add(skill);
        //     await _context.SaveChangesAsync();

        //     return character;           
        // }
    }
}