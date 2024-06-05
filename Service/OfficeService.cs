using Visitor_Security_Clearance_System.Common;
using Visitor_Security_Clearance_System.CosmosDB;
using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Entity;
using Visitor_Security_Clearance_System.Interface;

namespace Visitor_Security_Clearance_System.Service
{
    public class OfficeService : IOfficeService
    {
        private readonly ICosmoseDBService _iCosmoseDBService;
        public OfficeService(ICosmoseDBService iCosmoseDBService)
        {
            _iCosmoseDBService = iCosmoseDBService;
        }

        public async Task<OfficeDTO> AddOffice(OfficeDTO officeDTO)
        {
            OfficeEntity toAdd = new OfficeEntity();
            toAdd.Initialize(true, Credentials.OfficeDocumentType, "Tushar");

            toAdd.Organization = officeDTO.Organization;
            toAdd.Role = Credentials.OfficeDocumentType;
            toAdd.Email = officeDTO.Email;
            toAdd.Password = officeDTO.Password;
            toAdd.PhoneNumber = officeDTO.PhoneNumber;
            toAdd.Address = officeDTO.Address;
            
            OfficeEntity toMap = await _iCosmoseDBService.AddEntity(toAdd);

            OfficeDTO toResponse = new OfficeDTO()
            {
                UId = toMap.UId,
                Organization = toMap.Organization,
                Role = toMap.Role,
                Email = toMap.Email,
                Password = toMap.Password,
                PhoneNumber = toMap.PhoneNumber,
                Address = toMap.Address
            };

            return toResponse;
        }

        public async Task<string> DeleteOfficeByUId(string uId)
        {
            OfficeEntity toDelete = await _iCosmoseDBService.GetOfficeEntityByUId(uId);
            toDelete.Active = false;
            toDelete.Archived = true;

            await _iCosmoseDBService.UpdateEntity(toDelete);

            toDelete.Initialize(false, Credentials.OfficeDocumentType, "Tushar");
            toDelete.Active = false;

            await _iCosmoseDBService.AddEntity(toDelete);

            return "Data Removed Successfully";
        }

        public async Task<OfficeDTO> GetOfficeByUId(string uId)
        {
            OfficeEntity toMap = await _iCosmoseDBService.GetOfficeEntityByUId(uId);

            if (toMap == null)
            {
                return null;
            }

            OfficeDTO toResponse = new OfficeDTO()
            {
                UId = toMap.UId,
                Organization = toMap.Organization,
                Role = toMap.Role,
                Email = toMap.Email,
                Password = toMap.Password,
                PhoneNumber = toMap.PhoneNumber,
                Address = toMap.Address
            };

            return toResponse;
        }

        public async Task<bool> IsOfficeExist(string email)
        {
            var response = await _iCosmoseDBService.IsOfficeExist(email);
            return response;
        }

        public async Task<OfficeDTO> UpdateOffice(OfficeDTO officeDTO)
        {
            OfficeEntity toUpdate = await _iCosmoseDBService.GetOfficeEntityByUId(officeDTO.UId);
            toUpdate.Active = false;
            toUpdate.Archived = true;
            await _iCosmoseDBService.UpdateEntity(toUpdate);

            toUpdate.Initialize(false, Credentials.OfficeDocumentType, "Tushar");

            toUpdate.Organization = officeDTO.Organization;
            toUpdate.Role = officeDTO.Role;
            toUpdate.Email = officeDTO.Email;
            toUpdate.Password = officeDTO.Password;
            toUpdate.PhoneNumber = officeDTO.PhoneNumber;
            toUpdate.Address = officeDTO.Address;

            OfficeEntity toMap = await _iCosmoseDBService.AddEntity(toUpdate);

            OfficeDTO toResponse = new OfficeDTO()
            {
                UId = toMap.UId,
                Organization = toMap.Organization,
                Role = toMap.Role,
                Email = toMap.Email,
                Password = toMap.Password,
                PhoneNumber = toMap.PhoneNumber,
                Address = toMap.Address
            };

            return toResponse;
        }
    }
}
