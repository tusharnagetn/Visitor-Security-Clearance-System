using Visitor_Security_Clearance_System.DTO;

namespace Visitor_Security_Clearance_System.Interface
{
    public interface IVisitorService
    {
        Task<bool> IsVisitorExist(string email);
        Task<VisitorDTO> GetVisitorByUId(string uId);
        Task<VisitorDTO> AddVisitorAndSendMailToManager(VisitorDTO visitorDTO);
        Task<VisitorDTO> UpdateVisitor(VisitorDTO visitorDTO);
        Task<string> DeleteVisitorByUId(string uId);
    }
}
