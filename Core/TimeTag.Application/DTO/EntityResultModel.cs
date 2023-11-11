using TimeTag.Domain.Enums;

namespace TimeTag.Application.DTO
{
     public class EntityResultModel
    {
        public EntityResult Result { get; set; } = EntityResult.Warning;
        public string ResultMessage { get; set; }
        public object ResultObject { get; set; }
    }
}