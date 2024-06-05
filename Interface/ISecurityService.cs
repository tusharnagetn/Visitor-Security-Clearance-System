using Visitor_Security_Clearance_System.DTO;

namespace Visitor_Security_Clearance_System.Interface
{
    public interface ISecurityService
    {
        Task<bool> IsSecurityExist(string email);
        Task<SecurityDTO> GetSecurityByUId(string uId);
        Task<SecurityDTO> AddSecurity(SecurityDTO securityDTO);
        Task<SecurityDTO> UpdateSecurity(SecurityDTO securityDTO);
        Task<string> DeleteSecurityByUId(string uId);
    }
}
