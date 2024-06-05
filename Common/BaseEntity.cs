using Newtonsoft.Json;

namespace Visitor_Security_Clearance_System.Common
{
    public class BaseEntity
    {
        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public string UId { get; set; }

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "dtype", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentType { get; set; }

        [JsonProperty(PropertyName = "version", NullValueHandling = NullValueHandling.Ignore)]
        public int Version { get; set; }

        [JsonProperty(PropertyName = "createdBy", NullValueHandling = NullValueHandling.Ignore)]
        public string CreatedBy { get; set; }

        [JsonProperty(PropertyName = "createdOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "updatedBy", NullValueHandling = NullValueHandling.Ignore)]
        public string UpdatedBy { get; set; }

        [JsonProperty(PropertyName = "updatedOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime UpdatedOn { get; set; }

        [JsonProperty(PropertyName = "active", NullValueHandling = NullValueHandling.Ignore)]
        public bool Active { get; set; }

        [JsonProperty(PropertyName = "archived", NullValueHandling = NullValueHandling.Ignore)]
        public bool Archived { get; set; }

        public void Initialize(bool isNew, string dType, string CreatedOrUpdatedByName)
        {
            DocumentType = dType;
            Id = Guid.NewGuid().ToString();
            Active = true;
            Archived = false;
            if(isNew)
            {
                UId = Id;
                CreatedBy = CreatedOrUpdatedByName;
                CreatedOn = DateTime.UtcNow;
                Version = 1;
                UpdatedBy = CreatedOrUpdatedByName;
                UpdatedOn = DateTime.UtcNow;
            }
            else
            {
                Version++;
                UpdatedBy = CreatedOrUpdatedByName;
                UpdatedOn = DateTime.UtcNow;
            }
        }
    }
}
