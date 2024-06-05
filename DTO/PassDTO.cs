namespace Visitor_Security_Clearance_System.DTO
{
    public class PassDTO
    {
        public string UId { get; set; }
        public string VisitorId { get; set; }
        public string ApprovedByManagerId { get; set; }
        public bool IsApproved { get; set; }
        public string PdfFileName { get; set; }
    }
}
