using TimeTag.Domain.Enums;

namespace TimeTag.Application.DTO
{
     public class EntityResultModel
    {
        public int Id { get; set; }
        public EntityResult Result { get; set; } = EntityResult.Error;
        public string ResultMessage { get; set; }
        public object ResultObject { get; set; }
    }
}