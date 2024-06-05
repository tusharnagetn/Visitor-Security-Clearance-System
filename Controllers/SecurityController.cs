using Microsoft.AspNetCore.Mvc;
using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Interface;

namespace Visitor_Security_Clearance_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SecurityController : Controller
    {
        private readonly ISecurityService _iSecurityService;
        public SecurityController(ISecurityService iSecurityService) 
        {
            _iSecurityService = iSecurityService;
        }

        [HttpPost]
        public async Task<IActionResult> AddSecurity(SecurityDTO securityDTO)
        {
            bool isExist = await _iSecurityService.IsSecurityExist(securityDTO.Email);

            if (isExist)
            {
                return BadRequest("Already registered");
            }

            var response = await _iSecurityService.AddSecurity(securityDTO);

            return Ok(response);
        }

        [HttpGet("{uId}")]
        public async Task<IActionResult> GetSecurityByUId(string uId)
        {
            var response = await _iSecurityService.GetSecurityByUId(uId);

            if (response == null)
            {
                return NotFound("Data not found");
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSecurity(SecurityDTO securityDTO)
        {
            var isExist = await _iSecurityService.GetSecurityByUId(securityDTO.UId);

            if (isExist == null)
            {
                return BadRequest("Data not found");
            }

            var response = await _iSecurityService.UpdateSecurity(securityDTO);

            return Ok(response);
        }

        [HttpDelete("{uId}")]
        public async Task<IActionResult> DeleteSecurityByUId(string uId)
        {
            var isExist = await _iSecurityService.GetSecurityByUId(uId);

            if (isExist == null)
            {
                return NotFound("Data not found");
            }

            var response = await _iSecurityService.DeleteSecurityByUId(uId);

            return Ok(response);
        }
    }
}
