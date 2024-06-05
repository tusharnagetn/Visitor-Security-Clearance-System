using Visitor_Security_Clearance_System.DTO;

namespace Visitor_Security_Clearance_System.Interface
{
    public interface IFunctionalityService
    {
        Task<bool> Login(string username, string password, string role);
        Task<PassDTO> AddPass(PassDTO passDTO);
        Task<PassDTO> UpdateApprovedStatusOfPass(string PassId, bool IsApproved);
        Task<List<VisitorDTO>> GetVisitorsByStatusAndOfficeId(string officeId, bool acceptedOrNot);
    }
}
