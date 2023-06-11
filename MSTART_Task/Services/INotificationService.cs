namespace MSTART_Task.Services
{
    public interface INotificationService
    {
        public void Successfully(string message);
        public void Error(string message);
        public void Warning(string message);
    }
}
