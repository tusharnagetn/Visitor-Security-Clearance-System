using Visitor_Security_Clearance_System.DTO;

namespace Visitor_Security_Clearance_System.Interface
{
    public interface IManagerService
    {
        Task<bool> IsManagerExist(string email);
        Task<ManagerDTO> GetManagerByUId(string uId);
        Task<ManagerDTO> AddManager(ManagerDTO managerDTO);
        Task<ManagerDTO> UpdateManager(ManagerDTO managerDTO);
        Task<string> DeleteManagerByUId(string uId);
    }
}
