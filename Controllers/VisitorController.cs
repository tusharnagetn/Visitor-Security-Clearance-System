using Microsoft.AspNetCore.Mvc;
using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Interface;
using Visitor_Security_Clearance_System.Service;

namespace Visitor_Security_Clearance_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VisitorController : Controller
    {
        private readonly IVisitorService _iVisitorService;
        public VisitorController(IVisitorService iVisitorService) 
        {
            _iVisitorService = iVisitorService;
        }

        [HttpPost]
        public async Task<IActionResult> AddVisitorAndSendMailToManager(VisitorDTO visitorDTO)
        {
            bool isExist = await _iVisitorService.IsVisitorExist(visitorDTO.Email);

            if (isExist)
            {
                return BadRequest("Already registered");
            }

            var response = await _iVisitorService.AddVisitorAndSendMailToManager(visitorDTO);

            return Ok(response);
        }

        [HttpGet("{uId}")]
        public async Task<IActionResult> GetVisitorByUId(string uId)
        {
            var response = await _iVisitorService.GetVisitorByUId(uId);

            if (response == null)
            {
                return NotFound("Data not found");
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateVisitor(VisitorDTO visitorDTO)
        {
            var isExist = await _iVisitorService.GetVisitorByUId(visitorDTO.UId);

            if (isExist == null)
            {
                return BadRequest("Data not found");
            }

            var response = await _iVisitorService.UpdateVisitor(visitorDTO);

            return Ok(response);
        }

        [HttpDelete("{uId}")]
        public async Task<IActionResult> DeleteVisitorByUId(string uId)
        {
            var isExist = await _iVisitorService.GetVisitorByUId(uId);

            if (isExist == null)
            {
                return NotFound("Data not found");
            }

            var response = await _iVisitorService.DeleteVisitorByUId(uId);

            return Ok(response);
        }
    }
}
