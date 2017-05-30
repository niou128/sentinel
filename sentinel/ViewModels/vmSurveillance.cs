using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

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

        #endregion


        #region Constructor
        public vmSurveillance()
        {
            IsSurveillanceActive = false;
            Statut = "Aucune surveillance en cours";
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
            MessageBox.Show("Fichier supprimé");
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
                MessageBox.Show("Fichier créé");
            }
        }

        /// <summary>
        /// Surveille tous les changements dans le dossier surveillé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Observateur_Changed(object sender, FileSystemEventArgs e)
        {

        }

        /// <summary>
        /// Surveille le changement de nom des fichiers présents dans le dossier surveillé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Observateur_Renamed(object sender, RenamedEventArgs e)
        {
            MessageBox.Show("Fichier renommé");
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
            observateur.EnableRaisingEvents = false;
            RemoveEventListener();
        }

        #endregion
    }
}
