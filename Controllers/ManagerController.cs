using Microsoft.AspNetCore.Mvc;
using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Interface;

namespace Visitor_Security_Clearance_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ManagerController : Controller
    {
        private readonly IManagerService _iManagerService;
        public ManagerController(IManagerService iManagerService)
        {
            _iManagerService = iManagerService;
        }

        [HttpPost]
        public async Task<IActionResult> AddManager(ManagerDTO managerDTO)
        {
            var isExist = await _iManagerService.IsManagerExist(managerDTO.Email);

            if (isExist)
            {
                return BadRequest("Already registered");
            }

            ManagerDTO response = await _iManagerService.AddManager(managerDTO);

            return Ok(response);
        }

        [HttpGet("{uId}")]
        public async Task<IActionResult> GetManagerByUId(string uId)
        {
            var response = await _iManagerService.GetManagerByUId(uId);

            if (response == null)
            {
                return NotFound("Data not found");
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateManager(ManagerDTO managerDTO)
        {
            var isExist = await _iManagerService.GetManagerByUId(managerDTO.UId);

            if (isExist == null)
            {
                return BadRequest("Data not found");
            }

            var response = await _iManagerService.UpdateManager(managerDTO);

            return Ok(response);
        }

        [HttpDelete("{uId}")]
        public async Task<IActionResult> DeleteManagerByUId(string uId)
        {
            var isExist = await _iManagerService.GetManagerByUId(uId);

            if (isExist == null)
            {
                return NotFound("Data not found");
            }

            var response = await _iManagerService.DeleteManagerByUId(uId);

            return Ok(response);
        }
    }
}
