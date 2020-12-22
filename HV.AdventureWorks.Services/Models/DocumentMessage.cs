namespace HV.AdventureWorks.Services.Models
{
    public class DocumentMessage
    {
        public string FileName { get; set; }
        public string BlobFileName { get; set; }
        public string FileMimeType { get; set; }
        public string DocumentNode { get; set; }
        public string FileExtension { get; set; }
    }
}
