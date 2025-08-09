using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleManagementApi.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace PeopleManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PeopleController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetAll()
        {
            try
            {
                return await _context.People.ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"אירעה שגיאה בשרת: {ex.Message}");
            }
        }


        [HttpPost]
        public async Task<ActionResult<Person>> AddPerson([FromBody] Person person)
        {

            try
            {
                bool IsFirstNameValid(string input) =>
    !string.IsNullOrWhiteSpace(input) &&
    new StringInfo(input).LengthInTextElements >= 2;
            bool IsLastNameValid(string input) => !string.IsNullOrWhiteSpace(input) &&
    new StringInfo(input).LengthInTextElements >= 2;

            if (!IsFirstNameValid(person.FirstName))
            {
                return BadRequest("לפחות 2 אותיות");
            }
            if (!IsLastNameValid(person.LastName))
            {
                return BadRequest("לפחות 2 אותיות");
            }
            if (string.IsNullOrEmpty(person.IdentityNumber) || person.IdentityNumber.Length != 9 || !person.IdentityNumber.All(char.IsDigit))
            {
                return BadRequest("תעודת זהות חייבת להכיל בדיוק 9 ספרות.");
            }

            if (string.IsNullOrEmpty(person.PhoneNumber) || person.PhoneNumber.Length != 10 || !person.PhoneNumber.All(char.IsDigit))
            {
                return BadRequest("מספר הטלפון חייב להכיל בדיוק 10 ספרות.");
            }
            if (!IsValidEmail(person.Email))
            {
                return BadRequest("כתובת המייל אינה תקינה.");
            }
            _context.People.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAll), new { id = person.Id }, person);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"אירעה שגיאה בשרת: {ex.Message}");
            }
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            try
            {
                var person = await _context.People.FindAsync(id);
            if (person == null)
                return NotFound();

            _context.People.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"אירעה שגיאה בשרת: {ex.Message}");
            }
        }
        bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;
            string validRegex = @"^[a-zA-Z0-9](\.?[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-])*@[a-zA-Z0-9-]+(\.[a-zA-Z]{2,})+$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, validRegex);
        }
    }
}
