namespace DeliveryU.Notifications;

public interface INotificationService 
{
    Task SubscribeToTopicAsync(string token, string topic);
    Task SendNotificationToTopicAsync(string topic, string title, string body);
}