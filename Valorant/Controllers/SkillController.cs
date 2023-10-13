using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Valorant.Models;
using Valorant.Data;
using Valorant.Dto;

namespace Valorant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SkillController : ControllerBase
    {
        private readonly DataContext _context;
        public SkillController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Skill>>> All()
        {
            return Ok(await _context.Skills.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Skill>> Get(int id)
        {
            var skill = await _context.Skills
                .Where(c => c.Id == id)
                .ToListAsync();

            if (skill is null)
                return NotFound("skill not found!");

            return Ok(skill);
        }

        [HttpPost]
        public async Task<ActionResult<List<Skill>>> Add(SkillDto request)
        {

            var skill = new Skill
            {
                Name = request.Name,
                Damage = request.Damage,
            };

            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();

            await _context.SaveChangesAsync();
            return Ok(await _context.Skills.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Skill>> Update(SkillDto request, int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill is null)
                return NotFound();

            skill.Name = request.Name;
            skill.Damage = request.Damage;

            await _context.SaveChangesAsync();
            return Ok(await _context.Skills.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Skill>>> Delete(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill is null)
                return NotFound("Skill Not Found!");

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();

            var status = "SKill " + skill.Name + " Deleted";
            return Ok(status);
        }
    }
}