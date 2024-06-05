namespace Visitor_Security_Clearance_System.Common
{
    public class Credentials
    {
        public static readonly string CosmosUrl = Environment.GetEnvironmentVariable("cosmosUrl");

        public static readonly string PrimaryKey = Environment.GetEnvironmentVariable("primaryKey");

        public static readonly string DatabaseName = Environment.GetEnvironmentVariable("databaseName");

        public static readonly string Container = Environment.GetEnvironmentVariable("container");

        public static readonly string OfficeDocumentType = "Office";

        public static readonly string ManagerDocumentType = "Manager";

        public static readonly string SecurityDocumentType = "Security";

        public static readonly string VisitorDocumentType = "Visitor";

        public static readonly string PassDocumentType = "Pass";

        public static readonly string SenderMail = Environment.GetEnvironmentVariable("senderEmail");

        public static readonly string EmailApiKey = Environment.GetEnvironmentVariable("emaiApiKey");

    }
}
