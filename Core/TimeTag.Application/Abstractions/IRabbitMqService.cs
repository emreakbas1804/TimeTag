namespace TimeTag.Application.Abstractions;
public interface IRabbitMqService
{
    void SendMessage<T>(T message);    
}