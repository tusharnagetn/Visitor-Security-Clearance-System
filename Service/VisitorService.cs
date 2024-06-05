using SendGrid;
using SendGrid.Helpers.Mail;
using Visitor_Security_Clearance_System.Common;
using Visitor_Security_Clearance_System.CosmosDB;
using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Entity;
using Visitor_Security_Clearance_System.Interface;

namespace Visitor_Security_Clearance_System.Service
{
    public class VisitorService : IVisitorService
    {
        private readonly ICosmoseDBService _iCosmoseDBService;
        public VisitorService(ICosmoseDBService iCosmoseDBService)
        {
            _iCosmoseDBService = iCosmoseDBService;
        }

        public async Task<VisitorDTO> AddVisitorAndSendMailToManager(VisitorDTO visitorDTO)
        {
            VisitorEntity toAdd = new VisitorEntity();
            toAdd.Initialize(true, Credentials.VisitorDocumentType, "Tushar");

            toAdd.OfficeId = visitorDTO.OfficeId;
            toAdd.Name = visitorDTO.Name;
            toAdd.Email = visitorDTO.Email;
            toAdd.PhoneNumber = visitorDTO.PhoneNumber;
            toAdd.Address = visitorDTO.Address;
            toAdd.Purpose = visitorDTO.Purpose;
            toAdd.CompanyName = visitorDTO.CompanyName;
            toAdd.EntryTime = visitorDTO.EntryTime;
            toAdd.ExitTime = visitorDTO.ExitTime;

            VisitorEntity toMap = await _iCosmoseDBService.AddEntity(toAdd);

            await SendMailToManagerForApproval(toMap);

            VisitorDTO toResponse = new VisitorDTO()
            {
                UId = toMap.UId,
                OfficeId = toMap.OfficeId,
                Name = toMap.Name,
                Email = toMap.Email,
                PhoneNumber = toMap.PhoneNumber,
                Address = toMap.Address,
                Purpose = toMap.Purpose,
                CompanyName = toMap.CompanyName,
                EntryTime = toMap.EntryTime,
                ExitTime = toMap.ExitTime
            };

            return toResponse;
        }

        public async Task SendMailToManagerForApproval(VisitorEntity visitorEntity)
        {
            ManagerEntity manager = await _iCosmoseDBService.GetManagerEntityByOfficeId(visitorEntity.OfficeId);

            string body = string.Empty;

            using (StreamReader reader = new StreamReader("./EmailTemplate/ApprovalRequestToManager.html"))
            { 
                body = reader.ReadToEnd(); 
            }
            body = body.Replace("{VisitorName}", visitorEntity.Name).Replace("{CompanyName}", visitorEntity.CompanyName).Replace("{Purpose}",visitorEntity.Purpose);

            var apiKey = Credentials.EmailApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(Credentials.SenderMail, "Visitor Security Clearance System");
            var to = new EmailAddress(manager.Email);
            var msg = MailHelper.CreateSingleEmail(from, to, "Request For Approval", body, body);

            var response = await client.SendEmailAsync(msg);

        }

        public async Task<string> DeleteVisitorByUId(string uId)
        {
            VisitorEntity toDelete = await _iCosmoseDBService.GetVisitorEntityByUId(uId);
            toDelete.Active = false;
            toDelete.Archived = true;

            await _iCosmoseDBService.UpdateEntity(toDelete);

            toDelete.Initialize(false, Credentials.VisitorDocumentType, "Tushar");
            toDelete.Active = false;

            await _iCosmoseDBService.AddEntity(toDelete);

            return "Data Removed Successfully";
        }

        public async Task<VisitorDTO> GetVisitorByUId(string uId)
        {
            VisitorEntity toMap = await _iCosmoseDBService.GetVisitorEntityByUId(uId);

            if (toMap == null)
            {
                return null;
            }

            VisitorDTO toResponse = new VisitorDTO()
            {
                UId = toMap.UId,
                OfficeId = toMap.OfficeId,
                Name = toMap.Name,
                Email = toMap.Email,
                PhoneNumber = toMap.PhoneNumber,
                Address = toMap.Address,
                Purpose = toMap.Purpose,
                CompanyName = toMap.CompanyName,
                EntryTime = toMap.EntryTime,
                ExitTime = toMap.ExitTime
            };

            return toResponse;
        }

        public async Task<bool> IsVisitorExist(string email)
        {
            var response = await _iCosmoseDBService.IsVisitorExist(email);
            return response;
        }

        public async Task<VisitorDTO> UpdateVisitor(VisitorDTO visitorDTO)
        {
            VisitorEntity toUpdate = await _iCosmoseDBService.GetVisitorEntityByUId(visitorDTO.UId);
            toUpdate.Active = false;
            toUpdate.Archived = true;
            await _iCosmoseDBService.UpdateEntity(toUpdate);

            toUpdate.Initialize(false, Credentials.VisitorDocumentType, "Tushar");
            toUpdate.OfficeId = visitorDTO.OfficeId;
            toUpdate.Name = visitorDTO.Name;
            toUpdate.Email = visitorDTO.Email;
            toUpdate.PhoneNumber = visitorDTO.PhoneNumber;
            toUpdate.Address = visitorDTO.Address;
            toUpdate.Purpose = visitorDTO.Purpose;
            toUpdate.CompanyName = visitorDTO.CompanyName;
            toUpdate.EntryTime = visitorDTO.EntryTime;
            toUpdate.ExitTime = visitorDTO.ExitTime;

            VisitorEntity toMap = await _iCosmoseDBService.AddEntity(toUpdate);

            VisitorDTO toResponse = new VisitorDTO()
            {
                UId = toMap.UId,
                OfficeId = toMap.OfficeId,
                Name = toMap.Name,
                Email = toMap.Email,
                PhoneNumber = toMap.PhoneNumber,
                Address = toMap.Address,
                Purpose = toMap.Purpose,
                CompanyName = toMap.CompanyName,
                EntryTime = toMap.EntryTime,
                ExitTime = toMap.ExitTime
            };

            return toResponse;
        }
    }
}
