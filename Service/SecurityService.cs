using Visitor_Security_Clearance_System.Common;
using Visitor_Security_Clearance_System.CosmosDB;
using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Entity;
using Visitor_Security_Clearance_System.Interface;

namespace Visitor_Security_Clearance_System.Service
{
    public class SecurityService : ISecurityService
    {
        private readonly ICosmoseDBService _iCosmoseDBService;
        public SecurityService(ICosmoseDBService iCosmoseDBService)
        {
            _iCosmoseDBService = iCosmoseDBService;
        }

        public async Task<SecurityDTO> AddSecurity(SecurityDTO securityDTO)
        {
            SecurityEntity toAdd = new SecurityEntity();
            toAdd.Initialize(true, Credentials.SecurityDocumentType, "Tushar");

            toAdd.Name = securityDTO.Name;
            toAdd.Email = securityDTO.Email;
            toAdd.Password = securityDTO.Password;
            toAdd.PhoneNumber = securityDTO.PhoneNumber;
            toAdd.Role = Credentials.SecurityDocumentType;

            SecurityEntity toMap = await _iCosmoseDBService.AddEntity(toAdd);

            SecurityDTO toResponse = new SecurityDTO()
            {
                UId = toMap.UId,
                Name = toMap.Name,
                Email = toMap.Email,
                Password = toMap.Password,
                PhoneNumber = toMap.PhoneNumber,
                Role = toMap.Role,
            };

            return toResponse;
        }

        public async Task<string> DeleteSecurityByUId(string uId)
        {
            SecurityEntity toDelete = await _iCosmoseDBService.GetSecurityEntityByUId(uId);
            toDelete.Active = false;
            toDelete.Archived = true;

            await _iCosmoseDBService.UpdateEntity(toDelete);

            toDelete.Initialize(false, Credentials.SecurityDocumentType, "Tushar");
            toDelete.Active = false;

            await _iCosmoseDBService.AddEntity(toDelete);

            return "Data Removed Successfully";
        }

        public async Task<SecurityDTO> GetSecurityByUId(string uId)
        {
            SecurityEntity toMap = await _iCosmoseDBService.GetSecurityEntityByUId(uId);

            if (toMap == null)
            {
                return null;
            }

            SecurityDTO toResponse = new SecurityDTO()
            {
                UId = toMap.UId,
                Name = toMap.Name,
                Email = toMap.Email,
                Password = toMap.Password,
                PhoneNumber = toMap.PhoneNumber,
                Role = toMap.Role,
            };

            return toResponse;
        }

        public async Task<bool> IsSecurityExist(string email)
        {
            var response = await _iCosmoseDBService.IsSecurityExist(email);
            return response;
        }

        public async Task<SecurityDTO> UpdateSecurity(SecurityDTO securityDTO)
        {
            SecurityEntity toUpdate = await _iCosmoseDBService.GetSecurityEntityByUId(securityDTO.UId);
            toUpdate.Active = false;
            toUpdate.Archived = true;
            await _iCosmoseDBService.UpdateEntity(toUpdate);

            toUpdate.Initialize(false, Credentials.SecurityDocumentType, "Tushar");
            toUpdate.Name = securityDTO.Name;
            toUpdate.Email = securityDTO.Email;
            toUpdate.Password = securityDTO.Password;
            toUpdate.PhoneNumber = securityDTO.PhoneNumber;
            toUpdate.Role = Credentials.SecurityDocumentType;

            SecurityEntity toMap = await _iCosmoseDBService.AddEntity(toUpdate);

            SecurityDTO toResponse = new SecurityDTO()
            {
                UId = toMap.UId,
                Name = toMap.Name,
                Email = toMap.Email,
                Password = toMap.Password,
                PhoneNumber = toMap.PhoneNumber,
                Role = toMap.Role,
            };

            return toResponse;
        }
    }
}
