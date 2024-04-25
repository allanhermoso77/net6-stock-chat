namespace Stock.Chat.RabbitMq.Contracts
{
    public interface IChatService
    {
        IChatApi CreateApi();
    }
}
