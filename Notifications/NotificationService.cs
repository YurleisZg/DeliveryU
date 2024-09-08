using FirebaseAdmin.Messaging;

namespace DeliveryU.Notifications;

public class NotificationService :  INotificationService
{
    public async Task SubscribeToTopicAsync(string token, string topic)
    {
        try
        {
            var response = await FirebaseMessaging.DefaultInstance.SubscribeToTopicAsync(new[] {token}, topic);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error subscribing to topic: {ex.Message}");
        }
    }

    public async Task SendNotificationToTopicAsync(string topic, string title, string body)
    {
        var message = new Message()
        {
            Notification = new Notification()
            {
                Title = title,
                Body = body
            },
            Topic = topic
        };

        try
        {
            string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al enviar el mensaje: {ex.Message}");
        }
    }
}