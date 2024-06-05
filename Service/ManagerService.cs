using Visitor_Security_Clearance_System.Common;
using Visitor_Security_Clearance_System.CosmosDB;
using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Entity;
using Visitor_Security_Clearance_System.Interface;

namespace Visitor_Security_Clearance_System.Service
{
    public class ManagerService : IManagerService
    {
        private readonly ICosmoseDBService _iCosmoseDBService;
        public ManagerService(ICosmoseDBService iCosmoseDBService) 
        { 
            _iCosmoseDBService = iCosmoseDBService;
        }

        public async Task<ManagerDTO> AddManager(ManagerDTO managerDTO)
        {
            ManagerEntity toAdd = new ManagerEntity();
            toAdd.Initialize(true, Credentials.ManagerDocumentType, "Tushar");

            toAdd.OfficeId = managerDTO.OfficeId;
            toAdd.Name = managerDTO.Name;
            toAdd.Email = managerDTO.Email;
            toAdd.Password = managerDTO.Password;
            toAdd.PhoneNumber = managerDTO.PhoneNumber;
            toAdd.Role = managerDTO.Role;

            ManagerEntity toMap = await _iCosmoseDBService.AddEntity(toAdd);

            ManagerDTO toResponse = new ManagerDTO()
            { 
                UId = toMap.UId,
                OfficeId = toMap.OfficeId,
                Name = toMap.Name,
                Email = toMap.Email,
                Password = toMap.Password,
                PhoneNumber = toMap.PhoneNumber,
                Role = toMap.Role,
            };

            return toResponse;
        }

        public async Task<string> DeleteManagerByUId(string uId)
        {
            ManagerEntity toDelete = await _iCosmoseDBService.GetManagerEntityByUId(uId);
            toDelete.Active = false;
            toDelete.Archived = true;

            await _iCosmoseDBService.UpdateEntity(toDelete);

            toDelete.Initialize(false, Credentials.ManagerDocumentType, "Tushar");
            toDelete.Active = false;

            await _iCosmoseDBService.AddEntity(toDelete);

            return "Data Removed Successfully";
        }

        public async Task<ManagerDTO> GetManagerByUId(string uId)
        {
            ManagerEntity toMap = await _iCosmoseDBService.GetManagerEntityByUId(uId);

            if (toMap == null)
            {
                return null;
            }

            ManagerDTO toResponse = new ManagerDTO()
            {
                UId = toMap.UId,
                OfficeId = toMap.OfficeId,
                Name = toMap.Name,
                Email = toMap.Email,
                Password = toMap.Password,
                PhoneNumber = toMap.PhoneNumber,
                Role = toMap.Role,
            };

            return toResponse;
        }

        public async Task<bool> IsManagerExist(string email)
        {
            var response = await _iCosmoseDBService.IsManagerExist(email);
            return response;
        }

        public async Task<ManagerDTO> UpdateManager(ManagerDTO managerDTO)
        {
            ManagerEntity toUpdate = await _iCosmoseDBService.GetManagerEntityByUId(managerDTO.UId);
            toUpdate.Active = false;
            toUpdate.Archived = true;
            await _iCosmoseDBService.UpdateEntity(toUpdate);

            toUpdate.Initialize(false, Credentials.ManagerDocumentType, "Tushar");
            toUpdate.OfficeId = managerDTO.OfficeId;
            toUpdate.Name = managerDTO.Name;
            toUpdate.Email = managerDTO.Email;
            toUpdate.Password = managerDTO.Password;
            toUpdate.PhoneNumber = managerDTO.PhoneNumber;
            toUpdate.Role = managerDTO.Role;

            ManagerEntity toMap = await _iCosmoseDBService.AddEntity(toUpdate);

            ManagerDTO toResponse = new ManagerDTO()
            {
                UId = toMap.UId,
                OfficeId = toMap.OfficeId,
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
