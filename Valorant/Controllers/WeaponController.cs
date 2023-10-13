using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Valorant.Models;
using Valorant.Data;
using Valorant.Dto;

namespace Valorant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class WeaponController : ControllerBase
    {
        private readonly DataContext _context;
        public WeaponController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Weapon>>> GetAll()
        {
            return Ok(await _context.Weapons.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Weapon>> Get(int id)
        {
            var weapon = await _context.Weapons
                .Where(c => c.Id == id)
                .ToListAsync();

            return Ok(weapon);
        }

        [HttpPost]
        public async Task<ActionResult<Weapon>> Add(WeaponDto request)
        {
            // var character = await _context.Characters.FindAsync(request.CharacterId);
            // if (character == null)
            //     return NotFound();

            var newWeapon = new Weapon
            {
                Name = request.Name,
                Damage = request.Damage,
                // Character = character
            };

            _context.Weapons.Add(newWeapon);
            await _context.SaveChangesAsync();

            return Ok(newWeapon);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Weapon>> Update(WeaponDto request, int id)
        {
            var weapon = await _context.Weapons.FindAsync(id);
            if (weapon is null)
                return NotFound("Weapon Not Found!");

            // var character = await _context.Characters.FindAsync(weapon.CharacterId);
            // if (character == null)
            //     return NotFound("Weapon Not Found!");

            // var Test = new Character();

            weapon.Name = request.Name;
            weapon.Damage = request.Damage;
            // Test = character;

            await _context.SaveChangesAsync();
            return Ok(await _context.Weapons.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Weapon>>> Delete(int id)
        {
            var weapon = await _context.Weapons.FindAsync(id);
            if (weapon is null)
                return BadRequest();

            _context.Weapons.Remove(weapon);
            await _context.SaveChangesAsync();

            var status = "Weapon " + weapon.Name + " Deleted";
            return Ok(status);
        }
    }
}