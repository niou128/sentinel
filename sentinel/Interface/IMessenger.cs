using MahApps.Metro.Controls.Dialogs;
using System;
using System.Threading.Tasks;

namespace sentinel.Interface
{
    /// <summary>
    /// Interface de description d'une messagerie par popup
    /// </summary>
    public interface IMessenger
    {
        /// <summary>
        /// Crée une notification au contenu entièrement customiser.
        /// </summary>
        /// <param name="customFlyout"></param>
        void CustomFlyoutControl(CustomFlyout customFlyout);

        /// <summary>
        /// Affiche/masque la notification customisé
        /// </summary>
        void SwitchCustomFlyout();

        /// <summary>
        /// Affichhe une notification avec un bouton de confirmation.
        /// </summary>
        /// <param name="message"></param>
        void FlyoutNotification(Message message);

        /// <summary>
        /// Affiche une notification auto-fermante.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="time">Délai avant la fermeture de la notification</param>
        /// <returns></returns>
        Task FlyoutNotification(Message message, int time);

        /// <summary>
        /// Affiche un message plein écran avec un bouton de confirmation.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task ShowMessageAsync(string title, Message message);

        /// <summary>
        /// Affiche un message plein écran, le nombre de  boutons est paramètrable.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="style"></param>
        /// <param name="settings"></param>
        /// <returns>Le bouton qui a été cliqué</returns>
        Task<MessageDialogResult> ShowToolkitMessageAsync(string title, Message message, MessageDialogStyle style = MessageDialogStyle.Affirmative, IMetroDialogSettings settings = null);

        /// <summary>
        /// Affiche un message d'attente en plein écran pendant l'execution d'une commande.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="action"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        Task ShowProgressAsync(string title, Message message, Action action, IMetroDialogSettings settings = null);

        /// <summary>
        /// Affiche un message d'attente en plein écran et renvoi le controller pour commander la progressbar.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        Task<IProgressDialogController> ShowProgressAsync(string title, string message, bool isCancelable = false, IMetroDialogSettings settings = null);


    }

    public class uselessMessenger : IMessenger
    {
        /// <summary>
        /// Affiche une notification entirement customiser.
        /// </summary>
        /// <param name="customFlyout"></param>
        public void CustomFlyoutControl(CustomFlyout customFlyout)
        { }

        /// <summary>
        /// Affiche/masque la notification customisé
        /// </summary>
        public void SwitchCustomFlyout()
        { }

        /// <summary>
        /// Affichhe une notification avec un bouton de confirmation.
        /// </summary>
        /// <param name="message"></param>
        public void FlyoutNotification(Message message)
        { }

        /// <summary>
        /// Affiche une notification auto-fermante.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="time">Délai avant la fermeture de la notification</param>
        /// <returns></returns>
        public async Task FlyoutNotification(Message message, int time)
        { await Task.Run(() => { }); }

        /// <summary>
        /// Affiche un message plein écran avec un bouton de confirmation.
        /// Le messsage peut être auto-fermant.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task ShowMessageAsync(string title, Message message)
        { await Task.Run(() => { }); }

        /// <summary>
        /// Affiche un message plein écran, le nombre de  boutons est paramètrable.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="style"></param>
        /// <param name="settings"></param>
        /// <returns>Le bouton qui a été cliqué</returns>
        public async Task<MessageDialogResult> ShowToolkitMessageAsync(string title, Message message, MessageDialogStyle style = MessageDialogStyle.Affirmative, IMetroDialogSettings settings = null)
        {
            await Task.Run(() => { });
            return MessageDialogResult.Affirmative;
        }

        /// <summary>
        /// Affiche un message d'attente en plein écran pendant l'execution d'une commande.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public async Task ShowProgressAsync(string title, Message message, Action action, IMetroDialogSettings settings = null)
        {
            await Task.Run(() => action());
        }

        /// <summary>
        /// Affiche un message d'attente en plein écran et renvoi le controller pour commander la progressbar.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task<IProgressDialogController> ShowProgressAsync(string title, string message, bool isCancelable = false, IMetroDialogSettings settings = null)
        {
            await Task.Run(() => { });
            return null;
        }
    }

}
