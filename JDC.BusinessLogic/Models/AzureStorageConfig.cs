namespace JDC.BusinessLogic.Models
{
    public class AzureStorageConfig
    {
        public string AccountName { get; set; }

        public string AccountKey { get; set; }

        public string ImageContainer { get; set; }

        public string SasToken { get; set; }
    }
}
