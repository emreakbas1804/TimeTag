namespace TimeTag.Domain.Entities
{
    public class Licance : BaseModel
    {
        public string SerialNumber { get; set; }
        public bool IsAdded {get;set;}
        public Company Company { get; set; }
    }
}