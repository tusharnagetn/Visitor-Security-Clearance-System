
using Visitor_Security_Clearance_System.Entity;

namespace Visitor_Security_Clearance_System.CosmosDB
{
    public interface ICosmoseDBService
    {
        Task<dynamic> AddEntity(dynamic entity);
        Task<dynamic> UpdateEntity(dynamic entity);

        Task<bool> IsManagerExist(string email);
        Task<bool> IsOfficeExist(string email);
        Task<bool> IsSecurityExist(string email);
        Task<bool> IsVisitorExist(string email);

        Task<bool> IsManagerExist(string email, string pasword);
        Task<bool> IsOfficeExist(string email, string pasword);
        Task<bool> IsSecurityExist(string email, string pasword);

        Task<List<PassEntity>> GetPassEntitiesByApprovalStatus(string officeId, bool isApproved);

        Task<List<ManagerEntity>> GetManagerEntities();
        Task<List<OfficeEntity>> GetOfficeEntities();
        Task<List<PassEntity>> GetPassEntities();
        Task<List<SecurityEntity>> GetSecurityEntities();
        Task<List<VisitorEntity>> GetVisitorEntities();

        Task<ManagerEntity> GetManagerEntityByUId(string uid);
        Task<OfficeEntity> GetOfficeEntityByUId(string uid);
        Task<PassEntity> GetPassEntityByUId(string uid);
        Task<SecurityEntity> GetSecurityEntityByUId(string uid);
        Task<VisitorEntity> GetVisitorEntityByUId(string uid);
        Task<ManagerEntity> GetManagerEntityByOfficeId(string officeId);
    }
}
