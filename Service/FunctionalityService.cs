using SendGrid.Helpers.Mail;
using SendGrid;
using Visitor_Security_Clearance_System.Common;
using Visitor_Security_Clearance_System.CosmosDB;
using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Entity;
using Visitor_Security_Clearance_System.Interface;
using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace Visitor_Security_Clearance_System.Service
{
    public class FunctionalityService : IFunctionalityService
    {
        private readonly ICosmoseDBService _iCosmoseDBService;

        public FunctionalityService(ICosmoseDBService iCosmoseDBService)
        {
            _iCosmoseDBService = iCosmoseDBService;
        }

        public async Task<PassDTO> AddPass(PassDTO passDTO)
        {
            PassEntity toAdd = new PassEntity();
            toAdd.Initialize(true, Credentials.PassDocumentType, "Tushar");

            toAdd.VisitorId = passDTO.VisitorId;
            toAdd.ApprovedByManagerId = passDTO.ApprovedByManagerId;
            toAdd.IsApproved = passDTO.IsApproved;

            PassEntity toMap = new PassEntity();

            if (passDTO.IsApproved)
            {
                toAdd.PdfFileName = "Pass_"+ passDTO.VisitorId + ".pdf";
                toMap = await _iCosmoseDBService.AddEntity(toAdd);

                var Visitor = await _iCosmoseDBService.GetVisitorEntityByUId(toMap.VisitorId);

                var pdfFilePath = await CreatePassPDF(Visitor, toMap);

                await SendMailToVisitor(Visitor, pdfFilePath);
            }
            else
            {
                toAdd.PdfFileName = null;
                toMap = await _iCosmoseDBService.AddEntity(toAdd);
            }

            PassDTO response = new PassDTO()
            {
                UId = toMap.UId,
                VisitorId =toMap.VisitorId,
                ApprovedByManagerId = toMap.ApprovedByManagerId,
                PdfFileName = toMap.PdfFileName,
                IsApproved = toMap.IsApproved,   
            };

            return response;
        }

        public async Task SendMailToVisitor(VisitorEntity visitorEntity, string pdfFilePath)
        {
            string body = string.Empty;

            using (StreamReader reader = new StreamReader("./EmailTemplate/VisitApprovedToVisitor.html"))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{VisitorName}", visitorEntity.Name).Replace("{Purpose}", visitorEntity.Purpose);

            var apiKey = Credentials.EmailApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(Credentials.SenderMail, "Visitor Security Clearance System");
            var to = new EmailAddress(visitorEntity.Email);
            var msg = MailHelper.CreateSingleEmail(from, to, "Visit Request Approved", body, body);

            var attachment = new Attachment
            {
                Content = Convert.ToBase64String(File.ReadAllBytes(pdfFilePath)),
                Type = "application/pdf",
                Filename = Path.GetFileName(pdfFilePath),
                Disposition = "attachment"
            };
            msg.AddAttachment(attachment);

            var response = await client.SendEmailAsync(msg);

        }

        public async Task<string> CreatePassPDF(VisitorEntity visitorEntity, PassEntity passEntity)
        {
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Passes");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string filePath = Path.Combine(directoryPath, passEntity.PdfFileName);

            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                PdfWriter writer = new PdfWriter(fs);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                document.Add(new Paragraph("Visitor Pass"));
                document.Add(new Paragraph("Name: " + visitorEntity.Name));
                document.Add(new Paragraph("Company: " + visitorEntity.CompanyName));
                document.Add(new Paragraph("Purpose: " + visitorEntity.Purpose));
                document.Add(new Paragraph("EntryTime: " + visitorEntity.EntryTime));
                document.Add(new Paragraph("ExitTime: " + visitorEntity.ExitTime));
                document.Add(new Paragraph("Pass ID: " + passEntity.UId));
                document.Add(new Paragraph("Approval Status: Approved"));

                document.Close();
            }

            return filePath;
        }

        public async Task<List<VisitorDTO>> GetVisitorsByStatusAndOfficeId(string officeId, bool isApproved)
        {
            var PassEntities = await _iCosmoseDBService.GetPassEntitiesByApprovalStatus(officeId, isApproved);
            List<VisitorEntity> visitorEntities = await _iCosmoseDBService.GetVisitorEntities();

            List<VisitorDTO> response = new List<VisitorDTO>();

            foreach (var pass in PassEntities)
            {
                VisitorEntity entity = visitorEntities.Where(x => x.UId == pass.VisitorId).FirstOrDefault();
                
                VisitorDTO visitor = new VisitorDTO()
                {
                    UId = entity.UId,
                    OfficeId = entity.OfficeId,
                    Name = entity.Name,
                    Email = entity.Email,
                    PhoneNumber = entity.PhoneNumber,
                    Address = entity.Address,
                    Purpose = entity.Purpose,
                    CompanyName = entity.CompanyName,
                    EntryTime = entity.EntryTime,
                    ExitTime = entity.ExitTime
                };
                response.Add(visitor);
            }

            return response;
        }

        public async Task<bool> Login(string username, string password, string role)
        {
            if (role == Credentials.SecurityDocumentType)
            {
                return await _iCosmoseDBService.IsSecurityExist(username, password);
            }
            else if(role == Credentials.ManagerDocumentType)
            {
                return await _iCosmoseDBService.IsManagerExist(username, password);
            }
            else if (role == Credentials.OfficeDocumentType)
            {
                return await _iCosmoseDBService.IsOfficeExist(username, password);
            }

            return false;
        }

        public async Task<PassDTO> UpdateApprovedStatusOfPass(string PassId, bool IsApproved)
        {
            PassEntity toUpdate = await _iCosmoseDBService.GetPassEntityByUId(PassId);
            toUpdate.Active = false;
            toUpdate.Archived = true;
            await _iCosmoseDBService.UpdateEntity(toUpdate);

            toUpdate.Initialize(false, Credentials.PassDocumentType, "Tushar");
            toUpdate.IsApproved = IsApproved;

            PassEntity toMap = await _iCosmoseDBService.AddEntity(toUpdate);

            PassDTO response = new PassDTO()
            {
                UId = toMap.UId,
                VisitorId = toMap.VisitorId,
                ApprovedByManagerId = toMap.ApprovedByManagerId,
                PdfFileName = toMap.PdfFileName,
                IsApproved = toMap.IsApproved,
            };

            return response;

        }
    }
}
