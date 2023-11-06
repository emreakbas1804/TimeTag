namespace TimeTag.Domain.Entities
{
    public class FileUpload : BaseModel
    {
        public string FileUrl { get; set; }
        public string OriginalFileName { get; set; }
        public string FileSize { get; set; }
    }
}