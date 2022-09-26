namespace Common.HelperContract
{
    public interface IRabbitMQHelper
    {
        public bool EnqueueProductMailAnnouncement(string Message);
    }
}
