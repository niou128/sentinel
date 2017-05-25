using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace sentinel.ViewModels
{
    public class vmSurveillance : BindableBase
    {
        public enum eStatut
        {
            Inactif,
            encours
        }

        #region Binding Properties
        public string OpenFilePath
        {
            get { return _OpenFilePath; }
            set
            {
                SetProperty(ref _OpenFilePath, value);
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
            set { SetProperty(ref _ShowPath, value); }
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
                 _Statut = "Inactif";
                return _Statut;
            }
            set { SetProperty(ref _Statut, value); }
        }
        protected string _Statut;

        #endregion


        #region Constructor
        public vmSurveillance()
        {
            OpenFilePath = "Ceci est un test";
        }
        #endregion
    }
}
