using Microsoft.Azure.Cosmos;
using Visitor_Security_Clearance_System.Common;
using Visitor_Security_Clearance_System.Entity;

namespace Visitor_Security_Clearance_System.CosmosDB
{
    public class CosmosDBService : ICosmoseDBService 
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public CosmosDBService()
        {
            _cosmosClient = new CosmosClient(Credentials.CosmosUrl, Credentials.PrimaryKey);
            _container = _cosmosClient.GetContainer(Credentials.DatabaseName, Credentials.Container);
        }

        public async Task<dynamic> AddEntity(dynamic Entity)
        {
            var response = await _container.CreateItemAsync(Entity);
            return response;
        }

        public async Task<dynamic> UpdateEntity(dynamic entity)
        {
            var response = await _container.ReplaceItemAsync(entity, entity.Id);
            return response;
        }

        public async Task<List<ManagerEntity>> GetManagerEntities()
        {
            var response =  _container.GetItemLinqQueryable<ManagerEntity>(true).Where(q=>
            q.Active && q.Archived == false && q.DocumentType == Credentials.ManagerDocumentType).ToList();
            return response;
        }

        public async Task<ManagerEntity> GetManagerEntityByUId(string uid)
        {
            var response = _container.GetItemLinqQueryable<ManagerEntity>(true).Where(q => q.UId == uid &&
            q.Active && q.Archived == false && q.DocumentType == Credentials.ManagerDocumentType).FirstOrDefault();
            return response;
        }

        public async Task<List<OfficeEntity>> GetOfficeEntities()
        {
            var response = _container.GetItemLinqQueryable<OfficeEntity>(true).Where(q =>
            q.Active && q.Archived == false && q.DocumentType == Credentials.OfficeDocumentType).ToList();
            return response;
        }

        public async Task<OfficeEntity> GetOfficeEntityByUId(string uid)
        {
            var response = _container.GetItemLinqQueryable<OfficeEntity>(true).Where(q => q.UId == uid &&
            q.Active && q.Archived == false && q.DocumentType == Credentials.OfficeDocumentType).FirstOrDefault();
            return response;
        }

        public async Task<List<PassEntity>> GetPassEntities()
        {
            var response = _container.GetItemLinqQueryable<PassEntity>(true).Where(q =>
            q.Active && q.Archived == false && q.DocumentType == Credentials.PassDocumentType).ToList();
            return response;
        }

        public async Task<PassEntity> GetPassEntityByUId(string uid)
        {
            var response = _container.GetItemLinqQueryable<PassEntity>(true).Where(q => q.UId == uid &&
            q.Active && q.Archived == false && q.DocumentType == Credentials.PassDocumentType).FirstOrDefault();
            return response;
        }

        public async Task<List<SecurityEntity>> GetSecurityEntities()
        {
            var response = _container.GetItemLinqQueryable<SecurityEntity>(true).Where(q =>
            q.Active && q.Archived == false && q.DocumentType == Credentials.SecurityDocumentType).ToList();
            return response;
        }

        public async Task<SecurityEntity> GetSecurityEntityByUId(string uid)
        {
            var response = _container.GetItemLinqQueryable<SecurityEntity>(true).Where(q => q.UId == uid &&
            q.Active && q.Archived == false && q.DocumentType == Credentials.SecurityDocumentType).FirstOrDefault();
            return response;
        }

        public async Task<List<VisitorEntity>> GetVisitorEntities()
        {
            var response = _container.GetItemLinqQueryable<VisitorEntity>(true).Where(q =>
            q.Active && q.Archived == false && q.DocumentType == Credentials.VisitorDocumentType).ToList();
            return response;
        }

        public async Task<VisitorEntity> GetVisitorEntityByUId(string uid)
        {
            var response = _container.GetItemLinqQueryable<VisitorEntity>(true).Where(q => q.UId == uid &&
            q.Active && q.Archived == false && q.DocumentType == Credentials.VisitorDocumentType).FirstOrDefault();
            return response;
        }

        public async Task<bool> IsManagerExist(string email)
        {
            var response = _container.GetItemLinqQueryable<ManagerEntity>(true).Where(q => q.Email == email &&
            q.Active && q.Archived == false && q.DocumentType == Credentials.ManagerDocumentType).FirstOrDefault();
            return response != null;
        }

        public async Task<bool> IsOfficeExist(string email)
        {
            var response = _container.GetItemLinqQueryable<OfficeEntity>(true).Where(q => q.Email == email &&
            q.Active && q.Archived == false && q.DocumentType == Credentials.OfficeDocumentType).FirstOrDefault();
            return response != null;
        }

        public async Task<bool> IsSecurityExist(string email)
        {
            var response = _container.GetItemLinqQueryable<SecurityEntity>(true).Where(q => q.Email == email &&
            q.Active && q.Archived == false && q.DocumentType == Credentials.SecurityDocumentType).FirstOrDefault();
            return response != null;
        }

        public async Task<bool> IsVisitorExist(string email)
        {
            var response = _container.GetItemLinqQueryable<VisitorEntity>(true).Where(q => q.Email == email &&
            q.Active && q.Archived == false && q.DocumentType == Credentials.VisitorDocumentType).FirstOrDefault();
            return response != null;
        }

        public async Task<ManagerEntity> GetManagerEntityByOfficeId(string officeId)
        {
            var response = _container.GetItemLinqQueryable<ManagerEntity>(true).Where(q => q.OfficeId == officeId &&
            q.Active && q.Archived == false && q.DocumentType == Credentials.ManagerDocumentType).FirstOrDefault();
            return response;
        }

        public async Task<bool> IsManagerExist(string email, string pasword)
        {
            var response = _container.GetItemLinqQueryable<ManagerEntity>(true).Where(q => q.Email == email && q.Password == pasword &&
            q.Active && q.Archived == false && q.DocumentType == Credentials.ManagerDocumentType).FirstOrDefault();
            return response != null;
        }

        public async Task<bool> IsOfficeExist(string email, string pasword)
        {
            var response = _container.GetItemLinqQueryable<OfficeEntity>(true).Where(q => q.Email == email && q.Password == pasword &&
            q.Active && q.Archived == false && q.DocumentType == Credentials.OfficeDocumentType).FirstOrDefault();
            return response != null;
        }

        public async Task<bool> IsSecurityExist(string email, string pasword)
        {
            var response = _container.GetItemLinqQueryable<SecurityEntity>(true).Where(q => q.Email == email && q.Password == pasword &&
            q.Active && q.Archived == false && q.DocumentType == Credentials.SecurityDocumentType).FirstOrDefault();
            return response != null;
        }

        public async Task<List<PassEntity>> GetPassEntitiesByApprovalStatus(string officeid, bool isApproved)
        {
            var response = _container.GetItemLinqQueryable<PassEntity>(true).Where(q =>q.UId == officeid && q.IsApproved == isApproved &&
            q.Active && q.Archived == false && q.DocumentType == Credentials.PassDocumentType).ToList();
            return response;
        }
    }
}
