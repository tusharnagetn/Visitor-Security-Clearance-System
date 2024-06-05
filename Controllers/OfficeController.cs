using Microsoft.AspNetCore.Mvc;
using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Interface;

namespace Visitor_Security_Clearance_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OfficeController : Controller
    {
        private readonly IOfficeService _iOfficeService;
        public OfficeController(IOfficeService iOfficeService) 
        {
            _iOfficeService = iOfficeService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOffice(OfficeDTO officeDTO)
        {
            bool isExist = await _iOfficeService.IsOfficeExist(officeDTO.Email);

            if (isExist)
            {
                return BadRequest("Already registered");
            }

            var response = await _iOfficeService.AddOffice(officeDTO);

            return Ok(response);
        }

        [HttpGet("{uId}")]
        public async Task<IActionResult> GetOfficeByUId(string uId)
        {
            var response = await _iOfficeService.GetOfficeByUId(uId);

            if (response == null)
            {
                return NotFound("Data not found");
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOffice(OfficeDTO officeDTO)
        {
            var isExist = await _iOfficeService.GetOfficeByUId(officeDTO.UId);

            if (isExist == null)
            {
                return BadRequest("Data not found");
            }

            var response = await _iOfficeService.UpdateOffice(officeDTO);

            return Ok(response);
        }

        [HttpDelete("{uId}")]
        public async Task<IActionResult> DeleteOfficeByUId(string uId)
        {
            var isExist = await _iOfficeService.GetOfficeByUId(uId);

            if (isExist == null)
            {
                return NotFound("Data not found");
            }

            var response = await _iOfficeService.DeleteOfficeByUId(uId);

            return Ok(response);
        }

    }
}
