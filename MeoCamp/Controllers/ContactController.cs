using MeoCamp.Service.BusinessModel;
using MeoCamp.Service.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.ModelBinding;

namespace MeoCamp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IContactService _contactService;
        private readonly ILogger<ContactController> _logger;

        public ContactController(IConfiguration configuration, IContactService contactService, ILogger<ContactController> logger)
        {
            _configuration = configuration;
            _contactService = contactService;
            _logger = logger;
        }

        [HttpGet("getAllContacts")]
        public async Task<IActionResult> GetAllContactAsync()
        {
            var result = await _contactService.GetAllContactAsync();
            if (result != null && result.Any())
            {
                // lấy list thành công
                return Ok(result);
            }
            else
            {
                // lấy list failed
                return BadRequest("List rỗng.");
            }
        }

        [HttpGet("getContactbyuserId")]
        public async Task<IActionResult> GetContactbyUsernameAsync(string user_name)
        {
            if (ModelState.IsValid)
            {
                var result = await _contactService.GetContactbyUsernameAsync(user_name);
                if (result != null)
                {
                    // lấy contact thành công
                    return Ok(result);
                }
                else
                {
                    // tìm contact failed
                    return BadRequest("chưa có người này Contact.");
                }
            }
            return BadRequest("Invalid data.");
        }
        [HttpPost("CreateContact")]
        public async Task<IActionResult> CreateBlog(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _contactService.CreateContact(model);
                if (result != null)
                {
                    // tạo contact thành công
                    return Ok(result);
                }
                else
                {
                    // tạo contact failed
                    return BadRequest("Contact không thành công.");
                }
            }
            return BadRequest("Invalid data.");
        }

        [HttpDelete("DeleteContact/{Id}")]
        public async Task<IActionResult> DeleteContact(int Id)
        {
            if (ModelState.IsValid)
            {
                var result = await _contactService.DeleteContact(Id);
                if (result)
                {
                    // xoá Contact thành công
                    return Ok("Contact xoá thành công.");
                }
                else
                {
                    // xoá Contact failed
                    return BadRequest("Contact xoá không thành công.");
                }
            }
            return BadRequest("Invalid data.");
        }
    }
}
    
 

