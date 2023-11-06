using System;

namespace TimeTag.Domain.Entities
{
    public class BaseModel
    {
        public int Id { get; set; }
        public DateTime RecordCreateTime { get; set; } = DateTime.Now;
        public DateTime LastUpdateTime { get; set; } = DateTime.Now;
    }
}