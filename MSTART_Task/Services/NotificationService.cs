using NToastNotify;

namespace MSTART_Task.Services
{
    public class NotificationService:INotificationService
    {

        private readonly IToastNotification _toastNotification;
        public NotificationService(IToastNotification toastNotification)
        {
            _toastNotification = toastNotification;
        }

        public void Successfully(string message)=>_toastNotification.AddSuccessToastMessage(message);
           
        public void Error(string message) => _toastNotification.AddErrorToastMessage(message);

        public void Warning(string message) => _toastNotification.AddWarningToastMessage(message);
    }
}
