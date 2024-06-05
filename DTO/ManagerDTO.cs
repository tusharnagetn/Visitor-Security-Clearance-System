namespace Visitor_Security_Clearance_System.DTO
{
    public class ManagerDTO
    {
        public string UId { get; set; }
        public string OfficeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; } = "Manager";
    }
}
