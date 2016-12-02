namespace Vnap.Services
{
    public interface INotificationService
    {
        void Notify(string title, string content, int badge, string parameter = null);

        void SetBadge(int count);

        void ClearBadge();

        //void RegisterDevice();

        //Task UnregisterDevice();
    }
}
