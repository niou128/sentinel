using System;
namespace MahApps.Metro.Controls.Dialogs
{
    public interface IMetroDialogSettings
    {
        string AffirmativeButtonText { get; set; }
        bool AnimateHide { get; set; }
        bool AnimateShow { get; set; }
        System.Threading.CancellationToken CancellationToken { get; set; }
        MetroDialogColorScheme ColorScheme { get; set; }
        System.Windows.ResourceDictionary CustomResourceDictionary { get; set; }
        string DefaultText { get; set; }
        string FirstAuxiliaryButtonText { get; set; }
        double MaximumBodyHeight { get; set; }
        string NegativeButtonText { get; set; }
        string SecondAuxiliaryButtonText { get; set; }
        bool SuppressDefaultResources { get; set; }
    }
}
