using GESIDENTITY.COMMON;
using GESIDENTITY.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Data.DataForm;
using Telerik.Windows.Data;

namespace GESIDENTITY.IdentificationsModules.ViewModels
{
    public class IdentificationsViewModel : ObservableObject
    {

        #region Members
        public GESIDENTITYEntities model;
        private BackgroundWorker worker = new BackgroundWorker();
        ObservableCollection<Identifications> _data = new ObservableCollection<Identifications>();
        ObservableCollection<Districts> _allDistricts = new ObservableCollection<Districts>();
        ObservableCollection<Regions> _allRegions = new ObservableCollection<Regions>();
        ObservableCollection<Departements> _allDepartements = new ObservableCollection<Departements>();
        ObservableCollection<Federations> _allFederations = new ObservableCollection<Federations>();
        ObservableCollection<Sections> _allSections = new ObservableCollection<Sections>();
        ObservableCollection<Bases> _allBases = new ObservableCollection<Bases>();
        ObservableCollection<Fonctions> _allFonctions = new ObservableCollection<Fonctions>();
        ObservableCollection<FonctionParti> _allFonctionParti = new ObservableCollection<FonctionParti>();
        ObservableCollection<NaturePiece> _allNaturePiece = new ObservableCollection<NaturePiece>();
        ObservableCollection<Utilisateurs> _allUtilisateurs = new ObservableCollection<Utilisateurs>();
        Identifications _selectedData = new Identifications();
        bool _isBusy;
        int _count = 0;
        #endregion

        #region Properties
        public ObservableCollection<Identifications> AllData
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                RaisePropertyChanged("AllData");
            }
        }

        public ObservableCollection<Districts> AllDistricts
        {
            get
            {
                return _allDistricts;
            }
            set
            {
                _allDistricts = value;
                RaisePropertyChanged("AllDistricts");
            }
        }

        public ObservableCollection<Regions> AllRegions
        {
            get
            {
                return _allRegions;
            }
            set
            {
                _allRegions = value;
                RaisePropertyChanged("AllRegions");
            }
        }

        public ObservableCollection<Departements> AllDepartements
        {
            get
            {
                return _allDepartements;
            }
            set
            {
                _allDepartements = value;
                RaisePropertyChanged("AllDepartements");
            }
        }

        public ObservableCollection<Federations> AllFederations
        {
            get
            {
                return _allFederations;
            }
            set
            {
                _allFederations = value;
                RaisePropertyChanged("AllFederations");
            }
        }

        public ObservableCollection<Sections> AllSections
        {
            get
            {
                return _allSections;
            }
            set
            {
                _allSections = value;
                RaisePropertyChanged("AllSections");
            }
        }

        public ObservableCollection<Bases> AllBases
        {
            get
            {
                return _allBases;
            }
            set
            {
                _allBases = value;
                RaisePropertyChanged("AllBases");
            }
        }

        public ObservableCollection<Fonctions> AllFonctions
        {
            get
            {
                return _allFonctions;
            }
            set
            {
                _allFonctions = value;
                RaisePropertyChanged("AllFonctions");
            }
        }

        public ObservableCollection<FonctionParti> AllFonctionParti
        {
            get
            {
                return _allFonctionParti;
            }
            set
            {
                _allFonctionParti = value;
                RaisePropertyChanged("AllFonctionParti");
            }
        }

        public ObservableCollection<NaturePiece> AllNaturePiece
        {
            get
            {
                return _allNaturePiece;
            }
            set
            {
                _allNaturePiece = value;
                RaisePropertyChanged("AllNaturePiece");
            }
        }

        public ObservableCollection<Utilisateurs> AllUtilisateurs
        {
            get
            {
                return _allUtilisateurs;
            }
            set
            {
                _allUtilisateurs = value;
                RaisePropertyChanged("AllUtilisateurs");
            }
        }

        public Identifications SelectedData
        {
            get
            {
                return _selectedData;
            }
            set
            {
                _selectedData = value;
                RaisePropertyChanged("SelectedData");
            }
        }

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                RaisePropertyChanged("IsBusy");
            }
        }
        #endregion

        #region Construction
        public IdentificationsViewModel()
        {
            worker.DoWork += this.WorkerDoWork;
            worker.RunWorkerCompleted += WorkerRunWorkerCompleted;

            Refresh();

        }

        public void Refresh()
        {

            //Demarre le Background Worker et commence le radBusyIndicator
            if (!worker.IsBusy)
            {
                this.IsBusy = true;
                worker.RunWorkerAsync();
            }

        }

        //La methode de demmarge du BackgroundWorker
        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            //appel la methode load
            Load();
        }

        //Met a jour le UI et stop le RadBusyIndicator
        private void UpdateDataSource()
        {

            this.IsBusy = false;

        }

        void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Quand la methode load a fini de charger les infos on appel la methode UpdateDataSource
            //pour mettre a jour le UI
            //Dispatcher.BeginInvoke(new Action(this.UpdateDataSource));

            this.IsBusy = false;
        }

        private void Load()
        {

            model = new GESIDENTITYEntities();

            var resultat = from res in model.Identifications
                           where res.Etat == "ACTIF"
                           select res;

            AllData = new ObservableCollection<Identifications>(resultat.ToList());

            var resultat1 = from res in model.Districts
                            where res.Etat == "ACTIF"
                            select res;

            AllDistricts = new ObservableCollection<Districts>(resultat1.ToList());
            var resultat2 = from res in model.Regions
                            where res.Etat == "ACTIF"
                            select res;

            AllRegions = new ObservableCollection<Regions>(resultat2.ToList());
            //var resultat3 = from res in model.Departements
            //                where res.Etat == "ACTIF"
            //                select res;

            //AllDepartements = new ObservableCollection<Departements>(resultat3.ToList());
            var resultat4 = from res in model.Federations
                            where res.Etat == "ACTIF"
                            select res;

            AllFederations = new ObservableCollection<Federations>(resultat4.ToList());
            //var resultat5 = from res in model.Sections
            //                where res.Etat == "ACTIF"
            //                select res;

            //AllSections = new ObservableCollection<Sections>(resultat5.ToList());
            //var resultat6 = from res in model.Bases
            //                where res.Etat == "ACTIF"
            //                select res;

            //AllBases = new ObservableCollection<Bases>(resultat6.ToList());
            var resultat7 = from res in model.Fonctions
                            where res.Etat == "ACTIF"
                            select res;

            AllFonctions = new ObservableCollection<Fonctions>(resultat7.ToList());
            var resultat8 = from res in model.FonctionParti
                            where res.Etat == "ACTIF"
                            select res;

            AllFonctionParti = new ObservableCollection<FonctionParti>(resultat8.ToList());
            var resultat9 = from res in model.Utilisateurs
                            where res.Etat == "ACTIF"
                            select res;

            AllUtilisateurs = new ObservableCollection<Utilisateurs>(resultat9.ToList());
            var resultat10 = from res in model.NaturePiece
                             where res.Etat == "ACTIF"
                            select res;

            AllNaturePiece = new ObservableCollection<NaturePiece>(resultat10.ToList());



        }

        public void CancelChanged()
        {

            var resultat = from res in model.Identifications
                           where res.Etat == "ACTIF"
                           select res;

            AllData = new ObservableCollection<Identifications>(resultat.ToList());

        }

        public void SaveChanged()
        {

            model.SaveChangesAsync();

        }

        #endregion

        #region Commands
        void AddCommandExecute()
        {

            if (SelectedData != null)
            {

            }
        }

        bool CanAddCommandExecute()
        {
            return true;
        }

        public ICommand AddCommand { get { return new RelayCommand(AddCommandExecute, CanAddCommandExecute); } }


        #endregion

    }
}
