using Visitor_Security_Clearance_System.DTO;

namespace Visitor_Security_Clearance_System.Interface
{
    public interface IOfficeService
    {
        Task<bool> IsOfficeExist(string email);
        Task<OfficeDTO> GetOfficeByUId(string uId);
        Task<OfficeDTO> AddOffice(OfficeDTO OfficeDTO);
        Task<OfficeDTO> UpdateOffice(OfficeDTO OfficeDTO);
        Task<string> DeleteOfficeByUId(string uId);
    }
}
