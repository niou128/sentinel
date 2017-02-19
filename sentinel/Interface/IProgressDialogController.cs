using System;
namespace MahApps.Metro.Controls.Dialogs
{
    public interface IProgressDialogController
    {
        event EventHandler Canceled;
        System.Threading.Tasks.Task CloseAsync();
        event EventHandler Closed;
        bool IsCanceled { get; }
        bool IsOpen { get; }
        double Maximum { get; set; }
        double Minimum { get; set; }
        void SetCancelable(bool value);
        void SetIndeterminate();
        void SetMessage(string message);
        void SetProgress(double value);
        void SetTitle(string title);
    }
}
