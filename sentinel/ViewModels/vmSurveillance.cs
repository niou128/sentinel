using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace sentinel.ViewModels
{
    public class vmSurveillance : BindableBase
    {
        public enum eStatut
        {
            Inactif,
            encours
        }

        #region Private Properties
        DateTime obLastRaise;
        private MediaPlayer mediaPlayer;// = new MediaPlayer();
        #endregion

        #region Public Properties
        public FileSystemWatcher observateur;
        #endregion

        #region Binding Properties
        public string OpenFilePath
        {
            get { return _OpenFilePath; }
            set
            {
                SetProperty(ref _OpenFilePath, value);
                if (!String.IsNullOrEmpty(value))
                    ShowPath = _OpenFilePath;
            }
        }
        protected string _OpenFilePath;

        public string OpenDirectoryPath
        {
            get { return _OpenDirectoryPath; }

            set
            {
                SetProperty(ref _OpenDirectoryPath, value);
                if (!String.IsNullOrEmpty(value))
                    ShowPath = _OpenDirectoryPath;
            }
        }
        protected string _OpenDirectoryPath;

        public string ShowPath
        {
            get
            {
                return _ShowPath;
            }
            set
            {
                if (SetProperty(ref _ShowPath, value))
                {
                    TypePath = String.IsNullOrEmpty(OpenFilePath) ? "Chemin du dossier surveillé :" : "Chemin du fichier surveillé :";
                }
            }
        }
        protected string _ShowPath;

        public string TypePath
        {
            get
            {
                if (String.IsNullOrEmpty(OpenFilePath) && !String.IsNullOrEmpty(OpenDirectoryPath))
                {
                    _TypePath = "Chemin du dossier surveillé :";
                }
                if (String.IsNullOrEmpty(OpenDirectoryPath) && !String.IsNullOrEmpty(OpenFilePath))
                {
                    _TypePath = "Chemin du fichier surveillé :";
                }
                return _TypePath;
            }
            set { SetProperty(ref _TypePath, value); }
        }
        protected string _TypePath;

        public string Statut
        {
            get
            {
                return _Statut;
            }
            set { SetProperty(ref _Statut, value); }
        }
        protected string _Statut;

        public bool IsSurveillanceActive
        {
            get { return _IsSurveillanceActive; }
            set { SetProperty(ref _IsSurveillanceActive, value); }
        }
        protected bool _IsSurveillanceActive;

        public string Song
        {
            get { return _Song; }
            set { SetProperty(ref _Song, value); }
        }
        protected string _Song;


        public string Modification
        {
            get { return _Modification; }
            set { SetProperty(ref _Modification, value); }
        }
        protected string _Modification;
        #endregion


        #region Constructor
        public vmSurveillance()
        {
            IsSurveillanceActive = false;
            Statut = "Aucune surveillance en cours";
            Modification = String.Empty;
        }
        #endregion

        #region Private Method
        /// <summary>
        /// Surveille la suppression de fichier dans le dossier surveillé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Observateur_Deleted(object sender, FileSystemEventArgs e)
        {
            if (DateTime.Now.Subtract(obLastRaise).TotalMilliseconds > 1000)
            {
                obLastRaise = DateTime.Now;
                //System.Threading.Thread.Sleep(100);
                PlaySong();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Modification);
                sb.Append("Le fichier " + e.Name + " à été supprimé à " + DateTime.Now.ToShortTimeString());

                Modification = sb.ToString();
            }
        }

        /// <summary>
        /// En cas de création d'un fichier dans le dossier surveillé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Observateur_Created(object sender, FileSystemEventArgs e)
        {
            if (DateTime.Now.Subtract(obLastRaise).TotalMilliseconds > 1000)
            {
                obLastRaise = DateTime.Now;
                //System.Threading.Thread.Sleep(100);
                PlaySong();

                string CreatedFileName = e.Name;
                FileInfo createdfile = new FileInfo(CreatedFileName);
                string extension = createdfile.Extension;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Modification);
                sb.Append("Ajout du fichier " + CreatedFileName + " à " + DateTime.Now.ToShortTimeString());

                Modification = sb.ToString();                
            }
        }

        /// <summary>
        /// Surveille tous les changements dans le dossier surveillé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Observateur_Changed(object sender, FileSystemEventArgs e)
        {
            if (DateTime.Now.Subtract(obLastRaise).TotalMilliseconds > 1000)
            {
                obLastRaise = DateTime.Now;
                //System.Threading.Thread.Sleep(100);
                PlaySong();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Modification);
                sb.Append("Modification du fichier " + e.Name + " à " + DateTime.Now.ToShortTimeString());

                Modification = sb.ToString();
            }

        }

        /// <summary>
        /// Surveille le changement de nom des fichiers présents dans le dossier surveillé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Observateur_Renamed(object sender, RenamedEventArgs e)
        {
            if (DateTime.Now.Subtract(obLastRaise).TotalMilliseconds > 1000)
            {
                obLastRaise = DateTime.Now;
               // System.Threading.Thread.Sleep(100);
                PlaySong();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Modification);
                sb.Append("Le fichier " + e.OldName + " à été renommé en " + e.Name + " à "+ DateTime.Now.ToShortTimeString());

                Modification = sb.ToString();

            }
        }

        private void PlaySong()
        {
            if (String.IsNullOrEmpty(Song))
            {
                SystemSounds.Beep.Play();
            }
            else
            {
                mediaPlayer = new MediaPlayer();
                mediaPlayer.Open(new Uri(Song));
                mediaPlayer.Play();
            }
        }
        #endregion


        #region Public Methods
        public void AddEventListener()
        {
            observateur.Renamed += Observateur_Renamed;
            observateur.Changed += Observateur_Changed;
            observateur.Created += Observateur_Created;
            observateur.Deleted += Observateur_Deleted;
        }

        public void RemoveEventListener()
        {
            observateur.Renamed -= Observateur_Renamed;
            observateur.Changed -= Observateur_Changed;
            observateur.Created -= Observateur_Created;
            observateur.Deleted -= Observateur_Deleted;
        }

        public void StartSurveillance()
        {
            IsSurveillanceActive = true;
            Statut = "Surveillance en cours";
            if (!String.IsNullOrEmpty(OpenFilePath))
            {
                var dirPath = Directory.GetParent(OpenFilePath).FullName;
                var filter = Path.GetFileName(OpenFilePath);
                observateur = new FileSystemWatcher(dirPath);
                observateur.Filter = filter;
            }
            else
            {
                observateur = new FileSystemWatcher(ShowPath, "*.*");
            }
            observateur.EnableRaisingEvents = true;
            observateur.IncludeSubdirectories = true;
            AddEventListener();
        }

        public void StopSurveillance()
        {
            IsSurveillanceActive = false;
            Statut = "Aucune surveillance en cours";
            Modification = String.Empty;
            observateur.EnableRaisingEvents = false;
            RemoveEventListener();
        }

        #endregion
    }
}
